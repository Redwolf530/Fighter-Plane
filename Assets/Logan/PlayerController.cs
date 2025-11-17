using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public int lives = 3;
    public float speed = 6f;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    public GameManager gameManager;
    private float horizontalInput;

    void Start()
    {
        // Find the GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Spawn player near the bottom of the screen
        float initialY = -gameManager.verticalScreenSize + 1f; // 1 unit above bottom
        transform.position = new Vector3(0, initialY, 0);

        // Update UI
        gameManager.ChangeLivesText(lives);
    }
    public void GainALife()
    {
        lives++;
        gameManager.ChangeLivesText(lives);
    }
    public void AddLife()
    {
        lives++;
        if (gameManager != null)
            gameManager.ChangeLivesText(lives);
    }


    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        // Read horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        // Calculate bottom Y (never change)
        float bottomY = -gameManager.verticalScreenSize + 1f;

        // Move horizontally ONLY
        transform.position = new Vector3(
            transform.position.x + (horizontalInput * speed * Time.deltaTime),
            bottomY,
            0
        );

        // Horizontal wrap-around
        float screenLimit = gameManager.horizontalScreenSize;
        if (transform.position.x > screenLimit)
            transform.position = new Vector3(-screenLimit, bottomY, 0);
        else if (transform.position.x < -screenLimit)
            transform.position = new Vector3(screenLimit, bottomY, 0);
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        lives--;
        gameManager.ChangeLivesText(lives);

        if (lives <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
