using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, iProjectile, iPoolable
{
    [SerializeField]
    private float force;
    [SerializeField]
    private float returnToPoolAfterSeconds;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private bool launchOnEnable = false;

    private Rigidbody2D rb2D;
    private Action<GameObject> returnToPoolAction;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if(launchOnEnable) Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       

        if ((targetLayer.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            
            // get health component
            iTakeDamage target = collision.gameObject.GetComponent<iTakeDamage>();

            // damage target
            target.TakeDamage(damage);

            // disable/return projectile to pool
            ReturnToPool();
        }
    }

    public void SetReturnToPoolAction(Action<GameObject> returnToPoolAction)
    {
        this.returnToPoolAction = returnToPoolAction;
    }

    IEnumerator ReturnAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        // stop projectile from moving
        rb2D.velocity = Vector2.zero;

        // if there is a pool, return to it
        if(returnToPoolAction != null)
        {
            returnToPoolAction(this.gameObject);
        } 
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch()
    {
        Launch(transform.right);
    }

    public void Launch(Vector3 direction)
    {
        rb2D.AddForce(direction * force, ForceMode2D.Impulse);
        StartCoroutine(ReturnAfterDelay(returnToPoolAfterSeconds));
    }


}
