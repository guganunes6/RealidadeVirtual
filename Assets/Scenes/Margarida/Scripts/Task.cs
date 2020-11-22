using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Task : MonoBehaviour {

    private Timer timer;
    private CSVEncoder encoder;
    public static Task currentTask;

    public Task() {
        timer = new Timer();
        //encoder = new CSVEncoder("tasks_time");
    }

    public virtual void Start() {
        timer.Start();
    }

    public virtual void Stop(List<GameObject> objsHit) {
        timer.Stop();
        
        // Do input stuff
        Debug.Log("Finishing task " + GetTaskId() + ", press ENTER to continue");
        StartCoroutine("Wait");
        /*while (true) {
            while (!Input.GetKeyDown(KeyCode.Space)) yield return null;
            
            Debug.Log("Hit ENTER"); 
            yield return null;
        }*/
        Debug.Log("sisdjfb");
    }

    public static Task GetCurrentTask() {
        return currentTask;
    }

    public abstract bool CanStop();

    public abstract string GetTaskId();
}
