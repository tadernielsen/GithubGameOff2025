using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public GameObject[] objects = new GameObject[1];
    public Camera cam;
    public GameObject testClick;
    public float clickDelay = 0.5f;

    private bool canClick = true;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Ray mouseRay = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hit, 15f) && canClick)
        {
            if (hit.collider.gameObject.tag == "Water")
            {
                Instantiate(objects[0], hit.point + new Vector3(0, 2, 0), Quaternion.identity);
                StartCoroutine("delay");
            }
        }
    }

    IEnumerator delay()
    {
        canClick = false;
        yield return new WaitForSeconds(clickDelay);
        canClick = true;
    }
}
