using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the enemy's health has reached zero or below
        if (currentHealth <= 0)
        {
            // Enemy is defeated, you can perform any desired actions here
            //Destroy(gameObject);
        }
    }
}
