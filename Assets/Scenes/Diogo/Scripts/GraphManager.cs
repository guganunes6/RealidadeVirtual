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
    public List<Edge> edges;
    private Dictionary<string, DecodedNode> movies;

    public GameObject cylinderPrefab;
    private List<GameObject> cylinders;

    public GameObject spherePrefab;
    private List<GameObject> spheres;

    public GameObject spheresParent;
    public GameObject cylindersParent;
    public GameObject manager;

    public GameObject MovieUI;
    private void Start()
    {
        spheresParent = GameObject.Find("Spheres");
    }

    public void InitializeGraphManager(Dictionary<string, DecodedNode> decodedMovies)
    {
        movies = decodedMovies;
        nodes = new Dictionary<int, Node>();
        edges = new List<Edge>();
        cylinders = new List<GameObject>();
        spheres = new List<GameObject>();
        InitializeNodes();
        CalculateNeighbours();
        PreGenerateGraph();
        GenerateGraph();
        AddGraphY();
        InstanciateCylinders();
        manager.SetActive(true);
    }

    private void CreateCylinder(Node node1, Node node2)
    {
        var startPos = node1.position;
        var endPos = node2.position;
        var diff = endPos - startPos;
        var position = startPos + (diff / 2);
        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, diff);

        var cyl = Instantiate(cylinderPrefab, position, Quaternion.identity);
        cyl.transform.localScale = new Vector3(cylinderPrefab.transform.localScale.x, diff.magnitude / 2, cylinderPrefab.transform.localScale.x);
        cyl.transform.rotation = tilt;
        edges.Add(new Edge(node1, node2, cyl.transform.position));
        cylinders.Add(cyl);
        cyl.transform.parent = cylindersParent.transform;
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
                    CreateCylinder(node, neighbour.Item1);
                }
            }
        }
    }

    private GameObject InstanciateSphere(int id, DecodedNode movie)
    {
        var sphere = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);
        sphere.GetComponent<Node>().NodeConstructor(id, movie);
        spheres.Add(sphere);
        sphere.transform.parent = spheresParent.transform;
        return sphere;
    }

    private void InitializeNodes()
    {
        var id = 0;
        foreach (var movie in movies.Values)
        {
            var newNode = InstanciateSphere(id, movie);
            nodes.Add(id, newNode.GetComponent<Node>());
            id++;
        }
    }

    private void PreGenerateGraph()
    {
        UnityEngine.Random.InitState(1);
        foreach (var node in nodes.Values)
        {
            node.position = new Vector3(UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize), 0, UnityEngine.Random.Range(-graphCanvasSize, graphCanvasSize));
        }
    }
    
    public void GenerateGraph()
    {
        for (int i = 0; i < 100; i++)
        {
            foreach (var node in nodes.Values)
            {
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
                    }

                    var velocity = force * positionDiference.normalized * 0.4f;
                    otherNode.Item1.velocity = velocity;
                    otherNode.Item1.setPosition(new Vector3(otherNode.Item1.position.x + velocity.x, 0, otherNode.Item1.position.z + velocity.z));
                }
            }
        }
    }

    public void AddGraphY()
    {
        foreach (var node in nodes.Values)
        {
            node.setPosition(new Vector3(node.position.x, Mathf.Pow(float.Parse(node.movie.getVoteAverage(), new System.Globalization.CultureInfo("en-US").NumberFormat), 1.5f), node.position.z));
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

    public void DebugLogGenres(Vector3 nodePosition)
    {
        foreach (var node in nodes.Values)
        {
            if (node.position == nodePosition)
            {
                node.LogGenres();
            }
        }
    }

    public void ToggleMovieUI(bool show, GameObject nodeSphere)
    {
        if(show)
        {
            MovieUI.GetComponent<MovieUI>().ShowUI(nodeSphere.GetComponent<Node>().movie);
            MovieUI.GetComponent<AnimationFade>().startAnimation(MovieUI);
        }
        else
        {
            MovieUI.GetComponent<MovieUI>().HideUI();
        }
    }
    public void OutlineNodeEdges(GameObject nodeSphere, Color outlineColor, float outlineWidth, bool showOutline)
    {
        Node nodeToOutline = nodeSphere.GetComponent<Node>();
        nodeToOutline.isMarked = showOutline;
        nodeToOutline.markedColor = outlineColor;

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
                        else if (!showOutline && neighbour.Item1.isMarked)
                        {
                            outline.OutlineColor = neighbour.Item1.markedColor;
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


public class Edge
{
    public Node node1;
    public Node node2;
    public Vector3 position;

    public Edge(Node n1, Node n2, Vector3 pos)
    {
        node1 = n1;
        node2 = n2;
        position = pos;
    }
}
