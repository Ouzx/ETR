using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    /*
     * HARD CODED:
     * - Target checker delay: 0.1f
     * - Patrol distance checker: 1f
     * - WalkPoint searcher distance checker: 2f
     * - LookTarget: Rotation Speed: 50f 
    */
    public LayerMask GroundLayer, PlayerLayer, FoodLayer;
    Stats stats;

    // Patrolling
    [SerializeField] Vector3 walkPoint;
    [SerializeField] bool isWalkPointSet;

    [SerializeField] NavMeshAgent agent;
    Transform target;
    void Awake()
    {
    }
    void Start()
    {
        stats = GetComponent<PlayerController>().stats;
        StartCoroutine(nameof(TargetChecker));
    }
    IEnumerator TargetChecker()
    {
        while (true)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                LookTarget();
            }
            yield return new WaitForSeconds(0.1f); // you can change it if player acts laggy
        }
    }

    #region Movement
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = stats.attackRange.GetMaxValue();
        agent.updateRotation = false;
        target = newTarget.transform;
    }
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }
    void LookTarget()
    {
        Vector3 directon = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directon.x, 0f, directon.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 50f);
    }

    public void SetSpeed(float speed)
    {
        agent.acceleration = speed;
    }
    #endregion

    #region  Patrolling
    public void Patrol()
    {
        if (!isWalkPointSet) SearchWalkPoint();
        if (isWalkPointSet)
        {
            MoveToPoint(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) isWalkPointSet = false;
    }
    void SearchWalkPoint()
    {
        float walkPointRange = stats.walkPointRange;
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, GroundLayer))
            isWalkPointSet = true;
    }
    #endregion

}
