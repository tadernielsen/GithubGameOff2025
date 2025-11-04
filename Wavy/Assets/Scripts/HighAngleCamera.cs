using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighAngleCamera : MonoBehaviour
{
    public Transform boat;
    public float cameraSpeed = 2.0f;

    private bool lockedOnBoat = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            lockedOnBoat = !lockedOnBoat;
        }

        if (lockedOnBoat)
        {
            Vector3 targetPosition = boat.position + new Vector3(0, 4, -4.25f);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput) * cameraSpeed * Time.deltaTime;

            transform.position += movement;
        }
    }
}
