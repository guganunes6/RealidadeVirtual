using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Task2 : Task {
    // Find neighbour node with highest classification in that movie genre

    private string id = "2"; 

    private int nTrials = 5;
    private int trial = 0;
    private int[] nodes = {3, 7, 4, 185, 101};

    private GameObject spheres;
    private GameObject goalSphere;

    public Task2(CSVEncoder encoder, GameObject spheres) : base(encoder) {
        this.spheres = spheres;
    }

    public override void Start() {
        base.Start();

        // Iluminate main node
        IluminateNode(trial);

        // Get neighbour with highest classification
        DefineHighestClassificationNeigh(trial);
    }

    public void Start(int node) {
        base.StartAgain();

        // Iluminate main node
        IluminateNode(node);

        // Get neighbour with highest classification
        DefineHighestClassificationNeigh(node);
    }

    private void DefineHighestClassificationNeigh(int nodeIndex) {
        GameObject sphere = spheres.transform.GetChild(nodes[nodeIndex]).gameObject;
        Node mainNode = sphere.GetComponent<Node>();
        //List<Node> neighbours = mainNode.neighbours.Where(n => n.Item2 > 0).Select(n => n.Item1).ToList();
        List<Node> neighbours = mainNode.neighbours.Where(n => AreConnected(mainNode, n)).Select(n => n.Item1).ToList();
        //foreach (var n in neighbours)
        //{
        //    Debug.Log("n: " + n.movie.getOriginalTitle());
        //}
        Node highestClassifiedNeighbour = neighbours.OrderByDescending(n => n.movie.getVoteAverage()).First();
        goalSphere = highestClassifiedNeighbour.gameObject;

        goalSphere.GetComponent<Renderer>().material.color = Color.blue;
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
        goalSphere.GetComponent<Renderer>().material.color = Color.white;
    }

    public override void SelectNode(GameObject objHit) {
        Debug.Log("Select Node " + trial);
        if (objHit == goalSphere & trial < nTrials-1) {
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
        Debug.Log("START TASK 3");
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

    public bool AreConnected(Node node, Tuple<Node, int> neighbour)
    {
        return neighbour.Item2 == node.GetAmountOfGenres() && neighbour.Item1.GetAmountOfGenres() == node.GetAmountOfGenres();
    }
}
