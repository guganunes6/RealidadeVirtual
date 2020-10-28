using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public int NumberOfMovies;
    private Decoder decoder;
    void Start() {
        decoder = new Decoder("Assets/Scenes/Margarida/Scripts/Dataset/archive/movies_metadata.csv");
    }

    public Dictionary<string, DecodedNode> GetMovies()
    {
        return decoder.Parse(NumberOfMovies);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
