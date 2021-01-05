using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{

    private Player player;
    void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(nameof(CheckColliders));
    }
    #region NavMesh
    private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer, whatIsFood;
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
            ivedos = new List<Transform>();
            freds = new List<Transform>();
            barnies = new List<Transform>();
        }
        public List<Transform> foods { get; set; }
        public List<Transform> ivedos { get; set; }
        public List<Transform> freds { get; set; }
        public List<Transform> barnies { get; set; }
        public Transform nearestFood { get; set; }
        public Transform nearestIvedo { get; set; }
        public Transform nearestFred { get; set; }
        public Transform nearestBarney { get; set; }
    }
    [SerializeField] private Collisions collisions;

    private IEnumerator CheckColliders()
    {
        while (true)
        {
            if (!player.isAtBase)
            {
                Collider[] objects = Physics.OverlapSphere(transform.position, player.sightRange);
                Collisions _collisions = new Collisions();
                foreach (Collider col in objects)
                {
                    if (col.tag == "Food")
                    {
                        ColliderChecker(col, _collisions.foods, _collisions.nearestFood);
                    }
                    else if (col.tag == "Ivedo")
                    {
                        ColliderChecker(col, _collisions.ivedos, _collisions.nearestIvedo);
                    }
                    else if (col.tag == "Fred")
                    {
                        ColliderChecker(col, _collisions.freds, _collisions.nearestFred);
                    }
                    else if (col.tag == "Barney")
                    {
                        ColliderChecker(col, _collisions.barnies, _collisions.nearestBarney);
                    }
                }
                collisions = _collisions;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private void ColliderChecker(Collider col, List<Transform> collisionList, Transform nearestTransform)
    {
        collisionList.Add(col.transform);
        if (nearestTransform == null) nearestTransform = col.transform;
        else if (Vector3.Distance(transform.position, col.transform.position) < Vector3.Distance(transform.position, nearestTransform.position)) nearestTransform = col.transform;

    }
    #endregion

    #region Movement
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    private void Escape()
    {

    }
    private void ChasePlayer(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
    }
    private void SearchForFood()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            Invoke(nameof(MoveAgent), 0);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void MoveAgent()
    {
        agent.SetDestination(walkPoint);
        // GetTired(walkingCost);
    }
    #endregion

}
