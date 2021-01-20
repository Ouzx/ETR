using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Player : Stats
{
    [SerializeField] StatCoefficients stco;
    [SerializeField] CostCoefficients coco;
    public int epOnMorning;
    PlayerController playerController;
    void Awake()
    {
        SetStats();
        OnStatChanged();
    }
    void Start()
    {
        Clock.instance.OnMorning += OnMorning;
        Clock.instance.OnEvening += OnEvening;
        Clock.instance.OnSecond += OnSecondChanged;
        name = NameGenerator.instance.GetName();
        nameText.text = name;
        birthDay = Clock.instance.day;
        InvokeRepeating(nameof(UpdateBars), .1f, .2f);
        playerController = GetComponent<PlayerController>();
    }
    public Slider healthBar, energyBar;
    public Text nameText;
    void UpdateBars()
    {
        energyBar.value = energy.GetValue() / energy.GetMaxValue();
        healthBar.value = health.GetValue() / health.GetMaxValue();
    }
    void OnDestroy()
    {
        CancelInvoke();
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

        // Rangers();
        // Player Effects
            GetComponent<PlayerMotor>().SetSpeed(speed.GetValue());
    }
    // void Rangers()
    // {
    //     var go1 = new GameObject { name = "Circle" };
    //     go1.transform.position = new Vector3(transform.position.x, 1, transform.position.z);

    //     go1.DrawCircle(1, .02f);
    // }
    public void SetStats()
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
            starvingAmount.SetValue(0);
            energy.SetValue(energy.GetValue() + tempAmount);
        }
    }
    public void DestroyMe()
    {
        GameObject.Destroy(gameObject, 0.2f);
    }

    #endregion

    #region OnDayChanged
    bool nerf = false;
    bool isNight = true;
    bool alreadyReproduced = true;
    public void OnMorning()
    {
        isNight = false;
        // Check: if player is not hungry: reproduce
        if (!alreadyReproduced && !isHungry)
        {
            Reproduce();
            alreadyReproduced = true;
        }
        else
        {
            alreadyReproduced = false;
        }
        if (nerf) DeBuff();
        GameManager.instance.EarnEP(epOnMorning, InteractableType);
        age++;
        isHungry = true;
        starvingAmount.SetValue(starvingAmount.GetValue() + starvingAmount.GetMaxValue());
    }

    public void OnEvening()
    {
        isNight = true;
        // Debuff player stats
        DeBuff();
    }

    // Decrease sight range and speed
    void DeBuff()
    {
        sbyte deBuffMultiplier = 1;
        if (!isNight) deBuffMultiplier = -1;

        speed.SetValue(speed.GetValue() + nightCost * deBuffMultiplier);
        sightRange.SetMaxValue(sightRange.GetMaxValue() + nightCost * deBuffMultiplier);
        nerf = true;
    }
    #endregion

    #region OnSecondChanged
    public void OnSecondChanged()
    {
        if (isHungry) GetTired(tringCost);
        else if (playerController.isAtBase)
        {
            HealthRegen();
            EnergyRegen();
        }
    }
    #endregion

    #region Regen
    void HealthRegen()
    {
        if (health.GetValue() + healthRegen <= health.GetMaxValue()) health.AppendValue(healthRegen);
    }
    void EnergyRegen()
    {
        if (energy.GetValue() + energyRegen <= energy.GetMaxValue()) energy.AppendValue(energyRegen);
    }
    #endregion

    #region Reproduce
    void Reproduce()
    {
        GameObject child = Instantiate(gameObject, playerController.motor.RandomPointInBase(3), Quaternion.identity, transform.parent);
        child.GetComponent<Player>().isHungry = true;
        childCount++;
    }
    #endregion
}
