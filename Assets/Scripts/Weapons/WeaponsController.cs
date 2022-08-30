using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField]
    private List<Weapon> _activeWeapons;
    // Start is called before the first frame update
    void Start()
    {
        FireAllWeapons();
    }

    void FireAllWeapons()
    {
        for (int i = 0; i < _activeWeapons.Count; i++)
        {
            _activeWeapons[i].StartFiring();
        }
    }
}
