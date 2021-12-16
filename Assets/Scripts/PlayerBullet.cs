using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Rigidbody2D playerBullet;
    public float speed = 20f;
    public float timeLeft = 2;
    public int playerDamage = 20;

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
        if (hitInfo.gameObject.tag == "EnemyRed")
        {
            // FindObjectOfType<EnemyHealth>().TakeDamage(playerDamage);

            GameObject obj = GameObject.FindGameObjectWithTag("EnemyRed");
            EnemyHealth[] redEnemyHealthList = obj.GetComponents<EnemyHealth>();
            redEnemyHealthList[0].TakeDamage(playerDamage);
            Destroy(gameObject);
        }
        
        if (hitInfo.gameObject.tag == "EnemyBlue")
        {
            GameObject obj = GameObject.FindGameObjectWithTag("EnemyBlue");
            EnemyHealth[] blueEnemyHealthList = obj.GetComponents<EnemyHealth>();
            blueEnemyHealthList[0].TakeDamage(playerDamage);
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.tag == "EnemyGreen")
        {
            GameObject obj = GameObject.FindGameObjectWithTag("EnemyGreen");
            EnemyHealth[] greenEnemyHealthList = obj.GetComponents<EnemyHealth>();
            greenEnemyHealthList[0].TakeDamage(playerDamage);
            Destroy(gameObject);
        }
    }
}
