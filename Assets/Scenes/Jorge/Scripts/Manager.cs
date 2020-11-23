using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    private GameObject spheres;

    void OnEnable()
    {
        spheres = GameObject.Find("Spheres");
        int numberSpheres = spheres.transform.childCount;
        //int random = Random.Range(0, numberSpheres);
        //GameObject firstSelectedNode = spheres.transform.GetChild(random).gameObject;
        //NodeFeedback firstTest = firstSelectedNode.AddComponent<NodeFeedback>();
        Task task = new Task1(spheres);
        Task.currentTask = task; 
        task.Start();
    }


    void Update()
    {
        if(Task.currentTask.ToContinue() & Input.GetKeyDown(KeyCode.Space)) {
            Task.currentTask.StartNextTask();
        }
    }
}
