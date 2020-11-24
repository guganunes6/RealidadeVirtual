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
        CSVEncoder encoder = new CSVEncoder("tasks_time");
        Task task = new Task1(encoder, spheres);
        Task.currentTask = task; 
        task.Start();
    }


    void Update()
    {
        if(Task.currentTask.ToContinue() & Input.GetKeyDown(KeyCode.Return)) {
            Task.currentTask.StartNextTask();
        } else if(Task.currentTask.GetTaskId() == "2" & Input.GetKeyDown(KeyCode.Return)) {
            // Might not have been the right node selected 
            Task.currentTask.StopTask(null);
        } else if(Task.currentTask.GetTaskId() == "3" & Input.GetKeyDown(KeyCode.Return)) {
            // Might not have been the right node selected 
            Task.currentTask.StopTask(null);
        }
    }
}
