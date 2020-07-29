using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLight : MonoBehaviour
{
    public float speed;
    public float minIntensity;
    public float maxIntensity;
    public UnityEngine.Experimental.Rendering.Universal.Light2D neonLight;

    float startTime;

    void Start()
    {
        startTime = Time.time;
        neonLight = transform.GetChild(0).GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    private void Update()
    {
        LightControl();
    }

    public void LightControl()
    {
        float t = (Mathf.Sin(Time.time - startTime) * speed);
        neonLight.intensity = Mathf.Lerp(maxIntensity, minIntensity, t);
    }
}
