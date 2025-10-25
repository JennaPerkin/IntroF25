using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PREPEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;

    [Header("Patroling")]
    public Vector3 walkPoint;
    bool isWalkPointSet;
    public float walkPointRange;

    [Header("States")]
    public float sightRange;
    public bool isPlayerInSightRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if (isPlayerInSightRange) Chasing();
        else Patroling();
    }

    private void Patroling()
    {
        if (!isWalkPointSet) SearchWalkPoint();
        else
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            isWalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
            isWalkPointSet = true;
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }
}
