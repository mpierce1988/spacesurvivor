using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(GameObjectPool))]
public class Weapon : MonoBehaviour
{

    // Fields
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected Transform projectileHierarchyParent;
    [SerializeField]
    protected float fireSpeed;

    protected GameObjectPool projectilePool;


    [SerializeField]
    protected Transform spawnPoint;

    protected Coroutine firingCoroutine;
    protected bool isFiring;    

    public GameObject ProjectilePrefab { get => projectilePrefab; set { projectilePrefab = value; } }
    public float FireSpeed { get => fireSpeed; set { fireSpeed = value; } }
    public bool IsFiring { get => isFiring; set { isFiring = value; } }
    public Transform SpawnPoint { get => spawnPoint; set { spawnPoint = value; } }

    private void Awake()
    {
        projectilePool = GetComponent<GameObjectPool>();
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

    private void FireFromSpawnPoint(Transform spawnPoint)
    {
        GameObject projGameObject = projectilePool.GetItem();
        projGameObject.transform.position = spawnPoint.position;
        projGameObject.transform.rotation = spawnPoint.rotation;

        iProjectile proj = projGameObject.GetComponent<iProjectile>();
        proj.Launch(spawnPoint.transform.right);
    }
}
