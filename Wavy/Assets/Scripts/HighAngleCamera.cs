using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighAngleCamera : MonoBehaviour
{
    public Transform boat;
    public float cameraSpeed = 2.0f;
    public bool cameraActive = true;

    private bool lockedOnBoat = false;

    // Update is called once per frame
    void Update()
    {
        if (!cameraActive) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            lockedOnBoat = !lockedOnBoat;
        }

        if (boat != null)
        {
            if (lockedOnBoat)
            {
                if (boat != null)
                {
                    lockOn();
                }
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

    public void lockOn()
    {
        Vector3 targetPosition = boat.position + new Vector3(0, 6, -6.25f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}
