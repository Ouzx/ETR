using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Player player;
    public Interactable focus;
    Interactable me;

    PlayerMotor motor;

    // Collisions
    public Transform nearestFood, nearestEnemy;
    Transform _nearestFood, _nearestEnemy;


    void Start()
    {
        me = GetComponent<Interactable>();
        motor = GetComponent<PlayerMotor>();
        StartCoroutine(nameof(TriggerChecker));
    }

    IEnumerator TriggerChecker()
    {
        while (true)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, player.sightRange.GetMaxValue(), motor.PlayerLayer, QueryTriggerInteraction.Ignore);
            Collider[] foods = Physics.OverlapSphere(transform.position, player.sightRange.GetMaxValue(), motor.FoodLayer, QueryTriggerInteraction.Ignore);
            if (enemies.Length == 0) { nearestEnemy = null; _nearestEnemy = null; }
            else
            {
                foreach (Collider col in enemies)
                {
                    if (col.gameObject != gameObject) // Check for player type before checking collisions
                        if (_nearestEnemy == null) _nearestEnemy = col.transform;
                        else if (Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, _nearestEnemy.position)) _nearestEnemy = col.transform;
                }
            }

            if (foods.Length == 0) { nearestFood = null; _nearestFood = null; }
            else
            {
                foreach (Collider col in foods)
                {
                    if (_nearestFood == null) _nearestFood = col.transform;
                    else if (Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, _nearestFood.position)) _nearestFood = col.transform;
                }
            }
            nearestEnemy = _nearestEnemy;
            nearestFood = _nearestFood;
            yield return new WaitForSeconds(0.1f); // you can change it if player acts laggy
        }
    }

    public bool isAtBase;
    void Update()
    {
        Debug.Log(motor.agent.destination);
        isAtBase = motor.isAtBase();
        // If player at base but can see Enemy, attack!
        if (nearestEnemy != null) { SetFocus(nearestEnemy.GetComponent<Interactable>()); } // Attack or Escape
        if (player.isHungry)
        {
            if (nearestFood != null) { SetFocus(nearestFood.GetComponent<Interactable>()); } // Eat
            else { RemoveFocus(); motor.Patrol(); } // Patrol
        }
        else if (!isAtBase)
        {
            RemoveFocus();
            motor.ToBase();
        }
        else
        {
            RemoveFocus();
            motor.GoBase();
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            // motor.agent.isStopped = true;
            // motor.agent.isStopped = false;
            motor.agent.SetDestination(transform.position);
            if (focus != null)
                me.OnDefocused();
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        me.OnFocused(newFocus);

    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus = null;
            me.OnDefocused();
        }
        motor.StopFollowingTarget();
    }

}
