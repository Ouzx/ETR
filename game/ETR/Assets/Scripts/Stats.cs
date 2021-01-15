using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Stats : MonoBehaviour
{

    #region Stats
    [Header("Stats")]
    new public string name;
    public InteractableTypes InteractableType;
    public Stat health;
    public Stat energy;
    public Stat power;
    public float starvingAmount;
    #endregion

    #region  Actions
    [Header("Abilities")]
    public float speed;
    public float interactionSpeed; // Interaction speed per one second
    public float damage;
    // public Stat biteAmount;
    #endregion

    #region Ranges
    [Header("Ranges")]
    public float attackRange;
    public float sightRange;
    public float walkPointRange;
    #endregion

    #region Costs

    [Header("Enery Costs")]
    public float starvingCost;
    public float walkingCost;
    public float AttackingCost;

    [Header("Health Costs")]
    public float TiringCost;


    [Header("Speed Costs")]
    public float NightCost;
    #endregion

    #region INFO
    [Header("INFO")]
    public int childCount;
    public int gen;
    public float age;
    public int killCount;
    public float birthDay;
    #endregion

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, walkPointRange);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, sightRange);
        UnityEditor.Handles.color = Color.black;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, attackRange);
    }
}
