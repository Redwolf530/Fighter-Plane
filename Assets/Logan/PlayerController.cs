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
    private bool isShielded = false;
    private float shieldTimer = 0f;

    public GameObject shieldVisual;

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
    public void ActivateShield(float duration)
    {
        Debug.Log("ActivateShield() called with duration: " + duration);
        Debug.Log("shieldVisual: " + shieldVisual);
        isShielded = true;
        shieldTimer = duration;

        if (shieldVisual != null)
            shieldVisual.SetActive(true);

        if (shieldTimer <= 0)
        {
            isShielded = false;
            shieldVisual.SetActive(false);
        }
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
        HandleMovement();
        HandleShooting();
        HandleShieldTimer();
    }
    void HandleShieldTimer()
    {
        if (isShielded)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0f)
            {
                isShielded = false;
                if (shieldVisual != null)
                    shieldVisual.SetActive(false);
            }
        }
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
        if (isShielded)
            return; // Shield absorbs the hit

        lives--;
        gameManager.ChangeLivesText(lives);

        if (lives <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
