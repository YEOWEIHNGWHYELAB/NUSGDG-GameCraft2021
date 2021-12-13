using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int health;
    public GameObject deathEffect;
    [SerializeField] private EnemyHealthbar enemyHealthbar;

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
        health = maxHealth;
        enemyHealthbar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
