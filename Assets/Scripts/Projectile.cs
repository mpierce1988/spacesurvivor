using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float force;
    [SerializeField]
    private float returnToPoolAfterSeconds;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private int damage = 1;

    private Rigidbody2D rb2D;
    private Action<GameObject> returnToPoolAction;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            
            // get health component
            Health targetHealth = collision.gameObject.GetComponent<Health>();

            // damage target
            targetHealth.TakeDamage(damage);

            // disable/return projectile to pool
            ReturnToPool();
        }
    }

    public void Init(Action<GameObject> returnToPoolAction)
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

    private void Launch()
    {        
        rb2D.AddForce(transform.right * force, ForceMode2D.Impulse);
        StartCoroutine(ReturnAfterDelay(returnToPoolAfterSeconds));
    }
}
