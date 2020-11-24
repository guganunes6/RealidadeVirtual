using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Task2 : Task {
    // Find neighbour node with highest classification in that movie genre

    private string id = "2"; 

    private GameObject spheres;

    private GameObject mainSphere;

    private GameObject goalSphere;

    public Task2(CSVEncoder encoder, GameObject spheres, GameObject mainSphere) : base(encoder) {
        this.spheres = spheres;
        this.mainSphere = mainSphere;
    }

    public override void Start() {
        base.Start();
        Node mainNode = mainSphere.GetComponent<Node>();
        //Debug.Log(mainNode.movie.getOriginalTitle());
        List<Node> neighbours = mainNode.neighbours.Where(n => n.Item2 > 0).Select(n => n.Item1).ToList();
        Node highestClassifiedNeighbour = neighbours.OrderByDescending(n => n.movie.getVoteAverage()).First();
        goalSphere = highestClassifiedNeighbour.gameObject;
        //goalSphere.GetComponent<Renderer>().material.color = Color.blue;
        //Debug.Log(highestClassifiedNeighbour.movie.getOriginalTitle());
    }

    //public override void Stop(List<GameObject> objsHit) {
    //    // Stop Task
    //    mainSphere.GetComponent<Renderer>().material.color = Color.white;
        
    //    // Stop timer
    //    base.Stop(objsHit);

    //    // Update time to CSV file
    //    encoder.SetSecondTaskTime(timer.GetTime());
    //}

    public override void StopTask(GameObject objHit)
    {
        if (objHit == goalSphere)
        {
            // Stop Task
            mainSphere.GetComponent<Renderer>().material.color = Color.white;

            // Stop timer
            base.StopTimer();

            // Update time to CSV file
            encoder.SetSecondTaskTime(timer.GetTime());
        }
    }

    public override void StartNextTask() {
        Task task3 = new Task3(encoder, spheres);
        Task.currentTask = task3;
        task3.Start();
    }

    public override string GetTaskId() {
        return id;
    }

    public GameObject GetGoalSphere() {
        return goalSphere;
    }
}
