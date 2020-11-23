using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [HideInInspector]
    public int id;
    [HideInInspector]
    public List<Tuple<Node, int>> neighbours;
    [HideInInspector]
    public DecodedNode movie;
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public bool isMarked;
    [HideInInspector]
    public Color markedColor;

    public Node(int nodeId, DecodedNode m)
    {
        id = nodeId;
        neighbours = new List<Tuple<Node, int>>();
        movie = m;
        position = Vector3.zero;
        velocity = Vector3.zero;
        isMarked = false;
        markedColor = Color.clear;
    }

    public void AddNeighbour(Tuple<Node, int> neighbour)
    {
        neighbours.Add(neighbour);
    }

    public void LogGenres()
    {
        foreach (var item in movie.getGenres())
        {
            Debug.Log(movie.getTitle() + " " + item);
        }
    }

    public void Constructor(int nodeId, DecodedNode m)
    {
        id = nodeId;
        neighbours = new List<Tuple<Node, int>>();
        movie = m;
        position = Vector3.zero;
        velocity = Vector3.zero;
        isMarked = false;
        markedColor = Color.clear;
    }

}