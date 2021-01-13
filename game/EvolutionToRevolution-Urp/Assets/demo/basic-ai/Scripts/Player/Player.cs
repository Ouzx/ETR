using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Interactions))]
public class Player : MonoBehaviour
{


    #region  Player Stats
    public enum PlayerType
    {
        Fred,
        Ivedo,
        Barney
    }
    public string playerName;
    public float maxHealth;
    [HideInInspector] public float health;
    public float maxEnergy;
    [HideInInspector] public float energy;
    public float speed;
    public float biteAmount;
    public float sightRange;
    public float attackRange;
    [HideInInspector] public float power;
    [HideInInspector] public int childCount;
    [HideInInspector] public float age;
    [HideInInspector] public float gen;
    [HideInInspector] public int killCount;
    [HideInInspector] public float birthDay;
    public PlayerType playerType;
    #endregion

    #region Costs

    #region Energy
    [SerializeField] private float walkingCost;
    [SerializeField] private float hitCost;
    [SerializeField] private float starveCost;
    #endregion

    #region Health
    [SerializeField] private float damgeTakenCost;
    [SerializeField] private float TiredCost;
    #endregion

    #region Speed
    [SerializeField] private float nightCost;
    #endregion

    #endregion

    #region  States
    [SerializeField] private bool isSightRange;
    [SerializeField] private bool isInteractionRange;
    [SerializeField] private bool isHungry;
    public bool isAtBase;

    [HideInInspector] public string foodName = "";
    #endregion

    #region UI
    private Slider healthBar;
    private Slider energyBar;
    #endregion

    #region Modules
    private Movement movement;
    private Interactions interactions;
    #endregion
    private void Start()
    {
        movement = GetComponent<Movement>();
        interactions = GetComponent<Interactions>();

        healthBar = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        energyBar = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        StartCoroutine(nameof(UpdateBars));

        health = maxHealth;
        energy = maxEnergy;

        TimeCycle.instance.OnSecondChanged += HealthRegen;
        TimeCycle.instance.OnSecondChanged += EnergyRegen;
        TimeCycle.instance.OnSecondChanged += AtBaseRegen;

        TimeCycle.instance.OnDawn += Dawn;
        TimeCycle.instance.OnSunSet += Sunset;
    }


    private void Update()
    {
        movement.SearchForFood();

        // Starve();
        // if (collisions.closestFood == null) Patrolling();
        // else Eat(collisions.closestFood);

        // Patrolling();
        // CheckColliders();

        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        // playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // if (!playerInSightRange && !playerInAttackRange) Patrolling();
        // if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        // if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
    private void Dawn()
    {
        isHungry = true;
        // Debuff false
    }
    private void Sunset()
    {
        // Debuff true
    }

    private void Starve()
    {
        if (isHungry) GetTired(starveCost);
    }
    private void Walk()
    {

    }
    private void HealthRegen()
    {
        // Debug.Log("Healed");
    }
    private void EnergyRegen()
    {
        // Debug.Log("Regenned Energy");
    }
    private void AtBaseRegen()
    {
        // Debug.Log("AtBaseRegen");
    }
    private void GetDamage(float cost)
    {
        health -= cost;
        // if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void GetTired(float cost)
    {
        // Range costs energy too
        float tempCost = energy - cost;
        if (energy <= 0)
        {
            energy = 0;
            GetDamage(-tempCost);
        }
        else
        {
            energy = tempCost;
        }
    }

    private IEnumerator UpdateBars()
    {
        while (true)
        {
            energyBar.value = energy / maxEnergy;
            healthBar.value = health / maxEnergy;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, sightRange);
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, attackRange);
    }

}
