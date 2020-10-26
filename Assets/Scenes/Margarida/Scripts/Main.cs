using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    
    void Start() {
        Decoder decoder = new Decoder("Assets/Scenes/Margarida/Scripts/Dataset/archive/movies_metadata.csv");
        decoder.Parse(2);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
