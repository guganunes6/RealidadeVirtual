using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<Movie, TEdgeType>{
    public Graph(){
        Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        Dictionary<int, Edge> Edges = new Dictionary<int, Edge>();
    }

    public Dictionary<int, Node> Nodes { get; private set; }

    public Dictionary<int, Edge> Edges { get; private set; }
}
/*
public class Node{
    public int id { get; set; }

    //public Movie movie { get; set; }

    public Vector3 position { get; set; }
}*/

public class Edge
{
    public int id { get; set; }

    public Node From { get; set; }

    public Node To { get; set; }
}