using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public int[] movementVector = new int[3];
    GameObject player;
    Vector3 playerCoordinates;
    Vector3 enemyCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        movementVector[0] = -1;
        movementVector[1] = 0;
        movementVector[2] = 1;

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int randomVectorX = Random.Range(0, 3);
        int randomVectorY = Random.Range(0, 3);
        if (!GameManager.gameHasEnded)
        {
            playerCoordinates = player.transform.position;
        }
        enemyCoordinates = transform.position;

        float playerXCoor = playerCoordinates.x;
        float playerYCoor = playerCoordinates.y;

        float enemyXCoor = enemyCoordinates.x;
        float enemyYCoor = enemyCoordinates.y;

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
                } else if (playerDeltaX > 0) {
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
                } else if (playerDeltaX > 0) {
                    animator.SetInteger("Direction", 2);
                }
            }
        }

        // animator.SetBool("IsMoving", dir.magnitude > 0);

    }
}
