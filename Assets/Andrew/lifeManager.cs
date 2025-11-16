using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeManager : MonoBehaviour
{
    // default vars
    private float speed;
    private int type;
    [SerializeField] private BoxCollider thisCollider;

    //lifetime var for movement related stuff
    private int lifetime;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        speed = 2f;

    }

    // Update is called once per frame
    void Update()
    {
        //step life down by speed every frame
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // skoot around
        transform.Translate(Vector3.right * Mathf.Sin(lifetime / 100f) * Time.deltaTime);

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
            whatDidIHit.GetComponent<PlayerController>().GainALife();
            Destroy(this.gameObject);
        }
    }
}