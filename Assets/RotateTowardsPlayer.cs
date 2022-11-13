using UnityEngine;

public class RotateTowardsPlayer : RotateTowardsTarget
{    

    private void Start()
    {
        _target = FindObjectOfType<Player>().gameObject;
    }
}
