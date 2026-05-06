using System;
using System.Collections.Generic;

public class Dijkstra
{
    // Calculates shortest weighted distances from source to all other nodes
    private static Dictionary<string, double> GetShortestDistances(Graph graph, string source)
    {
        var distances = new Dictionary<string, double>();
        var priorityQueue = new SortedSet<(double distance, string node)>(
            Comparer<(double, string)>.Create((a, b) =>
                a.Item1 != b.Item1 ? a.Item1.CompareTo(b.Item1) : a.Item2.CompareTo(b.Item2))
        );

        // Initialise all distances as infinity
        foreach (var node in graph.GetNodes())
            distances[node] = double.MaxValue;

        // Start from source
        distances[source] = 0;
        priorityQueue.Add((0, source));

        while (priorityQueue.Count > 0)
        {
            // Get node with smallest distance
            var (currentDist, current) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            foreach (var (neighbour, weight) in graph.GetNeighbours(current))
            {
                double newDist = currentDist + weight;

                if (newDist < distances[neighbour])
                {
                    // Remove old distance, add updated distance
                    priorityQueue.Remove((distances[neighbour], neighbour));
                    distances[neighbour] = newDist;
                    priorityQueue.Add((newDist, neighbour));
                }
            }
        }

        return distances;
    }

    // Calculates influence score for a given node
    public static double CalculateInfluenceScore(Graph graph, string node)
    {
        var distances = GetShortestDistances(graph, node);

        double totalDistance = 0;
        int reachableNodes = 0;

        foreach (var kvp in distances)
        {
            if (kvp.Key != node && kvp.Value != double.MaxValue)
            {
                totalDistance += kvp.Value;
                reachableNodes++;
            }
        }

        if (totalDistance == 0) return 0;

        // Formula: (n-1) / sum of distances
        return reachableNodes / totalDistance;
    }
}