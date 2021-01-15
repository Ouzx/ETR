using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    private void Awake()
    {
        walkPointRange = sightRange.GetMaxValue() * .8f;
    }
    new public string name;
    public InteractableTypes InteractableType;
    #region Stats
    [Header("Stats")]
    public Stat health;
    public Stat energy;
    public float power;
    public Stat starvingAmount;
    #endregion

    #region  Actions
    [Header("Abilities")]
    public Stat speed;
    public Stat ispos; // Interaction speed per one second
    public Stat damage;
    // public float biteAmount;
    #endregion

    #region Ranges
    [Header("Ranges")]
    public Stat attackRange;
    public Stat sightRange;
    public float walkPointRange;
    #endregion

    #region Costs

    [Header("Enery Costs")]
    public Stat starvingCost;
    public Stat walkingCost;
    public Stat AttackingCost;

    [Header("Health Costs")]
    public Stat TiringCost;

    [Header("Speed Costs")]
    public Stat NightCost;
    #endregion

    #region INFO
    [Header("INFO")]
    public int childCount;
    public int gen;
    public int killCount;

    public Stat age;
    public Stat birthDay;
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
