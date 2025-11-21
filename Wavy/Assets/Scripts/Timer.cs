using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool timerActive = false;
    private float timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!timerActive) return;

        timeElapsed += Time.deltaTime;
    }

    public float GetTime()
    {
        return timeElapsed;
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
