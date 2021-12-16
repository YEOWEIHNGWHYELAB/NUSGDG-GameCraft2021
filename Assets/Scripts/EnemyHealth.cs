using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int health;
    public GameObject deathEffect;
    [SerializeField] private HealthbarBehaviour enemyHealthbar;

    public void TakeDamage(int damage)
    {
        health -= damage;
        enemyHealthbar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject enemyBlueObject = GameObject.FindGameObjectWithTag("EnemyBlue");
        EnemyHealth[] blueEnemyHealthList = enemyBlueObject.GetComponents<EnemyHealth>();
        int blueEnemyHealthListLength = blueEnemyHealthList.Length;
        int i = 0;
        while (i < blueEnemyHealthListLength)
        {
            blueEnemyHealthList[i].InitializeEnemyHealth();
            i++;
        }

        GameObject enemyRedObject = GameObject.FindGameObjectWithTag("EnemyRed");
        EnemyHealth[] redEnemyHealthList = enemyRedObject.GetComponents<EnemyHealth>();
        int redEnemyHealthListLength = redEnemyHealthList.Length;
        int j = 0;
        while (j < redEnemyHealthListLength)
        {
            redEnemyHealthList[j].InitializeEnemyHealth();
            j++;
        }

        GameObject enemyGreenObject = GameObject.FindGameObjectWithTag("EnemyGreen");
        EnemyHealth[] GreenEnemyHealthList = enemyGreenObject.GetComponents<EnemyHealth>();
        int greenEnemyHealthListLength = GreenEnemyHealthList.Length;
        int k = 0;
        while (k < greenEnemyHealthListLength)
        {
            GreenEnemyHealthList[k].InitializeEnemyHealth();
            k++;
        }
        */
        InitializeEnemyHealth();
    }

    public void InitializeEnemyHealth()
    {
        health = maxHealth;
        enemyHealthbar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
