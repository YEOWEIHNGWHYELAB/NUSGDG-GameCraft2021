using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D playerBullet;
    public float speed = 20f;
    public float timeLeft = 2;
    public int playerDamage = 20;
    bool isStuck = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameManager.pinStuckColor = "null";
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!isStuck && hitInfo.tag != "Player" && hitInfo.tag != "Wall")
        {
            isStuck = true;
            playerBullet.velocity = Vector2.zero;
            playerBullet.isKinematic = true;
            transform.parent = hitInfo.transform;
        }

        if (hitInfo.tag == "EnemyRed" || hitInfo.tag == "EnemyBlue" || hitInfo.tag == "EnemyGreen" || hitInfo.tag == "EnemyYellow")
        {
            GameManager.pinStuckColor = hitInfo.tag;
        }

        //pin dont do damange
        /*if (hitInfo.tag == "EnemyRed")
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyRed");

            for (int i = 0; i < obj.Length; i++)
            {
                EnemyHealth[] redEnemyHealthList = obj[i].GetComponents<EnemyHealth>();
                redEnemyHealthList[0].TakeDamage(playerDamage);
            }
        }

        if (hitInfo.tag == "EnemyBlue")
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyBlue");

            for (int j = 0; j < obj.Length; j++)
            {
                EnemyHealth[] blueEnemyHealthList = obj[j].GetComponents<EnemyHealth>();
                blueEnemyHealthList[0].TakeDamage(playerDamage);
            }
        }

        if (hitInfo.tag == "EnemyGreen")
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyGreen");

            for (int k = 0; k < obj.Length; k++)
            {
                EnemyHealth[] greenEnemyHealthList = obj[k].GetComponents<EnemyHealth>();
                greenEnemyHealthList[0].TakeDamage(playerDamage);
            }
        }

        if (hitInfo.tag == "EnemyYellow")
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyYellow");

            for (int l = 0; l < obj.Length; l++)
            {
                EnemyHealth[] yellowEnemyHealthList = obj[l].GetComponents<EnemyHealth>();
                yellowEnemyHealthList[0].TakeDamage(playerDamage);
            }
        }*/
    }
}
