using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanBehavior : MonoBehaviour
{
    private float waveEquationHelper;
    private float waveEquation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void calculateWave(float mass, float height)
    {
        waveEquationHelper = mass + 9.8f + height;
        waveEquation = Mathf.Sqrt(waveEquationHelper);
        Debug.Log(waveEquation);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Object")
        {
            return;
        }

        Vector3 contact = collision.contacts[0].point;
    }
}
