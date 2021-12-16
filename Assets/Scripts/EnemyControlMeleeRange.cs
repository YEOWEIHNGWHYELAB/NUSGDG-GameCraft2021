using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlMeleeRange : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public float meleeRangeThreshold = 1;
    public float shootingRange = 6;
    public float pursuitRange = 4;
    private float currentMeleeDistance;
    public int meleeDamage = 10;
    public Transform enemyTransform;
    private Transform playerTransform;
    public float bulletInterval = 5;
    private float bulletCooldownTimer = 0;
    public float lowWalkPointRangeX;
    public float walkPointRangeX;
    public float lowWalkPointRangeY;
    public float walkPointRangeY;
    private bool walkPointReached = true;
    private Vector2 patrolDestination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Patroling()
    {
        if (walkPointReached)
        {
            float randomVectorX = Random.Range(lowWalkPointRangeX, walkPointRangeX);
            float randomVectorY = Random.Range(lowWalkPointRangeY, walkPointRangeY);
            patrolDestination = new Vector2(randomVectorX, randomVectorY);

            agent.SetDestination(patrolDestination);
        }


        walkPointReached = false;

        Vector2 distance = (Vector2)enemyTransform.position - patrolDestination;

        Debug.Log(distance.magnitude);

        // WalkPoint Reached
        if (distance.magnitude < 4)
            walkPointReached = true;
    }

    private void ChasePlayer(Transform player)
    {
        Debug.Log("Chase!");
        agent.SetDestination(player.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.gameHasEnded)
        {
            playerTransform = GameObject.Find("PlayerNew").transform;
            currentMeleeDistance = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);

            if (currentMeleeDistance > pursuitRange)
            {
                Patroling();
            } else {
                ChasePlayer(playerTransform);
                walkPointReached = true;
            }

            if (bulletCooldownTimer > 0)
            {
                bulletCooldownTimer -= Time.deltaTime;
                return;
            }

            if (currentMeleeDistance < meleeRangeThreshold)
            {
                FindObjectOfType<PlayerHealth>().TakeDamage(meleeDamage);
                bulletCooldownTimer = bulletInterval;
            }
        }
    }
}
