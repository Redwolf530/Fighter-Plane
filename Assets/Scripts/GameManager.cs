using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject lifePrefab;
    public GameObject coinPrefab;
    public GameObject shieldPowerUpPrefab;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI shieldText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score = 0;
    public int sheld = 0;

    [SerializeField] public AudioSource coinSFX;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        //Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateLife", 10, 13);
        InvokeRepeating("SpawnShieldPowerUp", 5f, 10f);
        StartCoroutine(SpawnCoinsRoutine());

        scoreText.text = "Score: " + score;
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

    }

    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateLife()
    {
        Instantiate(lifePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    void CreateCoin()
    {
        Instantiate(coinPrefab,
            new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f,
            verticalScreenSize, 0),
            Quaternion.Euler(180, 0, 0));
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }

    }
    public void AddScore(int points, string source = "none")
    {
        score += points;
        scoreText.text = "Score: " + score;
        if (source == "coin")
        {
            coinSFX.Play();
        }
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    void SpawnShieldPowerUp()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-horizontalScreenSize, horizontalScreenSize),
            verticalScreenSize,
            0
        );

        Instantiate(shieldPowerUpPrefab, spawnPos, Quaternion.identity);
    }
}