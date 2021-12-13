using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject enemyBullet;

    [SerializeField] private float bulletInterval = 5;
    private float bulletCooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    void shoot()
    {
        bulletCooldownTimer -= Time.deltaTime;

        if (bulletCooldownTimer > 0) return;

        bulletCooldownTimer = bulletInterval;

        Instantiate(enemyBullet, firepoint.position, firepoint.rotation);
    }
}
