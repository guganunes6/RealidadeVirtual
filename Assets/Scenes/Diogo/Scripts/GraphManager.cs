using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public Dictionary<int, Node> nodes;

    public float connectedNodeForce;
    public float disconnectedNodeForce;
    public float minConnectedDistance;

    private void Start()
    {
        nodes = new Dictionary<int, Node>();
        GenerateDummyNodes();
        CalculateNeighbours();
        //foreach (var node in nodes.Values)
        //{
        //    Debug.Log("node" + node.id + ": ");
        //    foreach (var n in node.nodesForce)
        //    {
        //        Debug.Log("neighbour: " + n.Item1.id);
        //    }
        //}
        GenerateGraph();
    }

    private void Update()
    {
        //GenerateGraph();
        //foreach (var node in nodes.Values)
        //{
        //    node.position += node.velocity * Time.deltaTime;
        //    Debug.Log(node.id);
        //}
    }

    private void OnDrawGizmos()
    {
        foreach (var node in nodes.Values)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(node.position, 0.2f);
            Gizmos.color = Color.green;
            foreach (var otherNode in node.nodesForce)
            {
                if (otherNode.Item2 > 0)
                {
                    //Debug.Log("n" + node.id + " " + "n" + otherNode.Item1.id);
                    Gizmos.DrawLine(node.position, otherNode.Item1.position);
                }
            }
        }
    }
    public void GenerateDummyNodes()
    {
        nodes.Add(0, new Node(new DummyMovie(0, "0A", new string[1] { "A" })));
        nodes.Add(1, new Node(new DummyMovie(1, "1A", new string[1] { "B" })));
        nodes.Add(2, new Node(new DummyMovie(2, "2A", new string[2] { "A", "B" })));
        nodes.Add(3, new Node(new DummyMovie(3, "3A", new string[2] { "A", "B" })));
        nodes.Add(4, new Node(new DummyMovie(4, "4A", new string[2] { "C", "B" })));
        nodes.Add(5, new Node(new DummyMovie(5, "5A", new string[4] { "A", "B", "C" , "D" })));
        nodes.Add(6, new Node(new DummyMovie(6, "6A", new string[2] { "D", "B" })));
        nodes.Add(7, new Node(new DummyMovie(7, "7A", new string[3] { "A", "B", "D" })));
        nodes.Add(8, new Node(new DummyMovie(8, "8A", new string[2] { "D", "C" })));
        nodes.Add(9, new Node(new DummyMovie(9, "9A", new string[1] { "C" })));
        nodes.Add(10, new Node(new DummyMovie(10, "0A", new string[1] { "A" })));
        nodes.Add(11, new Node(new DummyMovie(11, "1A", new string[1] { "B" })));
        nodes.Add(22, new Node(new DummyMovie(22, "2A", new string[2] { "A", "B" })));
        nodes.Add(33, new Node(new DummyMovie(33, "3A", new string[2] { "A", "B" })));
        nodes.Add(44, new Node(new DummyMovie(44, "4A", new string[2] { "C", "B" })));
        nodes.Add(55, new Node(new DummyMovie(55, "5A", new string[4] { "A", "B", "C", "D" })));
        nodes.Add(66, new Node(new DummyMovie(66, "6A", new string[2] { "D", "B" })));
        nodes.Add(77, new Node(new DummyMovie(77, "7A", new string[3] { "A", "B", "D" })));
        nodes.Add(81, new Node(new DummyMovie(81, "8A", new string[1] { "D" })));
        nodes.Add(91, new Node(new DummyMovie(91, "9A", new string[1] { "C" })));
        nodes.Add(0144, new Node(new DummyMovie(0144, "0A", new string[1] { "A" })));
        nodes.Add(116, new Node(new DummyMovie(116, "1A", new string[1] { "B" })));
        nodes.Add(216, new Node(new DummyMovie(216, "2A", new string[2] { "A", "B" })));
        nodes.Add(316, new Node(new DummyMovie(316, "3A", new string[2] { "A", "B" })));
        nodes.Add(416, new Node(new DummyMovie(416, "4A", new string[2] { "C", "B" })));
        nodes.Add(516, new Node(new DummyMovie(516, "5A", new string[4] { "A", "B", "C", "D" })));
        nodes.Add(616, new Node(new DummyMovie(616, "6A", new string[2] { "D", "B" })));
        nodes.Add(716, new Node(new DummyMovie(716, "7A", new string[3] { "A", "B", "D" })));
        nodes.Add(816, new Node(new DummyMovie(816, "8A", new string[1] { "D" })));
        nodes.Add(916, new Node(new DummyMovie(916, "9A", new string[1] { "C" })));
        nodes.Add(0118, new Node(new DummyMovie(0118, "0A", new string[1] { "A" })));
        nodes.Add(111, new Node(new DummyMovie(111, "1A", new string[1] { "B" })));
        nodes.Add(211, new Node(new DummyMovie(211, "2A", new string[2] { "A", "B" })));
        nodes.Add(311, new Node(new DummyMovie(311, "3A", new string[2] { "A", "B" })));
        nodes.Add(411, new Node(new DummyMovie(411, "4A", new string[2] { "C", "B" })));
        nodes.Add(511, new Node(new DummyMovie(511, "5A", new string[4] { "A", "B", "C", "D" })));
        nodes.Add(611, new Node(new DummyMovie(611, "6A", new string[2] { "D", "B" })));
        nodes.Add(711, new Node(new DummyMovie(711, "7A", new string[3] { "A", "B", "D" })));
        nodes.Add(811, new Node(new DummyMovie(811, "8A", new string[1] { "D" })));
        nodes.Add(911, new Node(new DummyMovie(911, "9A", new string[1] { "C" })));
        nodes.Add(109, new Node(new DummyMovie(109, "0A", new string[1] { "A" })));
        nodes.Add(19, new Node(new DummyMovie(11, "1A", new string[1] { "B" })));
        nodes.Add(29, new Node(new DummyMovie(21, "2A", new string[2] { "A", "B" })));
        nodes.Add(39, new Node(new DummyMovie(31, "3A", new string[2] { "A", "B" })));
        nodes.Add(49, new Node(new DummyMovie(41, "4A", new string[2] { "C", "B" })));
        nodes.Add(59, new Node(new DummyMovie(51, "5A", new string[4] { "A", "B", "C", "D" })));
        nodes.Add(69, new Node(new DummyMovie(61, "6A", new string[2] { "D", "B" })));
        nodes.Add(79, new Node(new DummyMovie(71, "7A", new string[3] { "A", "B", "D" })));
        nodes.Add(89, new Node(new DummyMovie(81, "8A", new string[1] { "D" })));
        nodes.Add(99, new Node(new DummyMovie(91, "9A", new string[1] { "C" }))); 
    }

    public void GenerateGraph()
    {
        foreach (var node in nodes.Values)
        {
            node.position = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(-10, 10));
            //foreach (var otherNode in node.nodesForce)
            //{
            //    var positionDiference = node.position - otherNode.Item1.position;
            //    var distance = positionDiference.magnitude;

            //    float force;
            //    if (otherNode.Item2 > 0)
            //    {
            //        force = connectedNodeForce * Mathf.Log10(distance / minConnectedDistance);
            //    }
            //    else
            //    {
            //        force = disconnectedNodeForce / Mathf.Pow(distance, 2.1f);
            //    }

            //    otherNode.Item1.velocity += force * Time.deltaTime * positionDiference.normalized;
            //}
        }
    }

    public void CalculateNeighbours()
    {
        foreach (var node in nodes.Values)
        {
            foreach (var otherNode in nodes.Values)
            {
                if (node.id != otherNode.id)
                {
                    var similarity = Similarity(node, otherNode); // similarity should be mapped from 0-1 to be used in force function
                    node.AddNeighbour(new Tuple<Node, int>(otherNode, similarity));
                    //otherNode.AddNeighbour(new Tuple<Node, int>(node, similarity));
                }
            }
        }
    }

    private int Similarity(Node node1, Node node2)
    {
        var numberOfEqualGenres = 0;
        foreach (string genre in node1.movie.genres)
        {
            foreach (string otherGenre in node2.movie.genres)
            {
                if (genre == otherGenre)
                {
                    numberOfEqualGenres++;
                }
            }
        }

        return numberOfEqualGenres;
    }
}

public class Node
{
    public int id;
    public List<Tuple<Node, int>> nodesForce;
    public DummyMovie movie;
    public Vector3 position;
    public Vector3 velocity;
    public Node(DummyMovie m)
    {
        id = m.id;
        nodesForce = new List<Tuple<Node, int>>();
        movie = m;
        position = UnityEngine.Random.insideUnitSphere * 5;
        velocity = Vector3.zero;
    }

    public void AddNeighbour(Tuple<Node, int> neighbour)
    {
        nodesForce.Add(neighbour);
    }
}

public class DummyMovie
{
    public int id;
    public string name;
    public string[] genres;

    public DummyMovie(int i, string n, string[] g)
    {
        id = i;
        name = n;
        genres = g;
    }
}
