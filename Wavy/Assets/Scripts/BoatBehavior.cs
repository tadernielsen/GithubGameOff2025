using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBehavior : MonoBehaviour
{
    public int health;

    [Header("Boyancy Settings")]
    public float waterLevel = 0f;
    public float buoyancyForce = 10f;
    public float displacementAmount = 0.1f;
    public float drag = 1f;
    public float angularDrag = 1f;

    private UIManager uiManager;
    private Rigidbody rb;
    private GameManager gameManager;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        uiManager.UpdateHealth(health);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float displacement = Mathf.Clamp01((waterLevel - rb.position.y) / displacementAmount);

        Vector3 upwardForce = Vector3.up * displacement * buoyancyForce;
        rb.AddForce(upwardForce, ForceMode.Acceleration);

        rb.AddForce(-rb.velocity * drag, ForceMode.VelocityChange);
        rb.AddTorque(-rb.angularVelocity * angularDrag, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health--;
            uiManager.UpdateHealth(health);

            if (health <= 0)
            {
                Destroy(gameObject);
                gameManager.StartReset();
            }
        }
    }
}
