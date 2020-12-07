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
        this.goalNodes = new List<GameObject>();
        this.selectedNodes = new List<GameObject>();
    }

    private List<GameObject> ComputeGoalNodes(GameObject spheres, string goalGenre) {
        List<GameObject> goalSpheres = new List<GameObject>();
        //foreach (Transform sphere in spheres.transform) {
        //    if (sphere.gameObject.GetComponent<Node>().movie != null) {
        //        if (sphere.gameObject.GetComponent<Node>().movie.getGenres().Contains(goalGenre))
        //        {
        //            goalSpheres.Add(sphere.gameObject);
        //            sphere.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        //        }
        //    }
        //}
        foreach (var neighbour in mainSphere.gameObject.GetComponent<Node>().neighbours)
        {
            if (AreConnected(mainSphere.gameObject.GetComponent<Node>(), neighbour))
            {
                goalSpheres.Add(neighbour.Item1.gameObject);
            }
        }
        return goalSpheres;
    }

    public override void Start() {
        base.Start();

        mainSphere = spheres.transform.GetChild(160).gameObject;
        mainSphere.AddComponent<NodeFeedback>();

        mainSphere.GetComponent<Renderer>().material.color = Color.red;
        
        goalGenre = mainSphere.GetComponent<Node>().movie.getGenres()[0]; // Only counts the first genre
        goalNodes = ComputeGoalNodes(spheres, goalGenre);
        Debug.Log(goalNodes.Count() + " nodes remaining");
        
        selectedNodes = new List<GameObject>();
    }

    public bool TaskIsComplete() {
        return selectedNodes.Count == goalNodes.Count;
    }

    public override void StopTask() {
        // Stop timer
        base.StopTimer();

        // Update time to CSV file
        AddTime(timer.GetTime());
        encoder.UpdateFile();
    }

    private void TurnOffNode(GameObject node) {
        Destroy(node.GetComponent<NodeFeedback>());
        Destroy(node.GetComponent<Light>());
        node.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void SelectNode(GameObject objHit) {
        if (selectedNodes.Count() < goalNodes.Count() & goalNodes.Contains(objHit) & !selectedNodes.Contains(objHit)) {
            TurnOffNode(objHit);
            selectedNodes.Add(objHit);
            Debug.Log((goalNodes.Count() - selectedNodes.Count()) + " nodes remaining");
            if (selectedNodes.Count() == goalNodes.Count()) {
                StopTask();
                PrintTimes();
            }
        } else if (!goalNodes.Contains(objHit)) {
            wrongNodes++;
            Debug.Log("wrong nodes " + wrongNodes);
        }
    }

    public override void StartNextTask() {
        // Do nothing - Finish
    }

    public override string GetTaskId() {
        return id;
    }

    bool AreConnected(Node node, Tuple<Node, int> neighbour)
    {
        return neighbour.Item2 == node.GetAmountOfGenres() && neighbour.Item1.GetAmountOfGenres() == node.GetAmountOfGenres();
    }
}
