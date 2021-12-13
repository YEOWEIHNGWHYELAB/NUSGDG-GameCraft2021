using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float timeLeft = 5;
    public float speed = 20f;
    public Rigidbody2D rigidBod;

    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector2 relativePos = (Vector2)player.position - rigidBod.position;
        relativePos.Normalize();
        rigidBod.velocity = relativePos * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Debug.Log(hitInfo.name);

        if (hitInfo.name != "Enemy")
            Destroy(gameObject);
    }
}
