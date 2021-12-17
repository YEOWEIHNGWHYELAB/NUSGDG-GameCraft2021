using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlRange : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public float pursuitRange = 8;
    public float chaseRange = 6;
    private float currentPlayerDistance;
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
        // Debug.Log("Patroling!");
        currentPlayerDistance = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);

        if (currentPlayerDistance < pursuitRange)
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


    private void GetInRangeToPlayer(Transform player)
    {
        // Debug.Log("Chase To Range!");
        agent.CalculatePath(player.position, navMeshPath);
        agent.SetPath(navMeshPath);
    }

    private void RangeShooting()
    {
        // Debug.Log("Shooting!");
        agent.isStopped = true;
        Shoot();
    }

    void Shoot()
    {
        bulletCooldownTimer -= Time.deltaTime;

        if (bulletCooldownTimer > 0) return;

        bulletCooldownTimer = bulletInterval;
        FindObjectOfType<AudioManager>().Play("MageFire");
        Instantiate(enemyBullet, enemyTransform.position, enemyTransform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.gameHasEnded)
        {
            playerTransform = GameObject.Find("PlayerNew").transform;
            currentPlayerDistance = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);

            if (agent.isStopped)
            {
                agent.isStopped = false;
            }

            if (currentPlayerDistance > pursuitRange)
            {
                Patroling();
            } else if (currentPlayerDistance > chaseRange) {
                GetInRangeToPlayer(playerTransform);
            } else {
                RangeShooting();
            }

            if (bulletCooldownTimer > 0)
            {
                bulletCooldownTimer -= Time.deltaTime;
                return;
            }
        }
    }
}
