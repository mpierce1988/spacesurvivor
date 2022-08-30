using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract GameObject ProjectilePrefab { get; set; }
    public abstract float FireSpeed { get; set; }
    public abstract bool IsFiring { get; set; }

    public abstract Transform SpawnPoint { get; set; }
    public abstract void StartFiring();
    public abstract void StopFiring();
}
