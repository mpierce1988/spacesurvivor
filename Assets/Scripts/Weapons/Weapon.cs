using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{

    // Fields
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected Transform projectileHierarchyParent;
    [SerializeField]
    protected float fireSpeed;

    public GameObjectPool projectilePool;


    [SerializeField]
    protected Transform spawnPoint;

    protected Coroutine firingCoroutine;
    protected bool isFiring;

    //protected ObjectPool<GameObject> _pool;

    public GameObject ProjectilePrefab { get => projectilePrefab; set { projectilePrefab = value; } }
    public float FireSpeed { get => fireSpeed; set { fireSpeed = value; } }
    public bool IsFiring { get => isFiring; set { isFiring = value; } }
    public Transform SpawnPoint { get => spawnPoint; set { spawnPoint = value; } }

    private void Start()
    {
        // create pool of projectiles
        /*_pool = new ObjectPool<GameObject>(
            () =>
            {
                // instantiate prefab
                GameObject prefab = Instantiate(projectilePrefab);
                // assign return to pool function to projectile class
                prefab.GetComponent<iPoolable>().SetReturnToPoolAction(ReturnToPool);
                // set parent transform
                prefab.transform.parent = projectileHierarchyParent;
                // return gameobject to pool
                return prefab;
            },
            (gameObject) =>
            {
                gameObject.transform.position = spawnPoint.position;
                gameObject.transform.rotation = spawnPoint.rotation;
                gameObject.SetActive(true);
            },
            (gameObject) =>
            {
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                gameObject.SetActive(false);
            },
            (gameObject) =>
            {
                Destroy(gameObject.gameObject);
            }, false, 20, 80);*/
    }

    public void StartFiring() {
        isFiring = true;
        if (firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(ContinuousFire());
        }
    }
    public void StopFiring() {
        isFiring = false;
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

        firingCoroutine = null;
    }

    /*private void ReturnToPool(GameObject item)
    {
        _pool.Release(item);
    }*/

    private void FireFromSpawnPoint(Transform spawnPoint)
    {
        GameObject projGameObject = projectilePool.GetItem();
        projGameObject.transform.position = spawnPoint.position;
        projGameObject.transform.rotation = spawnPoint.rotation;

        iProjectile proj = projGameObject.GetComponent<iProjectile>();
        proj.Launch(spawnPoint.transform.right);
    }
}
