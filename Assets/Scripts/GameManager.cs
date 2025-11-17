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
<<<<<<< Updated upstream
=======
    public GameObject lifePrefab;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        InvokeRepeating("CreateEnemy", 1f, 3f);
        InvokeRepeating("SpawnCoin", 2f, coinSpawnInterval);
=======
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateLife", 10, 15);
        StartCoroutine(SpawnCoinsRoutine());
    }
    IEnumerator SpawnCoinsRoutine()
    {
        while (true)
        {
            // Wait for a random time before spawning a coin
            float waitTime = Random.Range(8f, 20f);  // you can tweak these numbers
            yield return new WaitForSeconds(waitTime);

            CreateCoin();
        }
    }

    // Update is called once per frame
    void Update()
    {
>>>>>>> Stashed changes

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

    void CreateCoin ()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));

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
