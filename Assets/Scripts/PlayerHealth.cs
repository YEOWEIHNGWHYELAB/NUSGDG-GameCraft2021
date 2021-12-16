using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int health;
    public GameObject deathEffect;
    public GameManager gm;
    [SerializeField] private HealthbarBehaviour playerHealthbar;

    private void Start()
    {
        health = maxHealth;
        gm = FindObjectOfType<GameManager>();
        playerHealthbar.SetHealth(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Voodoo Ouch");

        health -= damage;
        playerHealthbar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gm.EndGame(false);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
