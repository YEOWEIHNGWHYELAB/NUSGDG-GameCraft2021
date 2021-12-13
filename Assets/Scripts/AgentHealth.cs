using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    public void AgentTakeDamage(int damage)
    {
        // Debug.Log("Voodoo Ouch");

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
