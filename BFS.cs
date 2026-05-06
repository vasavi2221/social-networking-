using System;
using System.Collections.Generic;

public class BFS
{
    // Calculates shortest distances from source to all other nodes
    private static Dictionary<string, int> GetShortestDistances(Graph graph, string source)
    {
        var distances = new Dictionary<string, int>();
        var queue = new Queue<string>();

        // Initialise all distances as -1 (unvisited)
        foreach (var node in graph.GetNodes())
            distances[node] = -1;

        // Start from source
        distances[source] = 0;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var (neighbour, _) in graph.GetNeighbours(current))
            {
                if (distances[neighbour] == -1)
                {
                    distances[neighbour] = distances[current] + 1;
                    queue.Enqueue(neighbour);
                }
            }
        }

        return distances;
    }

    // Calculates influence score for a given node
    public static double CalculateInfluenceScore(Graph graph, string node)
    {
        var distances = GetShortestDistances(graph, node);

        int totalDistance = 0;
        int reachableNodes = 0;

        foreach (var kvp in distances)
        {
            if (kvp.Key != node && kvp.Value != -1)
            {
                totalDistance += kvp.Value;
                reachableNodes++;
            }
        }

        // Cannot calculate if no other nodes are reachable
        if (totalDistance == 0) return 0;

        // Formula: (n-1) / sum of distances
        return (double)reachableNodes / totalDistance;
    }
}