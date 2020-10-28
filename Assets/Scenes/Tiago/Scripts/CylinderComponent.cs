using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cylinderGameObject;
    public int numberOfObjects = 3;

    void Start()
    {
        //gameObject Spheres parent
        GameObject cylinders = GameObject.Find("Cylinders");

        GameObject spheres = GameObject.Find("Spheres");

        var sphereStart = spheres.transform.GetChild(1).position;

        var sphereEnd = spheres.transform.GetChild(2).position;

        Debug.Log(spheres.transform.GetChild(0).position);
        Debug.Log(spheres.transform.GetChild(2).position);



        GameObject cylinder = Instantiate(cylinderGameObject, new Vector3(0, 0, 0), Quaternion.identity);

        //Position
        cylinder.transform.position = (sphereEnd + sphereStart) * 0.5f;
        Debug.Log(cylinder.transform.position);

        var v3T = cylinder.transform.localScale;      // Scale it
        //v3T.y = (sphereEnd - sphereStart).magnitude;


        cylinder.transform.localScale = v3T;

        cylinder.transform.rotation = Quaternion.FromToRotation(Vector3.up, sphereEnd - sphereStart);

        cylinder.transform.parent = cylinders.transform;
        /*
        for (int i = 0; i < numberOfObjects; i++)
        {

            //gameObject Cylinder children
            GameObject cylinder = Instantiate(cylinderGameObject, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity);

            cylinder.transform.parent = cylinders.transform;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
