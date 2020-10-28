using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeFeedback2 : MonoBehaviour
{

    private Light light;
    private float maxIntensity = 1f;
    private float minIntensity = 0f;
    private float pulseSpeed = 1f;
    private float targetIntensity = 10f;
    private float currentIntensity;

    void Start()
    {
        Debug.Log("Started by: " + gameObject);
        light = gameObject.AddComponent<Light>();
        light.color = Color.yellow;

        SerializedObject haloName = new SerializedObject(gameObject.GetComponent<Light>());
        haloName.FindProperty("m_DrawHalo").boolValue = true;
        haloName.ApplyModifiedProperties();
    }


    void Update()
    {
        if (light != null)
        {
            currentIntensity = Mathf.MoveTowards(light.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
            if (currentIntensity >= maxIntensity)
            {
                currentIntensity = maxIntensity;
                targetIntensity = minIntensity;
            }
            else if (currentIntensity <= minIntensity)
            {
                currentIntensity = minIntensity;
                targetIntensity = maxIntensity;
            }
            light.intensity = currentIntensity;
        }
    }
}
