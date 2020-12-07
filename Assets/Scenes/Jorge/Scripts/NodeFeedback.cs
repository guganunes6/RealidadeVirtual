using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeFeedback : MonoBehaviour
{
    private Light light;
    private float maxRange = 12f;
    private float minRange = 2f;
    private float radiusSpeed = 10f;
    private float targetRadius = 50f;
    private float currentRadius;

    void Start()
    {
        //Debug.Log("Started by: " + gameObject);
        light = gameObject.AddComponent<Light>();
        light.color = Color.red;
        light.intensity = 10;

        gameObject.GetComponent<Renderer>().material.color = Color.red;
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
