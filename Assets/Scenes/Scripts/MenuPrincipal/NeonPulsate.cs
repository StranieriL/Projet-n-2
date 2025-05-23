using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NeonPulse : MonoBehaviour
{
    public Light2D light2D;
    public float speed = 2f;
    public float intensityMin = 0.5f;
    public float intensityMax = 1.5f;

    void Update()
    {
        float intensity = Mathf.Lerp(intensityMin, intensityMax, Mathf.PingPong(Time.time * speed, 1));
        light2D.intensity = intensity;
    }
}