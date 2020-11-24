using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Task3 : Task {
    // Find all nodes with a certain genre, from a selected node

    private string id = "3"; 

    private GameObject spheres;

    public Task3(CSVEncoder encoder, GameObject spheres) : base(encoder) {
        this.spheres = spheres;
    }

    public override void Start() {
        base.Start();

    }

    //public override void Stop(List<GameObject> objsHit) {
    //    // Stop Task

    //    // Stop timer
    //    base.Stop(objsHit);

    //    // Update time to CSV file
    //    encoder.SetThirdTaskTime(timer.GetTime());
    //}
    public override void StopTask(GameObject objsHit)
    {
        // Stop Task

        // Stop timer
        base.StopTimer();

        // Update time to CSV file
        encoder.SetThirdTaskTime(timer.GetTime());
    }

    public override void StartNextTask() {
        // Finish
    }

    public override string GetTaskId() {
        return id;
    }
}
