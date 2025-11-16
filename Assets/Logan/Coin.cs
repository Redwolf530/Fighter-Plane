using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after lifetime
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AddLife();  // increase player's life
            Destroy(gameObject);
        }
    }
}
