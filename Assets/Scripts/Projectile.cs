using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _force;
    [SerializeField]
    private float _returnToPoolAfterSeconds;

    private Rigidbody2D _rb2D;
    private Action<GameObject> _returnToPoolAction;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Launch();
    }

    public void Init(Action<GameObject> returnToPoolAction)
    {
        _returnToPoolAction = returnToPoolAction;
    }

    IEnumerator ReturnAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        // stop projectile from moving
        _rb2D.velocity = Vector2.zero;

        // if there is a pool, return to it
        if(_returnToPoolAction != null)
        {
            _returnToPoolAction(this.gameObject);
        } 
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Launch()
    {        
        _rb2D.AddForce(transform.right * _force, ForceMode2D.Impulse);
        StartCoroutine(ReturnAfterDelay(_returnToPoolAfterSeconds));
    }
}
