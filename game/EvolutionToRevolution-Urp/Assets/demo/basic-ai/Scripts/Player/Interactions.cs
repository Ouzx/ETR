using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    Player player;
    Movement movement;
    void Start()
    {
        player = GetComponent<Player>();
        movement = GetComponent<Movement>();
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
    // keep in mind last food
    public void Eat(Transform food)
    {
        // movement.Move(food.position);
        // transform.LookAt(food);
        player.foodName = food.name;
        food.GetComponent<Food>().Eat(player.biteAmount);

    }
    #endregion
}
