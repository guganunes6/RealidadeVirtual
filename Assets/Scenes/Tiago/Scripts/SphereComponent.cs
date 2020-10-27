using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereComponent : MonoBehaviour
{

    public GameObject sphereGameObject;
    public int numberOfObjects = 20;


    void Start()
    {
        //gameObject Spheres parent
        GameObject spheres = GameObject.Find("Spheres");

        for (int i = 0; i < numberOfObjects; i++)
        {

            //gameObject Sphere children
            GameObject sphere = Instantiate(sphereGameObject, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)) , Quaternion.identity);

            sphere.transform.parent = spheres.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
