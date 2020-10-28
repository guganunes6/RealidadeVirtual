using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public int NumberOfMovies;
    void Start() {
        var decoder = new Decoder("Assets/Scenes/Margarida/Scripts/Dataset/archive/movies_metadata.csv");
        gameObject.GetComponent<GraphManager>().InitializeGraphManager(decoder.Parse(NumberOfMovies));
    }
}
