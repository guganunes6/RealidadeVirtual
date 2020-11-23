using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Task : MonoBehaviour {

    public  Timer timer;
    public CSVEncoder encoder;
    public static Task currentTask;
    private bool toContinue;

    public Task() {
        timer = new Timer();
        encoder = new CSVEncoder("tasks_time");
        toContinue = false;
    }

    public virtual void Start() {
        timer.Start();
    }

    public virtual void Stop(List<GameObject> objsHit) {
        timer.Stop();
        toContinue = true;
        Debug.Log("Finishing task " + GetTaskId() + ", press ENTER to continue");
    }

    public bool ToContinue() {
        return toContinue;
    }

    public abstract void StartNextTask();

    public abstract string GetTaskId();

}
