using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Stats
{

    [SerializeField] StatCoefficients stco;
    [SerializeField] CostCoefficients coco;

    #region OnStatChanged
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
        starvingAmount.SetMaxValue(power * stco.StarvingViaPower, true);
        walkPointRange = sightRange.GetMaxValue() * stco.WalkPointRange;
        transform.localScale = Vector3.one * Mathf.Sqrt(power * stco.SizeViaPower);

        // COSTS
        starvingCost = power * coco.Starving;
        walkingCost = power * coco.Walking;
        attackingCost = power * coco.Attacking;
        tiringCost = power * coco.Tiring;
        nightCost = coco.Night;

        // Player Effects
        GetComponent<PlayerMotor>().SetSpeed(speed.GetValue());
    }
    #endregion
    void Awake()
    {
        Clock.instance.OnMorning += OnMorning;
        Clock.instance.OnEvening += OnEvening;
    }
    private void Update()
    {
        Debug.Log(Clock.instance.dayTime);
    }
    bool nerf = false;
    void OnMorning()
    {
        if (nerf) DeBuff(false);
        Debug.LogWarning("Normal speed: " + speed.GetValue());
        isHungry = true;
    }
    void OnEvening()
    {
        // Debuff player stats
        DeBuff(true);
        Debug.LogWarning("Speed: " + speed.GetValue());
    }

    // Decrease sight range and speed
    void DeBuff(bool isNight)
    {
        sbyte deBuffMultiplier = 1;
        if (!isNight) deBuffMultiplier = -1;

        speed.SetValue(speed.GetValue() + nightCost * deBuffMultiplier);
        sightRange.SetMaxValue(sightRange.GetMaxValue() + nightCost * deBuffMultiplier);
        nerf = true;
    }
}
