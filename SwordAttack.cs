using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        EnemyControler enemyHealth = other.GetComponent<EnemyControler>();
        if (enemyHealth != null)
        {
            // Reduce the enemy's health by the damage amount
            enemyHealth.TakeDamage(damageAmount);
        }
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
