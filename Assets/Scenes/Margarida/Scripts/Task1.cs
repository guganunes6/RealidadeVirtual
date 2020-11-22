using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : Task {

    private string id = "1"; 

    private GameObject spheres;

    public Task1(GameObject spheres) : base() {
       this.spheres = spheres;
    }

    public override void Start() {
        base.Start();
        GameObject firstSelectedNode = spheres.transform.GetChild(3).gameObject;
        NodeFeedback firstTest = firstSelectedNode.AddComponent<NodeFeedback>();
    }

    public override void Stop(List<GameObject> objsHit) {
        // Stop Task
        Destroy(objsHit[0].GetComponent<Light>());
        objsHit[0].GetComponent<Renderer>().material.color = Color.white;
        Debug.Log("Time until select random node: " + Time.time);
        
        // Stop timer
        base.Stop(objsHit);

        // Start next Task
        //Task.currentTask = task; // update currentTask
    }

    public override bool CanStop() {
        return false;
    }

    public override string GetTaskId() {
        return id;
    }
}
