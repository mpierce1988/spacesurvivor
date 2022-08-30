using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{

    // Fields
    [SerializeField]
    protected GameObject _projectilePrefab;
    [SerializeField]
    protected float _fireSpeed;


    [SerializeField]
    protected Transform _spawnPoint;

    protected Coroutine _firingCoroutine;
    protected bool _isFiring;

    protected ObjectPool<GameObject> _pool;

    public GameObject ProjectilePrefab { get => _projectilePrefab; set { _projectilePrefab = value; } }
    public float FireSpeed { get => _fireSpeed; set { _fireSpeed = value; } }
    public bool IsFiring { get => _isFiring; set { _isFiring = value; } }
    public Transform SpawnPoint { get => _spawnPoint; set { _spawnPoint = value; } }

    private void Start()
    {
        // create pool of projectiles
        _pool = new ObjectPool<GameObject>(
            () =>
            {
                // instantiate prefab
                GameObject prefab = Instantiate(_projectilePrefab);
                // assign return to pool function to projectile class
                prefab.GetComponent<Projectile>().Init(ReturnToPool);
                // return gameobject to pool
                return prefab;
            },
            (gameObject) =>
            {
                gameObject.transform.position = _spawnPoint.position;
                gameObject.transform.rotation = _spawnPoint.rotation;
                gameObject.SetActive(true);
            },
            (gameObject) =>
            {
                gameObject.SetActive(false);
            },
            (gameObject) =>
            {
                Destroy(gameObject.gameObject);
            }, false, 20, 80);
    }

    public void StartFiring() {
        _isFiring = true;
        if (_firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(ContinuousFire());
        }
    }
    public void StopFiring() {
        _isFiring = false;
    }

    IEnumerator ContinuousFire()
    {
        while (IsFiring)
        {
            // fire projectile            
            FireFromSpawnPoint(SpawnPoint);
            // wait
            yield return new WaitForSeconds(FireSpeed);
        }

        _firingCoroutine = null;
    }

    private void ReturnToPool(GameObject item)
    {
        _pool.Release(item);
    }

    private void FireFromSpawnPoint(Transform spawnPoint)
    {
        GameObject projectile = _pool.Get();        
    }
}
