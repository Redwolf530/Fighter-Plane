using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private float speed = 2f;
    private int lifetime;

    private GameManager gameManager;

    // How long the shield lasts after pickup
    public float shieldDuration = 5f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Move DOWN the screen
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Sideways wave motion
        transform.Translate(Vector3.right * Mathf.Sin(lifetime / 100f) * Time.deltaTime);

        // Destroy if below the screen
        if (transform.position.y < -gameManager.verticalScreenSize)
        {
            Destroy(gameObject);
        }

        lifetime++;
    }

    private void OnTriggerEnter(Collider whatDidIHit)
    {
        if (whatDidIHit.CompareTag("Player"))
        {
            // Activate shield on player
            PlayerController player = whatDidIHit.GetComponent<PlayerController>();
            if (player != null)
                player.ActivateShield(shieldDuration);

            Destroy(gameObject);
        }
    }
}
