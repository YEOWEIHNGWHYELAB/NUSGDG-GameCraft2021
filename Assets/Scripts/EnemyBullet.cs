using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float timeLeft = 5;
    public float speed = 20f;
    public int bulletDamage = 20;

    public Rigidbody2D rigidBod;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector2 relativePos = (Vector2)player.position - rigidBod.position;
        relativePos.Normalize();
        rigidBod.velocity = relativePos * speed;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rigidBod.velocity.y, rigidBod.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Debug.Log(hitInfo.name);

        if (hitInfo.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(bulletDamage);
        }

        if (hitInfo.tag != "EnemyRed" && hitInfo.tag != "EnemyBlue" && hitInfo.tag != "EnemyGreen" && hitInfo.tag != "Bullet")
            Destroy(gameObject);
    }
}
