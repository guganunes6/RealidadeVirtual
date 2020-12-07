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

    [HideInInspector]
    public Vector3 forceVector;
    [HideInInspector]
    public Vector3 dispVector;
    public void NodeConstructor(int nodeId, DecodedNode m)
    {
        id = nodeId;
        neighbours = new List<Tuple<Node, int>>();
        movie = m;
        position = Vector3.zero;
        velocity = Vector3.zero;
        isMarked = false;
        markedColor = Color.clear;
        forceVector = Vector3.zero;
        dispVector = Vector3.zero;
    }
    public void AddNeighbour(Tuple<Node, int> neighbour)
    {
        neighbours.Add(neighbour);
    }

    public int GetAmountOfGenres()
    {
        return movie.getGenres().Count;
    }

    public void LogGenres()
    {
        foreach (var item in movie.getGenres())
        {
            Debug.Log(movie.getTitle() + " " + item);
        }
    }

    public void setPosition(Vector3 newPosition)
    {
        position = newPosition;
        gameObject.transform.position = newPosition;
    }
}