using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Timer {

    Stopwatch stopwatch;

    public Timer() {
        stopwatch = new Stopwatch();
    }

    private void Reset() {
        stopwatch.Reset();
    }

    // Resets timer
    public void StartAgain() {
        Reset();
        stopwatch.Start();
    }

    // Continue 
    public void Start() {
        stopwatch.Start();
    }

    public void Stop() {
        stopwatch.Stop();
    }

    public long GetTime() {
        return stopwatch.ElapsedMilliseconds;
    }

}
