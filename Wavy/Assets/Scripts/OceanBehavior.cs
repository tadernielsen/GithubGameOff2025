using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanBehavior : MonoBehaviour
{
    private class Wave
    {
        public Vector3 position;
        public float radius;
        public float time;
        public float maxRadius;

        public Wave(Vector3 pos, float time, float maxRadius)
        {
            this.position = pos;
            this.radius = 0.0f;
            this.time = time;
            this.maxRadius = maxRadius;
        }
    }

    [Header("Wave Settings")]
    [SerializeField] private float waveWidth = 1.0f;
    [SerializeField] private float waveSpeed = 1.0f;
    [SerializeField] private float waveStrength = 1.0f;

    private List<Wave> currentWaves = new List<Wave>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWave();
    }

    private void CreateWave(Vector3 pos, float radius)
    {
        Debug.Log("Creating Wave");
        Vector3 wavePostion = new Vector3(pos.x, transform.position.y, pos.z);
        currentWaves.Add(new Wave(wavePostion, Time.time, radius));
    }

    private void UpdateWave()
    {
        foreach (Wave wave in currentWaves)
        {
            wave.radius += waveSpeed * Time.deltaTime;

            if (wave.radius > wave.maxRadius)
            {
                currentWaves.Remove(wave);
                break;
            }

            applyWaveForces(wave);
        }
    }

    private void applyWaveForces(Wave wave)
    {
        Collider[] hitColliders = Physics.OverlapSphere(wave.position, wave.radius + waveWidth);

        foreach (Collider hitCollider in hitColliders)
        {
            Vector3 objPos = hitCollider.transform.position;
            float distanceToCenter = Vector3.Distance(new Vector3(wave.position.x, wave.position.y, wave.position.z), objPos);

            float distanceFromWaveEdge = Mathf.Abs(distanceToCenter - wave.radius);

            if (distanceFromWaveEdge <= waveWidth)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    ApplyObjectForce(rb, wave, distanceFromWaveEdge);
                }
            }
        }
    }
    
    private void ApplyObjectForce(Rigidbody rb, Wave wave, float distanceFromWaveEdge)
    {
        Vector3 objPos = rb.position;
        Vector3 direction = new Vector3(objPos.x - wave.position.x, 0, objPos.z - wave.position.z).normalized;

        Vector3 force = direction * waveStrength;

        rb.AddForce(force, ForceMode.Force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Object")
        {
            return;
        }

        Vector3 contact = collision.contacts[0].point;
        ObjectBehavior obj = collision.gameObject.GetComponent<ObjectBehavior>();

        CreateWave(contact, obj.radius);
    }
}
