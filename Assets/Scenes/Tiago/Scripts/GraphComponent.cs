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
        var node1 = new Node<Vector3>() { Value = Vector3.zero, NodeColor = new Color(0.0f, 100.0f, 0.0f) };
        var node2 = new Node<Vector3>() { Value = Vector3.one, NodeColor = Color.black };
        var node3 = new Node<Vector3>() { Value = new Vector3(2,2,-2), NodeColor = Color.black };

        var edge1 = new Edge<float, Vector3>() { Value = 1.0f, From = node1, To = node2, EdgeColor = Color.cyan};
        var edge2 = new Edge<float, Vector3>() { Value = 1.0f, From = node1, To = node3, EdgeColor = Color.cyan };


        graph.Nodes.Add(node1);
        graph.Nodes.Add(node2);
        graph.Nodes.Add(node3);

        graph.Edges.Add(edge1);
        graph.Edges.Add(edge2);
    }

    // Update is called once per frame
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

    }
}
