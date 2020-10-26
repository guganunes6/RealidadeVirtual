using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph<TNodeType, TEdgeType>{
    public Graph(){
        Nodes = new List<Node<TNodeType>>();
        Edges = new List<Edge<TEdgeType, TNodeType>>();
    }

    public List<Node<TNodeType>> Nodes { get; private set; }

    public List<Edge<TEdgeType, TNodeType>> Edges { get; private set; }
}

public class Node<TNodeValue>{
    public Color NodeColor { get; set; }

    public TNodeValue Value { get; set; }
}

public class Edge<TEdgeValue, TNodeType>
{
    public Color EdgeColor { get; set; }

    public TEdgeValue Value { get; set; }

    public Node<TNodeType> From { get; set; }

    public Node<TNodeType> To { get; set; }

}