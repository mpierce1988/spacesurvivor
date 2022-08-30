using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    private EnemyState _currentState;
    public Rigidbody2D Rigidbody2D;
    public Player Player;
    public float TickSpeedInSeconds = 1f;
    public float ChaseDistance = 10f;
    public float AttackDistance = 1f;
    public float Speed = 2f;

    private float _tickInterval = 0;
    private Coroutine _tickCoroutine;

    private void Start()
    {
        if(Player == null)
        Player = FindObjectOfType<Player>();

        if (Rigidbody2D == null) Rigidbody2D = GetComponent<Rigidbody2D>();

        // calculate tick interval
        _tickInterval = 1f / TickSpeedInSeconds;

        // set default state
        SwitchToState(new EnemyIdleState());

        // start tick coroutine
        _tickCoroutine = StartCoroutine(TickCoroutine());
    }

    public void SwitchToState(EnemyState enemyState)
    {
        if(_currentState != null)
        {
            StartCoroutine(_currentState.ExitState(this));
        }

        _currentState = enemyState;
        StartCoroutine(_currentState.EnterState(this));
    }

    IEnumerator TickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(TickSpeedInSeconds);
            _currentState.Tick(this);
        }
    }
}

public abstract class EnemyState
{
    public abstract IEnumerator EnterState(Enemy enemy);
    public abstract IEnumerator ExitState(Enemy enemy);

    public abstract void Tick(Enemy enemy);

   
}

public class EnemyIdleState : EnemyState
{
    public override IEnumerator EnterState(Enemy enemy)
    {
        Debug.Log("Enemy entered Idle State...");
        // stop ship
        enemy.Rigidbody2D.velocity = Vector2.zero;
        yield return null;
    }

    public override IEnumerator ExitState(Enemy enemy)
    {
        // do nothing
        Debug.Log("Enemy exited Idle State...");
        yield return null;
        
    }

    public override void Tick(Enemy enemy)
    {
        // check if player is within chase distance
        if(IsPlayerWithinChaseDistance(enemy))
        {
            // switch to chasing player state
            enemy.SwitchToState(new EnemyChasingState());
        }
    }

    bool IsPlayerWithinChaseDistance(Enemy enemy)
    {
        return Mathf.Abs((enemy.Player.transform.position - enemy.transform.position).magnitude) < enemy.ChaseDistance;
    }
}

public class EnemyChasingState : EnemyState
{
    public override IEnumerator EnterState(Enemy enemy)
    {
        Debug.Log("Enemy entered Chase State...");
        yield return null;
    }

    public override IEnumerator ExitState(Enemy enemy)
    {
        Debug.Log("Enemy exited Chase State...");
        yield return null;
    }

    public override void Tick(Enemy enemy)
    {
        Debug.Log("Enemy Chase Tick...");
        // if player is outside of chase range, stop chasing
        if (!IsPlayerWithinChaseDistance(enemy))
        {
            Debug.Log("Enemy is NOT within chase distance");   
            // switch to idle state
            enemy.SwitchToState(new EnemyIdleState());
            return;
        }

        // if player is within attack range, start attacking
        if (IsPlayerWithinAttackDistance(enemy))
        {
            // switch to attacking state
            enemy.SwitchToState(new EnemyAttackingState());
            return;
        }

        // move towards player
        Vector3 movementDirection = (enemy.Player.transform.position - enemy.transform.position).normalized;
        enemy.Rigidbody2D.velocity = Vector2.zero;
        enemy.Rigidbody2D.AddForce(movementDirection * enemy.Speed);
    }

    bool IsPlayerWithinChaseDistance(Enemy enemy)
    {
        return Mathf.Abs((enemy.Player.transform.position - enemy.transform.position).magnitude) < enemy.ChaseDistance;
    }

    bool IsPlayerWithinAttackDistance(Enemy enemy)
    {
        return Mathf.Abs((enemy.Player.transform.position - enemy.transform.position).magnitude) < enemy.AttackDistance;
    }
}

public class EnemyAttackingState : EnemyState
{
    public override IEnumerator EnterState(Enemy enemy)
    {
        Debug.Log("Enemy entered Attacking State...");
        // stop ship
        enemy.Rigidbody2D.velocity = Vector2.zero;
        yield return null;
    }

    public override IEnumerator ExitState(Enemy enemy)
    {
        Debug.Log("Enemy exited Attacking State...");
        yield return null;
    }

    public override void Tick(Enemy enemy)
    {
        if (!IsPlayerWithinAttackDistance(enemy))
        {
            // switch to chasing
            enemy.SwitchToState(new EnemyChasingState());
        }
        else
        {
            Debug.Log("Enemy is attacking...");
        }
        
    }

    bool IsPlayerWithinAttackDistance(Enemy enemy)
    {
        return Mathf.Abs((enemy.Player.transform.position - enemy.transform.position).magnitude) < enemy.AttackDistance;
    }
}
