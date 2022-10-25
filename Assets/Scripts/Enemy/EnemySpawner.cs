using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private float _distanceFromPlayer;
    [SerializeField]
    private int _startingBudget;
    [SerializeField]
    private float _budgetIncreaseFactor;
    [SerializeField]
    private float _startWaveDurationSeconds;
    [SerializeField]
    private float _intervalBetweenWavesSeconds;
    [SerializeField]
    private List<EnemyUnit> _enemyUnits;

    private int _currentWave = 1;
    private bool _isSpawning = true;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }
    

    IEnumerator SpawnWaves()
    {
        while (_isSpawning)
        {
            List<EnemyUnit> enemiesToSpawn = GetEnemiesToSpawn();

            float spawnInterval = GetSpawnInterval(enemiesToSpawn.Count);            
            

            // instantiate/enable enemies
            while (enemiesToSpawn.Count > 0)
            {
                GameObject nextUnit = enemiesToSpawn[0].EnemyPrefabPool.GetItem();
                
                nextUnit.transform.position = GetRandomPositionAroundPlayer();
                nextUnit.SetActive(true);
                enemiesToSpawn.RemoveAt(0);
                yield return new WaitForSeconds(spawnInterval);
            }

            // wait until next round
            yield return new WaitForSeconds(_intervalBetweenWavesSeconds);

            // increment round number
            _currentWave++;
        }
    }

    float GetSpawnInterval(int numEnemies)
    {
        // determine spawn interval
        float spawnInterval = _startWaveDurationSeconds;
        if (_currentWave > 1)
        {
            spawnInterval *= (int)((_currentWave - 1));
        }
        spawnInterval /= numEnemies;

        return spawnInterval;
    }

    List<EnemyUnit> GetEnemiesToSpawn()
    {
        // create a random list of enemies for current budget
        int remainingBudget = _startingBudget;
        if (_currentWave > 1)
        {
            remainingBudget *= (int)((_currentWave - 1) * _budgetIncreaseFactor);
        }

        List<EnemyUnit> enemiesToSpawn = new List<EnemyUnit>();

        while (remainingBudget > 0)
        {
            int randomIndex = Random.Range(0, _enemyUnits.Count);
            if (_enemyUnits[randomIndex].Cost <= remainingBudget)
            {
                
                enemiesToSpawn.Add(_enemyUnits[randomIndex]);
                remainingBudget -= _enemyUnits[randomIndex].Cost;
            }
            else if (remainingBudget <= 0)
            {
                break;
            }
        }

        return enemiesToSpawn;
    }

    Vector3 GetRandomPositionAroundPlayer()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        return _player.transform.position + (randomDirection * _distanceFromPlayer);
    }
}

[System.Serializable]
public struct EnemyUnit
{
    public GameObjectPool EnemyPrefabPool;
    public int Cost;
}
