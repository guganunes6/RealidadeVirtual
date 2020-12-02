using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Task : MonoBehaviour {

    public  Timer timer;
    public CSVEncoder encoder;
    public static Task currentTask;
    private bool toContinue;

    public Task(CSVEncoder encoder) {
        timer = new Timer();
        toContinue = false;
        this.encoder = encoder;
    }

    public virtual void Start() {
        timer.Start();
    }

    public virtual void StartAgain() {
        timer.StartAgain();
    }

    public virtual void StopTimer()
    {
        timer.Stop();
        toContinue = true;
        Debug.Log("Task " + GetTaskId() + " took " + timer.GetTime() + " seconds. Press ENTER to continue");
    }

    public bool ToContinue() {
        return toContinue;
    }

    public void AddTime(double time) {
        encoder.AddTime(time);
    }

    public void PrintTimes() {
        encoder.PrintTimes();
    }

    public abstract void StopTask();

    public abstract void SelectNode(GameObject objHit);

    public abstract void StartNextTask();

    public abstract string GetTaskId();

}
