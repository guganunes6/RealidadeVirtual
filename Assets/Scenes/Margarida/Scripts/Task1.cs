using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Task {
    // Find red node

    private string id = "1"; 

    private GameObject spheres;
    GameObject firstSelectedNode;

    public Task1(GameObject spheres) : base() {
       this.spheres = spheres;
    }

    public override void Start() {
        base.Start();
        firstSelectedNode = spheres.transform.GetChild(3).gameObject;
        NodeFeedback firstTest = firstSelectedNode.AddComponent<NodeFeedback>();
    }

    public override void Stop(List<GameObject> objsHit) {
        // Stop Task
        Destroy(objsHit[0].GetComponent<Light>());
        objsHit[0].GetComponent<Renderer>().material.color = Color.white;
        Debug.Log("Time until select random node: " + Time.time);
        
        // Stop timer
        base.Stop(objsHit);

        // Update time to CSV file
        encoder.SetFirstTaskTime(timer.GetTime());
    }

    public override void StartNextTask() {
        Debug.Log("START TASK 2");
        Task task2 = new Task2(spheres, firstSelectedNode);
        Task.currentTask = task2;
        task2.Start();
    }

    public override string GetTaskId() {
        return id;
    }
}
