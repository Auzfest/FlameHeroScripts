using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    private bool isMoving = false;
    private float moveSpeed = 5f;
    private float timer = 0f;
    private float lifeTime = 3f;
    public int damage = 2;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        EnemyControler enemyHealth = other.GetComponent<EnemyControler>();
        if (enemyHealth != null)
        {
            // Reduce the enemy's health by the damage amount
            enemyHealth.TakeDamage(damage);
        }
    }

            private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartMoving(Vector3 direction)
    {
        isMoving = true;
        transform.rotation = Quaternion.LookRotation(direction);

    }
}
