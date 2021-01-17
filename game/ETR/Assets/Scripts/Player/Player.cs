using System.Reflection;
using UnityEngine;

public class Player : Stats
{
    [SerializeField] StatCoefficients stco;
    [SerializeField] CostCoefficients coco;
    Animator animator;

    void Awake()
    {
        SetStats();
        OnStatChanged();
        Clock.instance.OnMorning += OnMorning;
        Clock.instance.OnEvening += OnEvening;
        Clock.instance.OnSecond += OnSecondChanged;
        animator = GetComponent<Animator>();
    }

    void OnDestroy()
    {
        Clock.instance.OnMorning -= OnMorning;
        Clock.instance.OnEvening -= OnEvening;
        Clock.instance.OnSecond -= OnSecondChanged;
    }

    #region Stats and Costs
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
        tringCost = power * coco.Tring;
        nightCost = coco.Night;

        // Player Effects
        GetComponent<PlayerMotor>().SetSpeed(speed.GetValue());
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
                _stat.Equalize();
            }
        }
    }

    #endregion

    #region Actions
    public void GetDamage(float amount)
    {
        float tempAmount = health.GetValue() + amount;
        if (tempAmount > 0)
        {
            health.SetValue(tempAmount);
        }
        else
        {
            DestroyMe();
        }
    }

    public void GetTired(float amount)
    {
        float tempAmount = energy.GetValue() + amount;
        if (tempAmount > 0)
        {
            energy.SetValue(tempAmount);
        }
        else
        {
            energy.SetValue(0f);
            GetDamage(tringCost);
        }
    }

    public void Attack()
    {
        // Attack from Interactable not here.
        GetTired(attackingCost);
    }

    public void Eat(float amount)
    {
        float tempAmount = starvingAmount.GetValue() - amount;
        if (tempAmount > 0)
        {
            starvingAmount.SetValue(tempAmount);
        }
        else
        {
            isHungry = false;
            animator.SetBool("isHungry", false);
            starvingAmount.SetValue(0);
            energy.SetValue(energy.GetValue() + tempAmount);
        }
    }
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    #endregion

    #region OnDayChanged
    bool nerf = false;
    void OnMorning()
    {
        if (nerf) DeBuff(false);
        isHungry = true;
        animator.SetBool("isHungry", true);
        starvingAmount.Equalize();
    }
    void OnEvening()
    {
        // Debuff player stats
        DeBuff(true);
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
        if (isHungry) GetTired(tringCost);
    }
    #endregion

}
