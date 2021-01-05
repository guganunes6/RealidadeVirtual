using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Task : MonoBehaviour {

    public  Timer timer;
    public CSVEncoder encoder;
    public static Task currentTask;
    public bool toContinue;
    public int wrongNodes;

    public Task() {

    }

    public Task(CSVEncoder encoder) {
        timer = new Timer();
        toContinue = false;
        this.encoder = encoder;
        wrongNodes = 0;
    }

    public virtual void Start() {
        timer.Start();
    }

    public virtual void StartAgain() {
        timer.StartAgain();
    }

    public virtual void StopTimer() {
        timer.Stop();
    }

    public bool ToContinue() {
        return toContinue;
    }

    public void AddFinalTaskTime(int numTrials) {
        encoder.AddFinalTaskTime(numTrials);
    }

    public void AddTime(double time) {
        encoder.AddTime(time, wrongNodes);
        wrongNodes = 0;
    }

    public void PrintTimes() {
        encoder.PrintTimes();
    }

    public abstract void StopTask();

    public abstract void SelectNode(GameObject objHit);

    public abstract void StartNextTask();

    public abstract string GetTaskId();

}
