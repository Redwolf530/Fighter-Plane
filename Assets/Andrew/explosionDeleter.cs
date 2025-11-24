using UnityEngine;

public class explosionDeleter : MonoBehaviour
{
    //define lifetime
    private int lifetime;

    //define max life
    private int maxLife;

    void Start()
    {
        maxLife = 90;
    }

    // Update is called once per frame
    void Update()
    {
    if (lifetime > maxLife)
        {
            Destroy(this.gameObject);
        }

    lifetime++;
    }
}
