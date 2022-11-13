using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateTowardsTarget : MonoBehaviour
{
    // Start is called before the first frame update
    protected GameObject _target;

    [SerializeField] protected float _rotateSpeed;
    [SerializeField] protected float _rotationThreshold;

    protected Quaternion q;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Rotate();
    }

    protected void Rotate()
    {
        if(_target == null)
        {
            return;
        }

        q = GetLookRotation();
        if (IsTargetOutsideRotationThreshold())
        {
            SetRotation();
        }
    }

    protected bool IsTargetOutsideRotationThreshold()
    {
        return Quaternion.ToEulerAngles(q).magnitude > _rotationThreshold;
    }

    protected Quaternion GetLookRotation()
    {
        return Quaternion.LookRotation(_target.transform.position - transform.position);
    }

    protected void SetRotation()
    {
        Debug.Log("Setting new rotation for " + gameObject.name + " at " + (Quaternion.RotateTowards(transform.rotation, q, _rotateSpeed * Time.fixedDeltaTime)));
        _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, q, _rotateSpeed * Time.fixedDeltaTime));
    }
    
}
