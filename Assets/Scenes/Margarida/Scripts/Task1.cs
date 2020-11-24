using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Task {
    // Find red node

    private string id = "1"; 

    private GameObject spheres;
    GameObject firstSelectedNode;

    public Task1(CSVEncoder encoder, GameObject spheres) : base(encoder) {
       this.spheres = spheres;
    }

    public override void Start() {
        base.Start();
        firstSelectedNode = spheres.transform.GetChild(3).gameObject;
        NodeFeedback firstTest = firstSelectedNode.AddComponent<NodeFeedback>();
    }

    public override void StopTask(GameObject objHit)
    {
        if (objHit.GetComponent<Light>() != null)
        {
            // Stop Task
            Destroy(objHit.GetComponent<Light>());

            // Stop timer
            base.StopTimer();

            // Update time to CSV file
            encoder.SetFirstTaskTime(timer.GetTime());
        }
    }

    public override void StartNextTask() {
        Debug.Log("START TASK 2");
        Task task2 = new Task2(encoder, spheres, firstSelectedNode);
        Task.currentTask = task2;
        task2.Start();
    }

    public override string GetTaskId() {
        return id;
    }
}
