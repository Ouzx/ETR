using System.Reflection;
using UnityEngine;

public class Player : Stats
{
    [SerializeField] StatCoefficients stco;
    [SerializeField] CostCoefficients coco;

    void Awake()
    {
        SetStats();
        OnStatChanged();
        Clock.instance.OnMorning += OnMorning;
        Clock.instance.OnEvening += OnEvening;
        Clock.instance.OnSecond += OnSecondChanged;
    }

    void SetStats()
    {
        // This method, iterates over all Stat fields of this instance.
        // and point this class to player.temp class
        FieldInfo[] fields = typeof(Player).GetFields();
        foreach (FieldInfo _field in fields)
        {
            if (_field.FieldType == typeof(Stat))
            {
                FieldInfo field = typeof(Stat).GetField("temp");
                Player temp = (Player)field.GetValue(_field.GetValue(this));
                temp = this;

                Stat _stat = (Stat)_field.GetValue(this);
                _stat.SetValue(_stat.GetMaxValue());
            }
        }
    }

    #region Actions and Costs
    public void GetDamage(float amount)
    {
        float tempAmont = health.GetValue() - amount;
        if (tempAmont > 0)
        {
            health.SetValue(tempAmont);
        }
        else
        {
            DestroyMe();
        }
    }

    public void GetTired(float amount)
    {
        float tempAmont = energy.GetValue() - amount;
        if (tempAmont > 0)
        {
            energy.SetValue(tempAmont);
        }
        else
        {
            GetDamage(tiringCost);
        }
    }

    public void Attack()
    {
        // Attack from Interactable not here.
        GetTired(attackingCost);
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    #endregion

    #region OnStatChanged
    public void OnStatChanged()
    {
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
        starvingCost = coco.Starving;
        walkingCost = power * coco.Walking;
        attackingCost = power * coco.Attacking;
        tiringCost = power * coco.Tiring;
        nightCost = coco.Night;

        // Player Effects
        GetComponent<PlayerMotor>().SetSpeed(speed.GetValue());
    }
    #endregion

    #region OnDayChanged
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
    #endregion

    #region OnSecondChanged
    void OnSecondChanged()
    {
        if (isHungry) GetTired(starvingAmount.GetValue());
    }
    #endregion

}
