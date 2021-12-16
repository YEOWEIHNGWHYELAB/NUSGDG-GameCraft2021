using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponMelee : MonoBehaviour
{
    public float meleeRangeThreshold = 1;
    private float currentMelee;
    public int meleeDamage = 10;
    public Transform enemyTransform;
    public Transform playerTransform;
    public float bulletInterval = 5;
    private float bulletCooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMelee = Vector2.Distance((Vector2)enemyTransform.position, (Vector2)playerTransform.position);

        if (bulletCooldownTimer > 0)
        {
            bulletCooldownTimer -= Time.deltaTime;
            return;
        }

        if (currentMelee < meleeRangeThreshold)
        {
            FindObjectOfType<PlayerHealth>().TakeDamage(meleeDamage);
            bulletCooldownTimer = bulletInterval;
        } 
    }
}
