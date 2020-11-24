using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Task3 : Task {
    // Find all nodes with a certain genre, from a selected node

    private string id = "3"; 

    private GameObject spheres;

    private GameObject mainSphere;

    private string goalGenre;

    private List<GameObject> selectedNodes;

    private List<GameObject> goalNodes;

    public Task3(CSVEncoder encoder, GameObject spheres) : base(encoder) {
        this.spheres = spheres;
    }

    private List<GameObject> ComputeGoalNodes(GameObject spheres, string goalGenre) {
        List<GameObject> goalSpheres = new List<GameObject>();
        foreach (Transform sphere in spheres.transform) {
            if (sphere.gameObject.GetComponent<Node>().movie != null) {
                if(sphere.gameObject.GetComponent<Node>().movie.getGenres().Contains(goalGenre)) {
                    goalSpheres.Add(sphere.gameObject);
                    sphere.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                }
            }
        }
        return goalSpheres;
    }

    public void SelectNode(GameObject hit) {
        if (hit != null) {
            string hitTitle = hit.GetComponent<Node>().movie.getOriginalTitle();
            if (!selectedNodes.Where(n => n.GetComponent<Node>().movie.getOriginalTitle() == hitTitle).Any()) {
                if (goalNodes.Where(n => n.GetComponent<Node>().movie.getOriginalTitle() == hitTitle).Any()) {
                    selectedNodes.Add(hit);
                    hit.GetComponent<Renderer>().material.color = Color.white;
                    Debug.Log((goalNodes.Count - selectedNodes.Count) + " nodes remaining.");
                }
            }
        }
    }

    public override void Start() {
        base.Start();

        mainSphere = spheres.transform.GetChild(2).gameObject;
        mainSphere.GetComponent<Renderer>().material.color = Color.red;
        
        goalGenre = mainSphere.GetComponent<Node>().movie.getGenres()[0]; // Only counts the first genre
        goalNodes = ComputeGoalNodes(spheres, goalGenre);
        
        selectedNodes = new List<GameObject>();
    }

    public bool TaskIsComplete() {
        return selectedNodes.Count == goalNodes.Count;
    }
    public override void StopTask(GameObject objsHit) {
        SelectNode(objsHit);
        if (TaskIsComplete()) {
            // Stop Task
            mainSphere.GetComponent<Renderer>().material.color = Color.white;

            // Stop timer
            base.StopTimer();
            Debug.Log("Test complete. Thank you.");

            // Update time to CSV file
            encoder.SetThirdTaskTime(timer.GetTime());
            encoder.UpdateFile();
        }
    }

    public override void StartNextTask() {
        // Finish
    }

    public override string GetTaskId() {
        return id;
    }
}
