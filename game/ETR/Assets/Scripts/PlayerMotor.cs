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
     * - Target Interaction range multiplier: 0.7f
     * - LookTarget: Rotation Speed: 0.5f 
    */
    public LayerMask GroundLayer, PlayerLayer, FoodLayer;

    // Patrolling
    [SerializeField] Vector3 walkPoint;
    [SerializeField] bool isWalkPointSet;
    [SerializeField] float walkPointRange;

    Transform target;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = GetComponent<Interactable>().interactionRange * 0.7f;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


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
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, GroundLayer))
            isWalkPointSet = true;
    }
    #endregion


    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, walkPointRange);
    }

}
