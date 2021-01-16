using System;
using UnityEngine;

[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    [SerializeField] StatCoefficients stco;
    new public string name;
    public InteractableTypes InteractableType;
    void Awake()
    {
        SetStats();
        OnStatChanged();
    }
    #region PlayerStats
    #region Stats
    [Header("Stats")]
    public float power;
    public Stat health;
    public Stat energy;
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
    public float walkPointRange;
    public Stat attackRange;
    public Stat sightRange;
    #endregion

    #region Costs

    [Header("Enery Costs")]
    public Stat starvingCost;
    public Stat walkingCost;
    public Stat attackingCost;

    [Header("Health Costs")]
    public Stat tiringCost;

    [Header("Speed Costs")]
    public Stat nightCost;
    #endregion

    #region INFO
    [Header("INFO")]
    public int childCount;
    public int gen;
    public int killCount;

    public Stat age;
    public Stat birthDay;
    #endregion

    #endregion
    void SetStats()
    {
        health.temp = this;
        energy.temp = this;
        starvingAmount.temp = this;
        speed.temp = this;
        ispos.temp = this;
        damage.temp = this;
        attackRange.temp = this;
        sightRange.temp = this;
        starvingCost.temp = this;
        walkingCost.temp = this;
        attackingCost.temp = this;
        tiringCost.temp = this;
        nightCost.temp = this;
        age.temp = this;
        birthDay.temp = this;
    }

    public void OnStatChanged()
    {
        power = speed.GetMaxValue() * stco.SPEED +
                health.GetMaxValue() * stco.HEALTH +
                energy.GetMaxValue() * stco.ENERGY +
                damage.GetMaxValue() * stco.DAMAGE +
                ispos.GetMaxValue() * stco.ISPOS +
                attackRange.GetMaxValue() * stco.ATTACK_RANGE +
                sightRange.GetMaxValue() * stco.SIGHT_RANGE;
        walkPointRange = sightRange.GetMaxValue() * stco.WALKPOINT_RANGE;
    }
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
