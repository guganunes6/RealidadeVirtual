using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager2 : MonoBehaviour
{

    private GameObject spheres;

    void Start()
    {
        spheres = GameObject.Find("Spheres");
        int numberSpheres = spheres.transform.childCount;
        int random = Random.Range(0, numberSpheres);
        GameObject firstSelectedNode = spheres.transform.GetChild(random).gameObject;
        NodeFeedback firstTest = firstSelectedNode.AddComponent<NodeFeedback>();
    }


    void Update()
    {
        
    }
}
