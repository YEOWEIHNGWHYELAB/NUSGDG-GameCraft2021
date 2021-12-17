using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControlMelee : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public float meleeRangeThreshold = 1;
    public float pursuitRange = 8;
    private float currentPlayerDistance;
    public int meleeDamage = 10;
    public Transform enemyTransform;
    private Transform playerTransform;
    public float meleeInterval = 5;
    private float bulletCooldownTimer = 0;
    public float lowWalkPointRangeX;
    public float walkPointRangeX;
    public float lowWalkPointRangeY;
    public float walkPointRangeY;
    private Vector2 patrolDestination;
    NavMeshPath navMeshPath;
    public GameObject meleeAttack;
    public Animator animator;

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

    private void ChasePlayer(Transform player)
    {
        // Debug.Log("Chase To Melee");
        agent.CalculatePath(player.position, navMeshPath);
        agent.SetPath(navMeshPath);
    }

    void Aim()
    {
        LookAtTarget(playerTransform.position, meleeAttack.transform);
    }

    void LookAtTarget(Vector3 target, Transform source)
    {
        Vector3 diff = target - source.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        source.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!GameManager.gameHasEnded)
        {
            playerTransform = GameObject.Find("PlayerNew").transform;
            currentPlayerDistance = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);
            Aim();
            lookAtAnimation();
            if (currentPlayerDistance > pursuitRange)
            {
                Patroling();
            } else
            {
                ChasePlayer(playerTransform);
            }

            if (bulletCooldownTimer > 0)
            {
                bulletCooldownTimer -= Time.deltaTime;
                return;
            }

            if (currentPlayerDistance < meleeRangeThreshold)
            {
                meleeAttack.GetComponentInChildren<Animator>().SetTrigger("attack");
                FindObjectOfType<AudioManager>().Play("Melee");
                FindObjectOfType<PlayerHealth>().TakeDamage(meleeDamage, tag);
                bulletCooldownTimer = meleeInterval;
            }
        }
    }

    void lookAtAnimation()
    {
        float playerXCoor = playerTransform.position.x;
        float playerYCoor = playerTransform.position.y;

        float enemyXCoor = transform.position.x;
        float enemyYCoor = transform.position.y;

        float playerDeltaX = playerXCoor - enemyXCoor;
        float playerDeltaY = playerYCoor - enemyYCoor;


        if (playerDeltaY > 0)
        {
            animator.SetInteger("Direction", 1);

            if (Mathf.Abs(playerDeltaX) > Mathf.Abs(playerDeltaY))
            {
                if (playerDeltaX < 0)
                {
                    animator.SetInteger("Direction", 3);
                }
                else if (playerDeltaX > 0)
                {
                    animator.SetInteger("Direction", 2);
                }
            }
        }
        else if (playerDeltaY < 0)
        {
            animator.SetInteger("Direction", 0);

            if (Mathf.Abs(playerDeltaX) > Mathf.Abs(playerDeltaY))
            {
                if (playerDeltaX < 0)
                {
                    animator.SetInteger("Direction", 3);
                }
                else if (playerDeltaX > 0)
                {
                    animator.SetInteger("Direction", 2);
                }
            }
        }

        // animator.SetBool("IsMoving", dir.magnitude > 0);
    }
}
