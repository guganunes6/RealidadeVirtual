using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    Timer timer;
    
    void Start() {
        //Decoder decoder = new Decoder("Assets/Scenes/Margarida/Scripts/Dataset/archive/movies_metadata.csv");
        //decoder.Parse(1);

        timer = new Timer();
        CSVEncoder encoder = new CSVEncoder("tasks_time");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("a")) {
            print("A key was pressed");
            timer.Start();
        } else if (Input.GetKeyDown("s")) {
            print("S key was pressed");
            timer.Stop();
            print(timer.GetTime());
        }
    }
}
