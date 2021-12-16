using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D playerBullet;
    public float speed = 20f;
    public float timeLeft = 2;
    public int bulletDamage = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(playerBullet.velocity.y, playerBullet.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Debug.Log(hitInfo.gameObject.tag);
        if (hitInfo.tag == "EnemyRed")
        {
            // FindObjectOfType<EnemyHealth>().TakeDamage(playerDamage);

            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyRed");
            
            for (int i = 0; i < obj.Length; i++)
            {
                EnemyHealth[] redEnemyHealthList = obj[i].GetComponents<EnemyHealth>();
                redEnemyHealthList[0].TakeDamage(bulletDamage);
            }
            
            Destroy(gameObject);
        }
        
        if (hitInfo.tag == "EnemyBlue")
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyBlue");
            
            for (int j = 0; j < obj.Length; j++)
            {
                EnemyHealth[] blueEnemyHealthList = obj[j].GetComponents<EnemyHealth>();
                blueEnemyHealthList[0].TakeDamage(bulletDamage);
            }

            Destroy(gameObject);
        }

        if (hitInfo.tag == "EnemyGreen")
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("EnemyGreen");
            
            for (int k = 0; k < obj.Length; k++)
            {
                EnemyHealth[] greenEnemyHealthList = obj[k].GetComponents<EnemyHealth>();
                greenEnemyHealthList[0].TakeDamage(bulletDamage);
            }

            Destroy(gameObject);
        }
    }
}
