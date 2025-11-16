using System.Collections;
using System.Collections.Generic;
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
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        uiManager = FindObjectOfType<UIManager>();
        updateUI();

        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!boatActive)
        {
            return;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && currentIndex < objects.Length - 1)
        {
            currentIndex++;

            updateUI();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && currentIndex > 0)
        {
            currentIndex--;

            updateUI();
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

    private void updateUI()
    {
        uiManager.UpdateObject(objects[currentIndex].GetComponent<ObjectBehavior>().objectName);
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
