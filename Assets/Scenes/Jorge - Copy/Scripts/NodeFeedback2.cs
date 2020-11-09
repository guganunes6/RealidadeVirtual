using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeFeedback2 : MonoBehaviour
{
    private Light light;
    private float maxRange = 25f;
    private float minRange = 5f;
    private float radiusSpeed = 10f;
    private float targetRadius = 25f;
    private float currentRadius;

    void Start()
    {
        //Debug.Log("Started by: " + gameObject);
        light = gameObject.AddComponent<Light>();
        light.color = Color.red;
        light.intensity = 20;
    }


    void Update()
    {
        if (light != null)
        {
            currentRadius = Mathf.MoveTowards(light.range, targetRadius, Time.deltaTime * radiusSpeed);
            if (currentRadius >= maxRange)
            {
                currentRadius = maxRange;
                targetRadius = minRange;
            }
            else if (currentRadius <= minRange)
            {
                currentRadius = minRange;
                targetRadius = maxRange;
            }
            light.range = currentRadius;
        }
    }
}
