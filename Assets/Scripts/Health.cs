using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, iTakeDamage
{
    [SerializeField]
    private int startingHealth;    

    private int currentHealth;

    public UnityEvent OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
            // trigger death
            OnDeath.Invoke();
        }
        Debug.Log($"Current Health of {this.gameObject.name} After Damage: {currentHealth}");
    }
}
