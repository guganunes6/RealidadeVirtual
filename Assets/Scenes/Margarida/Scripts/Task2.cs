using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2 : Task {
    // Find neighbour node with highest classification in that movie genre

    private string id = "2"; 

    private GameObject spheres;

    private GameObject mainSphere;

    public Task2(GameObject spheres, GameObject mainSphere) : base() {
        Debug.Log("Task2 constructor");
        this.spheres = spheres;
        this.mainSphere = mainSphere;
    }

    public override void Start() {
        base.Start();
        
    }

    public override void Stop(List<GameObject> objsHit) {
        // Stop Task
        
        
        // Stop timer
        base.Stop(objsHit);

        // Update time to CSV file
        encoder.SetSecondTaskTime(timer.GetTime());
    }

    public override void StartNextTask() {
        //Task task3 = new Task3(spheres);
    }

    public override string GetTaskId() {
        return id;
    }
}
