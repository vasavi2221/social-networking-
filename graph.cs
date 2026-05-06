using System;
using System.Collections.Generic;

public class Graph
{
    // Adjacency list: each node maps to a list of (neighbour, weight) pairs
    private Dictionary<string, List<(string neighbour, double weight)>> _adjacencyList;
    private bool _isWeighted;

    public Graph(bool isWeighted)
    {
        _adjacencyList = new Dictionary<string, List<(string, double)>>();
        _isWeighted = isWeighted;
    }

    // Add a node if it doesn't exist
    public void AddNode(string node)
    {
        if (!_adjacencyList.ContainsKey(node))
            _adjacencyList[node] = new List<(string, double)>();
    }

    // Add an undirected edge
    public void AddEdge(string node1, string node2, double weight = 1.0)
    {
        AddNode(node1);
        AddNode(node2);
        _adjacencyList[node1].Add((node2, weight));
        _adjacencyList[node2].Add((node1, weight));
    }

    // Get all nodes
    public IEnumerable<string> GetNodes() => _adjacencyList.Keys;

    // Get neighbours of a node
    public List<(string neighbour, double weight)> GetNeighbours(string node)
    {
        return _adjacencyList.ContainsKey(node) 
            ? _adjacencyList[node] 
            : new List<(string, double)>();
    }

    // Total number of nodes
    public int NodeCount => _adjacencyList.Count;

    public bool IsWeighted => _isWeighted;
}