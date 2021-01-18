using System;
using UnityEngine;
public class Stats : MonoBehaviour
{
    new public string name;
    public InteractableTypes InteractableType;

    #region PlayerStats
    #region Stats
    [Header("Stats")]
    public float power;
    public Stat health;
    public Stat energy;
    public Stat starvingAmount;
    public bool isHungry;
    #endregion

    #region  Actions
    [Header("Abilities")]
    public Stat speed;
    public Stat ispos; // Interaction speed per one second
    public Stat damage;
    #endregion

    #region Ranges
    [Header("Ranges")]
    public float walkPointRange;
    public Stat attackRange;
    public Stat sightRange;
    #endregion

    #region Regen
    [Header("Regen")]
    public float healthRegen;
    public float energyRegen;
    #endregion

    #region Costs

    [Header("Enery Costs")]
    public float starvingCost;
    public float walkingCost;
    public float attackingCost;

    [Header("Health Costs")]
    public float tringCost;

    [Header("Speed Costs")]
    public float nightCost;
    #endregion

    #region INFO
    [Header("INFO")]
    public int childCount;
    public int gen;
    public int killCount;

    public int age;
    public int birthDay;
    #endregion

    #endregion

    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, walkPointRange);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, sightRange.GetMaxValue());
        UnityEditor.Handles.color = Color.black;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, attackRange.GetMaxValue());
    }
}
