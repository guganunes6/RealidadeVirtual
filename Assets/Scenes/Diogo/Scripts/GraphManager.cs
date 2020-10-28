using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public int graphCanvasSize;
    public Dictionary<int, Node> nodes;
    private Dictionary<string, DecodedNode> movies;

    private void Start()
    {
        movies = gameObject.GetComponent<Main>().GetMovies();
        nodes = new Dictionary<int, Node>();
        InitializeNodes();
        CalculateNeighbours();
        GenerateGraph();
        InstanciateCylinders();
    }

    public GameObject cylinderPrefab;
    public float cylinderRadius;
    public float sphereRadius = 0.2f;
    private void CreateCylinder(Vector3 startPos, Vector3 endPos)
    {

        var diff = endPos - startPos;
        var position = startPos + (diff / 2);
        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, diff);

        var cyl = Instantiate(cylinderPrefab, position, Quaternion.identity);
        cyl.transform.localScale = new Vector3(cylinderRadius, diff.magnitude / 2, cylinderRadius);
        cyl.transform.rotation = tilt;
    }

    private void InstanciateCylinders()
    {
        List<int> nodesAdded = new List<int>();
        foreach (var node in nodes.Values)
        {
            nodesAdded.Add(node.id);
            foreach (var neighbour in node.neighbours)
            {
                if (!nodesAdded.Contains(neighbour.Item1.id))
                {
                    CreateCylinder(node.position, neighbour.Item1.position);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var node in nodes.Values)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(node.position, 0.2f);
            Gizmos.color = Color.green;
            foreach (var otherNode in node.neighbours)
            {
                if (otherNode.Item2 > 0)
                {
                    Gizmos.DrawLine(node.position, otherNode.Item1.position);
                }
            }
        }
    }

    private void InitializeNodes()
    {
        foreach (var movie in movies.Values)
        {
            nodes.Add(Int32.Parse(movie.getId()), new Node(movie));
        }
    }
    public void GenerateGraph()
    {
        foreach (var node in nodes.Values)
        {
            node.position = new Vector3(UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize), UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize), UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize));
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
                    // similarity should be mapped from 0-1 to be used in force function
                    //Debug.Log(otherNode.movie.getId()); 
                    //Debug.Log(otherNode.movie.getId() + " " + otherNode.movie.getGenres());

                    node.AddNeighbour(new Tuple<Node, int>(otherNode, Similarity(node, otherNode)));
                }
            }
        }
    }

    private int Similarity(Node node1, Node node2)
    {
        var numberOfEqualGenres = 0;
        foreach (string genre in node1.movie.getGenres())
        {
            foreach (string otherGenre in node2.movie.getGenres())
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
    public List<Tuple<Node, int>> neighbours;
    public DecodedNode movie;
    public Vector3 position;
    public Node(DecodedNode m)
    {
        id = Int32.Parse(m.getId());
        neighbours = new List<Tuple<Node, int>>();
        movie = m;
        //position = UnityEngine.Random.insideUnitSphere * 5; // ? Vector3.zero
        position = Vector3.zero;
    }

    public void AddNeighbour(Tuple<Node, int> neighbour)
    {
        neighbours.Add(neighbour);
    }
}
