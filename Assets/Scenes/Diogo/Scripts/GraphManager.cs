using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public float connectedNodeForce;
    public float minConnectedDistance;
    public float disconnectedNodeForce;
    public float graphCanvasSize;
    public Dictionary<int, Node> nodes;
    private Dictionary<string, DecodedNode> movies;

    public GameObject cylinderPrefab;
    private List<GameObject> cylinders;

    public GameObject spherePrefab;
    private List<GameObject> spheres;

    private GameObject spheresParent;
    public GameObject manager;

    private void Start()
    {
        spheresParent = GameObject.Find("Spheres");
    }

    public void InitializeGraphManager(Dictionary<string, DecodedNode> decodedMovies)
    {
        movies = decodedMovies;
        nodes = new Dictionary<int, Node>();
        cylinders = new List<GameObject>();
        spheres = new List<GameObject>();
        InitializeNodes();
        CalculateNeighbours();
        PreGenerateGraph();
        GenerateGraph();
        AddGraphY();
        InstanciateCylinders();
        InstanciateSpheres();
        manager.SetActive(true);
    }

    private void CreateCylinder(Vector3 startPos, Vector3 endPos)
    {

        var diff = endPos - startPos;
        var position = startPos + (diff / 2);
        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, diff);

        var cyl = Instantiate(cylinderPrefab, position, Quaternion.identity);
        cyl.transform.localScale = new Vector3(cylinderPrefab.transform.localScale.x, diff.magnitude / 2, cylinderPrefab.transform.localScale.x);
        cyl.transform.rotation = tilt;
        cylinders.Add(cyl);
    }

    private void InstanciateCylinders()
    {
        List<int> nodesAdded = new List<int>();
        foreach (var node in nodes.Values)
        {
            nodesAdded.Add(node.id);
            foreach (var neighbour in node.neighbours)
            {
                if (!nodesAdded.Contains(neighbour.Item1.id) && neighbour.Item2 > 0)
                {
                    CreateCylinder(node.position, neighbour.Item1.position);
                }
            }
        }
    }

    private void InstanciateSpheres()
    {
        foreach (var node in nodes.Values)
        {
            var sphere = Instantiate(spherePrefab, node.position, Quaternion.identity);
            spheres.Add(sphere);
            sphere.transform.parent = spheresParent.transform;
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
    //private int M = 100;
    //private void FixedUpdate()
    //{
    //    //while (M != 0)
    //    //{
    //    //    M--;
    //    GenerateGraph();
    //    foreach (var node in nodes.Values)
    //    {
    //        //node.position += node.velocity * Time.deltaTime;
    //        var vel = node.velocity * Time.deltaTime;
    //        node.position = new Vector3(node.position.x + vel.x, 0, node.position.z + vel.z);

    //        //Debug.Log(node.id);
    //    }
    //}
    private void PreGenerateGraph()
    {
        foreach (var node in nodes.Values)
        {
            //node.position = new Vector3(UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize), UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize), UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize));
            node.position = new Vector3(UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize), 0, UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize));
            //node.position = new Vector3(1, 1, 1);
        }
    }
    
    public void GenerateGraph()
    {
        for (int i = 0; i < 100; i++)
        {
            foreach (var node in nodes.Values)
            {
                //Debug.Log("nodeId: " + node.id + "Len neighbours: ");
                foreach (var otherNode in node.neighbours)
                {
                    var positionDiference = node.position - otherNode.Item1.position;
                    var distance = positionDiference.magnitude;

                    float force;
                    if (otherNode.Item2 > 0)
                    {
                        force = connectedNodeForce * Mathf.Log(distance / minConnectedDistance);
                    }
                    else
                    {
                        force = disconnectedNodeForce / Mathf.Pow(distance, 2);
                        //Debug.Log(force);
                    }

                    //Debug.DrawRay(otherNode.Item1.position, positionDiference.normalized, Color.blue);
                    //otherNode.Item1.velocity = force * Time.deltaTime * positionDiference.normalized * 0.4f;
                    otherNode.Item1.velocity = force * positionDiference.normalized * 0.4f;
                    otherNode.Item1.position = new Vector3(otherNode.Item1.position.x + otherNode.Item1.velocity.x, 0, otherNode.Item1.position.z + otherNode.Item1.velocity.z);
                }
            }
        }
    }

    public void AddGraphY()
    {
        foreach (var node in nodes.Values)
        {
            node.position = new Vector3(node.position.x, Mathf.Pow(float.Parse(node.movie.getVoteAverage()), 1.2f), node.position.z);
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

    public void OutlineNodeEdges(Vector3 nodePosition, Color outlineColor, float outlineWidth, bool showOutline)
    {
        Debug.Log("found function");
        Node nodeToOutline = null;
        // find node 
        foreach (var node in nodes.Values)
        {
            if (node.position == nodePosition)
            {
                nodeToOutline = node;
                Debug.Log("found node");
            }
        }

        foreach (var neighbour in nodeToOutline.neighbours)
        {
            if (neighbour.Item2 > 0)
            {
                // find cylinder by position
                var diff = neighbour.Item1.position - nodeToOutline.position;
                var position = nodeToOutline.position + (diff / 2);

                foreach (var cyl in cylinders)
                {
                    if (cyl.transform.position == position)
                    {
                        var outline = cyl.GetComponent<Outline>();
                        if (showOutline && outlineColor != Color.white)
                        {
                            outline.enabled = true;
                            outline.OutlineColor = outlineColor;
                            outline.OutlineWidth = outlineWidth;
                        }
                        else
                        {
                            outline.enabled = false;
                        }
                    }
                }
            }
        }
    }
}

public class Node
{
    public int id;
    public List<Tuple<Node, int>> neighbours;
    public DecodedNode movie;
    public Vector3 position;
    public Vector3 velocity;
    public Node(DecodedNode m)
    {
        id = Int32.Parse(m.getId());
        neighbours = new List<Tuple<Node, int>>();
        movie = m;
        position = Vector3.zero;
        velocity = Vector3.zero;
    }

    public void AddNeighbour(Tuple<Node, int> neighbour)
    {
        neighbours.Add(neighbour);
    }
}
