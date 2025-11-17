using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // default vars
    private float speed;
    private int type;
    [SerializeField] private BoxCollider thisCollider;

    // lifetime for moving
    private int lifetime;

    private GameManager gameManager;

    // You can edit how much score the coin gives
    public int scoreValue = 10;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        speed = 2f;
    }

    void Update()
    {
        // Move down the screen (world space)
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Move sideways in a small sine wave (world space)
        transform.Translate(Vector3.right * Mathf.Sin(lifetime / 100f) * Time.deltaTime, Space.World);

        // Destroy if below screen
        if (transform.position.y < -gameManager.verticalScreenSize)
        {
            Destroy(this.gameObject);
        }

        lifetime++;
    }

    private void OnTriggerEnter(Collider whatDidIHit)
    {
        Debug.Log("Hit something!!!");
        Debug.Log("I hit: " + whatDidIHit.tag);

        if (whatDidIHit.tag == "Player")
        {
            // SCORE ONLY – no life added
            gameManager.AddScore(scoreValue);

            // Destroy coin after collecting
            Destroy(this.gameObject);
        }
    }
}
