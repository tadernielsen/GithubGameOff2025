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
        public float waveSize;

        public Wave(Vector3 pos, float time, float size)
        {
            this.position = pos;
            this.radius = 0.0f;
            this.time = time;
            this.maxRadius = size * 2;
            this.waveSize = size;
        }
    }

    [Header("Wave Settings")]
    [SerializeField] private float waveWidth = 1.0f;
    [SerializeField] private float waveSpeed = 1.0f;
    [SerializeField] private float waveStrength = 1.0f;

    [SerializeField] private LayerMask boatLayer;
    private List<Wave> currentWaves = new List<Wave>();

    // Update is called once per frame
    void Update()
    {
        UpdateWave();
    }

    private void CreateWave(Vector3 pos, float size)
    {
        Debug.Log("Creating Wave");
        Vector3 wavePostion = new Vector3(pos.x, transform.position.y, pos.z);
        currentWaves.Add(new Wave(wavePostion, Time.time, size));
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
        Collider[] hitColliders = Physics.OverlapSphere(wave.position, wave.radius + waveWidth, boatLayer);

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

        float newWaveStrength = waveStrength * (wave.waveSize / 5.0f);
        Vector3 force = direction * newWaveStrength * (1.0f - (distanceFromWaveEdge / waveWidth));

        rb.AddForce(force, ForceMode.Force);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Object")
        {
            return;
        }

        Vector3 contact = other.ClosestPoint(transform.position);
        ObjectBehavior obj = other.gameObject.GetComponent<ObjectBehavior>();

        CreateWave(contact, obj.waveSize);
    }
}
