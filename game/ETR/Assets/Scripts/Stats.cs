using System;
using UnityEngine;

[ExecuteInEditMode]
public class Stats : MonoBehaviour
{
    [SerializeField] StatCoefficients stco;
    [SerializeField] CostCoefficients coco;

    new public string name;
    public InteractableTypes InteractableType;
    void Awake()
    {
        SetStats();
        OnStatChanged();
    }
    void SetStats()
    {
        health.temp = this;
        energy.temp = this;
        speed.temp = this;
        ispos.temp = this;
        damage.temp = this;
        attackRange.temp = this;
        sightRange.temp = this;
        age.temp = this;
        birthDay.temp = this;
    }

    #region PlayerStats
    #region Stats
    [Header("Stats")]
    public float power;
    public Stat health;
    public Stat energy;
    public float starvingAmount;
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

    #region Costs

    [Header("Enery Costs")]
    public float starvingCost;
    public float walkingCost;
    public float attackingCost;

    [Header("Health Costs")]
    public float tiringCost;

    [Header("Speed Costs")]
    public float nightCost;
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

    public void OnStatChanged()
    {
        Invoke(nameof(UpdateStats), .1f);
    }
    public void UpdateStats()
    {
        power = speed.GetMaxValue() * stco.Speed +
                       health.GetMaxValue() * stco.Health +
                       energy.GetMaxValue() * stco.Energy +
                       damage.GetMaxValue() * stco.Damage +
                       ispos.GetMaxValue() * stco.Ispos +
                       attackRange.GetMaxValue() * stco.AttackRange +
                       sightRange.GetMaxValue() * stco.SightRange;

        // STATS
        starvingAmount = power * stco.StarvingViaPower;
        walkPointRange = sightRange.GetMaxValue() * stco.WalkPointRange;
        transform.localScale = Vector3.one * Mathf.Sqrt(power * stco.SizeViaPower);

        // COSTS
        starvingCost = power * coco.Starving;
        walkingCost = power * coco.Walking;
        attackingCost = power * coco.Attacking;
        tiringCost = power * coco.Tiring;
        nightCost = power * coco.Night;
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
