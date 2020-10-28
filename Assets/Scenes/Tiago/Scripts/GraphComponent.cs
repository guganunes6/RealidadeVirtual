using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class GraphComponent : MonoBehaviour
{
    private Graph<Vector3, float> graph;

    // Start is called before the first frame update
    void Start()
    {
        graph = new Graph<Vector3, float>();
        var node1 = new Node(new DummyMovie(0, "0A", new string[1] { "A" })) { id = 0, position = Vector3.zero /*, movie = null */  };
        var node2 = new Node(new DummyMovie(1, "0B", new string[1] { "B" })) { id = 1, position = Vector3.one/*, movie = null */ };
        var node3 = new Node(new DummyMovie(2, "0C", new string[1] { "C" })) { id = 2, position = new Vector3(2,2,-2)/*, movie = null */};

        var edge1 = new Edge() { id = 0, From = node1, To = node2 };
        var edge2 = new Edge() { id = 1, From = node1, To = node3 };


        graph.Nodes.Add(node1.id, node1);
        graph.Nodes.Add(node2.id, node2);
        graph.Nodes.Add(node3.id, node3);

        graph.Edges.Add(edge1.id, edge1);
        graph.Edges.Add(edge2.id, edge2);
        

    }

    // Update is called once per frame

    /*
    void OnDrawGizmos()
    {

        if (graph == null){
            Start();
        }

        foreach( var node in graph.Nodes){
            Gizmos.color = node.NodeColor;
            Gizmos.DrawSphere(node.Value, 0.125f);
        }

        foreach (var edge in graph.Edges){
            Gizmos.color = edge.EdgeColor;
            Gizmos.DrawLine(edge.From.Value, edge.To.Value);
        }

    }*/

    void Update()
    {
        
    }
}
