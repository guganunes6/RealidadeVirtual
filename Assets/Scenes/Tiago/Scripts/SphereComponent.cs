using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereComponent : MonoBehaviour
{

    public GameObject prefab;
    public int numberOfObjects = 3;


    void Start()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            Quaternion rot = new Quaternion();

            Instantiate(prefab, pos, rot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
