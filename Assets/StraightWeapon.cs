using System.Collections;
using UnityEngine;

public class StraightWeapon : Weapon
{
    // Fields
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private float _fireSpeed;
    
    
    [SerializeField]
    private Transform _spawnPoint;

    private Coroutine _firingCoroutine;
    private bool _isFiring;

    public override GameObject ProjectilePrefab { get => _projectilePrefab; set { _projectilePrefab = value; } }
    public override float FireSpeed { get => _fireSpeed; set { _fireSpeed = value; } }
    public override bool IsFiring { get => _isFiring; set { _isFiring = value; } }
    public override Transform SpawnPoint { get => _spawnPoint; set { _spawnPoint = value; } }

    public override void StartFiring()
    {
        _isFiring = true;
        if(_firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(ContinuousFire());
        }
    }

    public override void StopFiring()
    {
        _isFiring = false;
    }

    IEnumerator ContinuousFire()
    {
        while (IsFiring)
        {
            // fire projectile
            GameObject projectile = Instantiate(ProjectilePrefab);
            projectile.transform.position = SpawnPoint.position;
            projectile.transform.rotation = SpawnPoint.rotation;
            // wait
            yield return new WaitForSeconds(FireSpeed);
        }

        _firingCoroutine = null;
    }
}
