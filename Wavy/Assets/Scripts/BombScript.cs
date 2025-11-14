using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameManager gameManager;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = FindObjectOfType<UIManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        uiManager.UpdateHealth(0);
        Destroy(collision.gameObject);

        gameManager.StartReset();
    }
}
