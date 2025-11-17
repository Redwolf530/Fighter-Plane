using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // default vars
    private float speed;
    private int type;
    [SerializeField] private BoxCollider thisCollider;

    //lifetime var for movement related stuff
    private int lifetime;

    public GameObject explosionPrefab;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //on start select a random enemy type:
        //0: default (moves straight)
        //1: sinusoid (follows a sine wave)
        type = Random.Range(0, 3);

        //define speed based on type
        if (type == 0)
        {
            speed = 4f;
        } else if (type == 1)
        {
            speed = 1f;
        } else if (type == 2)
        {
            speed = 6f;
        } else
        {
            Debug.Log("Unrecognized enemy type!");
            speed = 0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //step enemy down by speed every frame
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        //type specific enemy movement
        if (type == 0) // basic enemy
        {
            //could make this more interesting if wanted
        } else if (type == 1) { // sinusoid enemy
            transform.Translate(Vector3.right * Mathf.Sin(lifetime / 50f) * Time.deltaTime);
        } else if (type == 2) { // reverse sinusoid enemy
            transform.Translate(Vector3.right * Mathf.Asin(lifetime / 20f) * Time.deltaTime * 5f);
        }

        //delete if below screen
        if (transform.position.y < -gameManager.verticalScreenSize)
        {
            Destroy(this.gameObject);
        }

        //step lifetime once per frame (use this for movement itll make it a lot easier, math math math)
        lifetime++;
    }

    //fixed collision handling
    private void OnTriggerEnter(Collider whatDidIHit)
    {
        Debug.Log("Hit something!!!");
        Debug.Log("I hit: " + whatDidIHit.tag);
        if (whatDidIHit.tag == "Player")
        {
            Destroy(this.gameObject);
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        else if (whatDidIHit.tag == "Weapon")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}