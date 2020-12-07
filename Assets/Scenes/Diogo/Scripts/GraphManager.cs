using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    public int algorithmIterations;
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
        //GenerateGraph2();
        //BarycentricMethodSetup();
        //GenerateGraphBarycentricMethod();
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
        cyl.SetActive(false);
    }

    private void InstanciateCylinders()
    {
        List<int> nodesAdded = new List<int>();
        foreach (var node in nodes.Values)
        {
            nodesAdded.Add(node.id);
            foreach (var neighbour in node.neighbours)
            {
                if (!nodesAdded.Contains(neighbour.Item1.id) && AreConnected(node, neighbour))
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
            node.position = new Vector3(UnityEngine.Random.Range(0, graphCanvasSize), 0, UnityEngine.Random.Range(0, graphCanvasSize));
        }
    }
    
    public void GenerateGraph()
    {
        for (int i = 0; i < algorithmIterations; i++)
        {
            foreach (var node in nodes.Values)
            {
                foreach (var otherNode in node.neighbours)
                {
                    var positionDiference = node.position - otherNode.Item1.position;
                    var distance = positionDiference.magnitude;

                    float force;
                    if (AreConnected(node, otherNode))
                    {
                        force = connectedNodeForce * Mathf.Log(distance / minConnectedDistance);
                    }
                    else
                    {
                        force = disconnectedNodeForce / Mathf.Pow(distance, 2);
                    }

                    var velocity = force * positionDiference.normalized * 0.01f;
                    otherNode.Item1.velocity = velocity;
                    var x = otherNode.Item1.position.x + velocity.x;
                    if (x > graphCanvasSize)
                    {
                        x = graphCanvasSize;
                    }
                    if (x < 0)
                    {
                        x = 0;
                    }
                    var z = otherNode.Item1.position.z + velocity.z;
                    if (z > graphCanvasSize)
                    {
                        z = graphCanvasSize;
                    }
                    if (z < 0)
                    {
                        z = 0;
                    }

                    otherNode.Item1.setPosition(new Vector3(x, 0, z));
                }
            }
        }
    }

    bool AreConnected(Node node, Tuple<Node, int> neighbour)
    {
        return neighbour.Item2 == node.GetAmountOfGenres() && neighbour.Item1.GetAmountOfGenres() == node.GetAmountOfGenres();
    }

    //public void GenerateGraph2()
    //{
    //    //for (int i = 0; i < algorithmIterations; i++)
    //    //{
    //        // force towards center??
    //        foreach (var node in nodes.Values)
    //        {
    //            var gravity = node.position * -1.1f; // gravity force constant
    //            node.forceVector = gravity;
    //        //Debug.Log("gravity: " + node.forceVector);
    //        //Debug.Log("node: " + node.position);
    //    }


    //    foreach (var node in nodes.Values)
    //        {
    //            foreach (var otherNode in node.neighbours)
    //            {
    //                // repulsive force between all nodes
    //                var direction = node.position - otherNode.Item1.position;
    //                var repulse = direction / (direction.magnitude * direction.magnitude);
    //                repulse = repulse * 1000; // repulsive force constant
    //                //Debug.Log("repulse: " + repulse);

    //                node.forceVector += repulse * -1;
    //                otherNode.Item1.forceVector += repulse;

    //                // connection force
    //                if (otherNode.Item2 == node.GetAmountOfGenres())
    //                {
    //                    var diff = direction.magnitude - 5f; // max distance???????
    //                                                   //Debug.Log("connection: " + dir);

    //                    node.forceVector -= direction;
    //                    otherNode.Item1.forceVector += direction;
    //                }
    //            }
    //        }

    //        // apply forces
    //        foreach (var node in nodes.Values)
    //        {
    //                                        // mass
    //        var velocity = (node.forceVector / 10000) * Time.deltaTime;
    //        Debug.Log("vel: " + velocity);
    //        node.setPosition(new Vector3(node.position.x + velocity.x, 0, node.position.z + velocity.z));
    //        }
    //    //}
    //}

    //float Area;
    //float K;
    //void BarycentricMethodSetup()
    //{
    //    Area = Mathf.Pow(graphCanvasSize, 2);
    //    PreGenerateGraph();
    //    K = Mathf.Sqrt(Area / nodes.Count);
    //}
    //float AttractionForce(float distance)
    //{
    //    return Mathf.Pow(distance, 2) / K;
    //}
    //float RepulsiveForce(float distance)
    //{
    //    return Mathf.Pow(K, 2) / distance;
    //}
    //public void GenerateGraphBarycentricMethod()
    //{
    //    //BarycentricMethodSetup();
    //    //for (int i = 0; i < algorithmIterations; i++)
    //    //{
    //        foreach (var node in nodes.Values)
    //        {
    //            node.dispVector = Vector3.zero;
    //            foreach (var otherNode in node.neighbours)
    //            {
    //                // repulsive force between all nodes
    //                var direction = node.position - otherNode.Item1.position;
    //                node.dispVector += (direction / direction.magnitude) * RepulsiveForce(direction.magnitude); 

    //                // connection force
    //                if (otherNode.Item2 == node.GetAmountOfGenres())
    //                {
    //                    node.dispVector -= (direction / direction.magnitude) * AttractionForce(direction.magnitude);
    //                    otherNode.Item1.dispVector += (direction / direction.magnitude) * AttractionForce(direction.magnitude);
    //                }
    //            }
    //        }

    //        // apply forces, {limit max displacement to temperature t and prevent from displacement outside canvas}
    //        foreach (var node in nodes.Values)
    //        {
    //            //v.pos := v.pos + (v.disp /| v.disp |) ∗ min(v.disp, t);
    //            //v.pos.x := min(W / 2, max(−W / 2, v.pos.x));
    //            //v.pos.y := min(L / 2, max(−L / 2, v.pos.y))

    //            node.position += (node.dispVector / node.dispVector.magnitude);
    //            var x = node.position.x > graphCanvasSize ? graphCanvasSize : node.position.x;
    //            var z = node.position.z > graphCanvasSize ? graphCanvasSize : node.position.z;

    //            node.setPosition(new Vector3(x, 0, z));
    //        }
    //    //}
    //}

    //private void Update()
    //{
    //    //GenerateGraphBarycentricMethod();
    //    //GenerateGraph2();
    //}

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
            //MovieUI.GetComponent<AnimationFade>().startAnimation(MovieUI);
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
            if (neighbour.Item2 == nodeToOutline.GetAmountOfGenres())
            {
                    // find cylinder by position
                    var diff = neighbour.Item1.position - nodeToOutline.position;
                var position = nodeToOutline.position + (diff / 2);

                foreach (var cyl in cylinders)
                {
                    if (cyl.transform.position == position)
                    {
                        cyl.SetActive(true);
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
                            cyl.SetActive(false);
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
