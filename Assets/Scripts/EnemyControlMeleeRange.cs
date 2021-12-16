using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlMeleeRange : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public float meleeRangeThreshold = 1;
    public float pursuitRange = 8;
    public float chaseToRange = 6;
    public float shootingRange = 4;
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
    private Vector2 patrolDestination;
    NavMeshPath navMeshPath;

    [SerializeField] private GameObject enemyBullet;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        navMeshPath = new NavMeshPath();
    }

    private void Patroling()
    {
        currentMeleeDistance = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);

        if (currentMeleeDistance < pursuitRange)
        {
            return;
        }

        float randomVectorX = Random.Range(lowWalkPointRangeX, walkPointRangeX);
        float randomVectorY = Random.Range(lowWalkPointRangeY, walkPointRangeY);
        patrolDestination = new Vector2(randomVectorX, randomVectorY);

        if (agent.CalculatePath(patrolDestination, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetPath(navMeshPath);
        }
        else
        {
            Patroling();
        }
    }

    private void ChasePlayer(Transform player)
    {
        // Debug.Log("Chase!");
        agent.CalculatePath(player.position, navMeshPath);
        agent.SetPath(navMeshPath);
    }

    private void GetInRangeToPlayer(Transform player)
    {
        // Debug.Log("Chase!");
        agent.CalculatePath(player.position, navMeshPath);
        agent.SetPath(navMeshPath);
    }

    private void RangeShooting()
    {

    }

    void shoot()
    {
        bulletCooldownTimer -= Time.deltaTime;

        if (bulletCooldownTimer > 0) return;

        bulletCooldownTimer = bulletInterval;

        Instantiate(enemyBullet, enemyTransform.position, enemyTransform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.gameHasEnded)
        {
            playerTransform = GameObject.Find("PlayerNew").transform;
            currentMeleeDistance = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);

            if (currentMeleeDistance > pursuitRange) {
                Patroling();
            } else if (currentMeleeDistance > chaseToRange) {
                ChasePlayer(playerTransform);
            } else if (currentMeleeDistance > shootingRange) {
                RangeShooting();
            } else {
                ChasePlayer(playerTransform);
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
