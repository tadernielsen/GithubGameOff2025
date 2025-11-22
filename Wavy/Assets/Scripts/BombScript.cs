using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);

        // Play Explosion Particle Here

        gameManager.StartReset();
    }
}
