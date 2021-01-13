using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer, PlayerLayer;

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
        if (target != null) MoveToPoint(target.position);
        yield return new WaitForSeconds(1);
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        target = newTarget.transform;
    }
    public void StopFollowingTarget()
    {
        target = null;
    }
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


    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, walkPointRange);
    }

}
