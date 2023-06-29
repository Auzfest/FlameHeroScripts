using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]float movementSpeed = 6f;
    [SerializeField]float jumpForce = 6f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    private bool isGrounded;
    private bool fireUp;
    private float specialAttackTimer = 5f;
    private float buffedTimer = 15f;
    private float specialAttackCooldown = 0f;
    private float specialAttackCooldown2 = 0f;
    private float buffed = 0f;


    public Camera playerCamera;
    public Transform enemy;
    public Image healthBar;

    public float health = 100;
    public float knockbackForce = 75f;

    public GameObject blockPrefab;
    public GameObject blockPrefab2;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the camera's forward direction
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        cameraForward.y = 0f; // Ignore vertical movement
        cameraRight.y = 0f; // Ignore vertical movement

        // Normalize the direction vector
        cameraForward.Normalize();
        cameraRight.Normalize();


        if (isGrounded || jumpsRemaining > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                jumpsRemaining--;
            }
        }

        isGrounded = false;

        if (specialAttackCooldown >= specialAttackTimer)
        {
            if (Input.GetMouseButtonDown(1))
            {
                SpawnBlock();
                specialAttackCooldown = 0;
            }
        }

        specialAttackCooldown += Time.deltaTime;

        if (specialAttackCooldown2 >= specialAttackTimer)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                fireUp = true;
                FirePowerUp();
                specialAttackCooldown2 = 0;
            }
        }

        specialAttackCooldown2 += Time.deltaTime;


        if (Input.GetKey(KeyCode.W)) 
        {
            transform.position += cameraForward * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= cameraForward * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= cameraRight * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += cameraRight * movementSpeed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Subtract health from the player
            health -= 10; // Adjust the amount of health to lose as needed
            healthBar.fillAmount = health / 100f;

            // Apply knockback force
            Vector3 knockbackDirection = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // You can update the health UI element here if you have one


            if (health <= 0)
            {
                // Player is dead, handle game over or respawn logic here
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(2);
            }
        }

        if (collision.gameObject.CompareTag("Terrain"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps;
        }
    }
    private void SpawnBlock()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 2f + Vector3.up * 1f; // Spawn in front of the player
        Vector3 direction = transform.forward; // Get player's facing direction
        GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        block.GetComponent<SpecialAttack>().StartMoving(direction);
    }

    private void FirePowerUp()
    {
        SwordAttack attack = gameObject.AddComponent<SwordAttack>();

        // Get the player's position
        Vector3 spawnPosition = transform.position + Vector3.up;  // Assuming block should spawn just above the player

        // Instantiate the block at the spawn position
        GameObject newBlock = Instantiate(blockPrefab2, spawnPosition, Quaternion.identity);

        // Parent the block to the player so that it moves with the player
        newBlock.transform.parent = transform;
        if (fireUp == true)
        {
            movementSpeed = 12f;
            attack.damageAmount = 20;
            buffed += Time.deltaTime;
            Destroy(newBlock, 15f);
            if (buffed >= buffedTimer)
            {
                Debug.Log(fireUp);
                fireUp = false;
            }
        }
        else
        {
            movementSpeed = 6f;
            attack.damageAmount = 10;
        }
        
    }

}
