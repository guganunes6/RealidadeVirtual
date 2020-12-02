using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Task {
    // Find N red nodes

    private int nTrials = 15;
    private int trial = 0;
    private int[] nodes = {3, 7, 4, 13, 8, 10, 19, 17, 6, 4, 5, 12, 15, 1, 9};

    private string id = "1"; 

    private GameObject spheres;

    public Task1(CSVEncoder encoder, GameObject spheres) : base(encoder) {
       this.spheres = spheres;
    }

    public override void Start() {
        base.Start();
        IluminateNode(trial);
    }

    public void Start(int node) {
        base.StartAgain();
        IluminateNode(node);
    }

    private void IluminateNode(int nodeIndex) {
        Debug.Log("Iluminate Node " + nodes[nodeIndex]);
        GameObject node = spheres.transform.GetChild(nodes[nodeIndex]).gameObject;

        node.GetComponent<Renderer>().material.color = Color.red;
        node.AddComponent<NodeFeedback>();
    }

    private void TurnOffNode(int nodeIndex) {
        GameObject node = spheres.transform.GetChild(nodes[nodeIndex]).gameObject;

        Destroy(node.GetComponent<NodeFeedback>());
        Destroy(node.GetComponent<Light>());
        node.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void SelectNode(GameObject objHit) {
        Debug.Log("Select Node");
        if (objHit.GetComponent<Light>() != null & trial < nTrials-1) {
            // Finish trial
            Debug.Log("Right node selected");
            TurnOffNode(trial);
            StopTask();

            // Start next trial
            Start(++trial);
        } else if (trial == nTrials-1) {
            // Finish last trial
            TurnOffNode(trial);
            StopTask();
            PrintTimes();
            
            // Start next Task
            toContinue = true;
            Debug.Log("Task " + GetTaskId() + " took " + timer.GetTime() + " seconds. Press ENTER to continue");
        } else {
            wrongNodes++;
        }
    }

    public override void StopTask() {
        // Stop timer
        base.StopTimer();

        // Update time to CSV file
        AddTime(timer.GetTime());
    }

    public override void StartNextTask() {
        Debug.Log("START TASK 2");
        Task task2 = new Task2(encoder, spheres);
        Task.currentTask = task2;
        task2.Start();
    }

    public override string GetTaskId() {
        return id;
    }
}
