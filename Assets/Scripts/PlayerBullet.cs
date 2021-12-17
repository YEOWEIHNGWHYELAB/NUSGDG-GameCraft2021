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
        if (!isStuck && hitInfo.tag != "Player" && hitInfo.tag != "Trigger")
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
    }
}
