using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    Rigidbody rb;

    public Transform player;
    public float speed = 1f;
    public int maxHealth = 100;
    private int currentHealth;
    public float knockbackForce = 3f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction from the enemy to the player
        Vector3 direction = player.position - transform.position;

        // Normalize the direction vector
        direction.Normalize();

        // Move the enemy towards the player
        transform.Translate(direction * (speed / 2) * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        // Check if the enemy's health has reached zero or below
        if (currentHealth <= 0)
        {
            // Enemy is defeated, you can perform any desired actions here
            Destroy(gameObject);
        }
        else
        {
            // Apply knockback effect
            Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                // Calculate knockback direction away from the sword
                Vector3 knockbackDirection = transform.position - player.position;
                knockbackDirection.Normalize();

                // Apply the knockback force to the enemy
                enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SpecialAttack"))
        {
            // Subtract health from the player
            currentHealth -= 10; // Adjust the amount of health to lose as needed
            Debug.Log(currentHealth);

            // Apply knockback force
            Vector3 knockbackDirection = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // You can update the health UI element here if you have one


            if (currentHealth <= 0)
            {
                // Player is dead, handle game over or respawn logic here
                Destroy(gameObject);
            }
        }
    }

}
