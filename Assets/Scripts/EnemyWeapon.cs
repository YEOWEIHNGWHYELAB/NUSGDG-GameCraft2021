using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    public Transform firepoint;
    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        shoot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shoot()
    {
        Instantiate(enemyBullet, firepoint.position, firepoint.rotation);
    }
}
