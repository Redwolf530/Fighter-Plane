using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;

    [Header("Coin Settings")]
    public float coinSpawnInterval = 5f; // seconds between coin spawns

    [Header("UI")]
    public TextMeshProUGUI livesText;

    [Header("Screen Settings")]
    public float horizontalScreenSize = 10f;
    public float verticalScreenSize = 6.5f;

    [Header("Game Stats")]
    public int score = 0;

    void Start()
    {
        // Spawn player at bottom of screen
        Vector3 playerSpawnPos = new Vector3(0, -verticalScreenSize + 0.5f, 0);
        GameObject playerInstance = Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);

        // Assign this GameManager to the player's PlayerController
        PlayerController pc = playerInstance.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.gameManager = this;
        }

        // Create sky, enemies, and coins
        CreateSky();
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("SpawnCoin", 2f, coinSpawnInterval);

        // Adjust camera
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = verticalScreenSize;
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }

    void CreateEnemy()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-horizontalScreenSize * 0.9f, horizontalScreenSize * 0.9f),
            verticalScreenSize,
            0
        );
        Instantiate(enemyOnePrefab, spawnPos, Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 cloudPos = new Vector3(
                Random.Range(-horizontalScreenSize, horizontalScreenSize),
                Random.Range(-verticalScreenSize, verticalScreenSize),
                0
            );
            Instantiate(cloudPrefab, cloudPos, Quaternion.identity);
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab == null) return;

        Vector3 spawnPos = new Vector3(
            Random.Range(-horizontalScreenSize, horizontalScreenSize),
            Random.Range(-verticalScreenSize, verticalScreenSize),
            0
        );

        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
    }

    public void ChangeLivesText(int currentLives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }
}
