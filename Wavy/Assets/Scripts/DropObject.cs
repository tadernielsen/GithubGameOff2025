using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public GameObject[] objects = new GameObject[1];
    public Camera cam;
    public float clickDelay = 0.5f;
    public float dropHeight = 2f;
    public bool boatActive = true;

    private bool canClick = true;
    private int currentIndex = 0;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        gameManager = GameManager.Instance;

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseUnpause();
        }

        if (!boatActive)
        {
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && currentIndex < objects.Length - 1)
        {
            currentIndex++;

            UpdateUI();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && currentIndex > 0)
        {
            currentIndex--;

            UpdateUI();
        }

        Vector2 mousePos = Input.mousePosition;
        Ray mouseRay = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hit, 15f) && canClick)
        {
            if (hit.collider.gameObject.tag == "Water")
            {
                dropObject(hit.point + new Vector3(0, dropHeight, 0));
            }
        }
    }

    private void UpdateUI()
    {
        gameManager.UpdateObjectType(objects[currentIndex].GetComponent<ObjectBehavior>().objectName);
    }

    private void dropObject(Vector3 position)
    {
        Instantiate(objects[currentIndex], position, Quaternion.identity);
        gameManager.DroppedObject();
        StartCoroutine("delay");
    }

    IEnumerator delay()
    {
        canClick = false;
        yield return new WaitForSeconds(clickDelay);
        canClick = true;
    }
}
