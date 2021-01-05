using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    void Start()
    {

    }

    #region Attack
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    private void AttackPlayer(Transform target)
    {
        // Make sure enemey doesnt move
        // agent.SetDestination(transform.position);
        // transform.LookAt(target);
        // if (!alreadyAttacked)
        // {
        //     Debug.Log("Attacked");
        //     alreadyAttacked = true;
        //     Invoke(nameof(ResetAttack), timeBetweenAttacks);
        // }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Eating
    private void Eat(Transform food)
    {
        // agent.SetDestination(transform.position);
        // transform.LookAt(food);
        // foodName = food.name;
        // food.GetComponent<Food>().Eat(biteAmount);
    }
    #endregion
}
