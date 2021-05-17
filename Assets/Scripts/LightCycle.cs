using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    Light directionalLight;
    float changeIntensity;
    float maxBrightness = 1.25f;

    void Start()
    {
        directionalLight = this.GetComponent<Light>();
        changeIntensity = maxBrightness / 720f;
    }

    public void UpdateLight(float clockTime)
    {
        float darkestHour = 3f;
        float darkestMinute = darkestHour * 60f;
        float brightestMinute = darkestHour * 60f + 1440 / 2;
        float time;
        if(clockTime > darkestMinute && clockTime < brightestMinute)
            time = clockTime - darkestMinute;
        else
        {
            if(clockTime < darkestMinute)
                time = darkestMinute - clockTime;
            else
                time = 1440 + darkestMinute - clockTime;
        }
        if (time * changeIntensity > maxBrightness * 1.1)
            directionalLight.intensity = 0f;
        else
            directionalLight.intensity = time * changeIntensity;
        float color = directionalLight.intensity / maxBrightness;
        directionalLight.color = new Color(color, color, color);
    }
}
