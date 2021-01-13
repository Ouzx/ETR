using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{

    private Player player;
    private Interactions interactions;
    void Start()
    {
        player = GetComponent<Player>();
        interactions = GetComponent<Interactions>();
        StartCoroutine(nameof(CheckColliders));
        StartCoroutine(nameof(SearchForFood));
        StartCoroutine(nameof(Eat));
    }
    #region NavMesh
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer, whatIsFood;
    [SerializeField] private float interactionRange;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    #endregion

    #region Collisions
    private class Collisions
    {
        public Collisions()
        {
            foods = new List<Transform>();
            enemies = new List<Transform>();
            nearestFood = null;
            nearestEnemy = null;

        }
        public List<Transform> foods { get; set; }
        public List<Transform> enemies { get; set; }
        public Transform nearestFood { get; set; }
        public Transform nearestEnemy { get; set; }
    }
    [SerializeField] private Collisions collisions = new Collisions();

    private IEnumerator CheckColliders()
    {
        while (true)
        {

            Collider[] objects = Physics.OverlapSphere(transform.position, player.sightRange);
            Collisions _collisions = new Collisions();
            if (!player.isAtBase)
            {
                foreach (Collider col in objects)
                {
                    if (col.tag == "Food")
                    {
                        _collisions.foods.Add(col.transform);
                        if (_collisions.nearestFood == null) _collisions.nearestFood = col.transform;
                        else if (Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, _collisions.nearestFood.transform.position)) _collisions.nearestFood = col.transform;
                    }
                    else if (col.tag == "Player")
                    {
                        if (col.transform.GetComponent<Player>().playerType != player.playerType)
                        {
                            _collisions.enemies.Add(col.transform);
                            if (_collisions.nearestEnemy == null) _collisions.nearestEnemy = col.transform;
                            else if (Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, _collisions.nearestEnemy.position)) _collisions.nearestEnemy = col.transform;
                        }
                    }
                }
            }
            collisions = _collisions;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    #endregion

    #region Movement
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    public IEnumerator SearchForFood()
    {
        while (true)
        {
            if (collisions.nearestFood == null) Patrol();
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // else
        // {
        //     MoveTo(collisions.nearestFood.transform.position / 10);
        //     Debug.Log("HAM HAM HAM");
        //     interactions.Eat(collisions.nearestFood.transform);
        // }
    }
    public void SearchForEnemy()
    {
        // check strongness
        if (collisions.nearestEnemy != null) Escape();
        // or chase
    }
    public void Patrol()
    {
        if (!walkPointSet) GenerateWalkPoint();
        else MoveAgent(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    public IEnumerator Eat()
    {
        while (true)
        {
            if (collisions.nearestFood != null)
            {
                agent.isStopped = true;
                interactions.Eat(collisions.nearestFood.transform);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private void Escape()
    {

    }

    public void ToBase()
    {

    }

    private void GenerateWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        walkPointSet = Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround);
    }

    public void MoveTo(Vector3 target)
    {


    }

    private void MoveAgent(Vector3 target)
    {
        if (collisions.nearestEnemy == null)
        {
            agent.SetDestination(target);
        }
        else
        {
            // Escape();
            // Fight();
        }

        // GetTired(walkingCost);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, walkPointRange);
    }

}
