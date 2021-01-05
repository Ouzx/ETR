using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public GameObject Home;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {

    }


    private void Update()
    {
        Searching();
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Searching();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attack();

    }

    private void Searching()
    {


    }
    private void PickAngle()
    {
        transform.LookAt(Home.transform);
        float angle = Random.Range(transform.rotation.y - 45, transform.rotation.y + 45);
        transform.rotation = Quaternion.Euler(transform.rotation.x, -angle, transform.rotation.z);
    }
    private void Chasing()
    {

    }

    private void Escaping()
    {

    }

    private void Attack()
    {

    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, sightRange);
    }
}
