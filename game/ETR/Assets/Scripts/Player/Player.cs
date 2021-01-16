using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Stats
{
    [SerializeField] StatCoefficients stco;
    [SerializeField] CostCoefficients coco;

    public override void OnStatChanged()
    {
        base.OnStatChanged();
        Invoke(nameof(UpdateStats), .1f);
    }
    void UpdateStats()
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

        // Player Effects
        GetComponent<PlayerMotor>().SetSpeed(speed.GetValue());
    }

}
