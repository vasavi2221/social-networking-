using System;
using System.IO;

public class FileLoader
{
    public static Graph LoadGraph(string filePath, bool isWeighted)
    {
        var graph = new Graph(isWeighted);

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return graph;
        }

        var lines = File.ReadAllLines(filePath);

        // Skip the header line
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            var parts = line.Split(',');

            if (isWeighted && parts.Length >= 3)
            {
                string node1 = parts[0].Trim();
                string node2 = parts[1].Trim();
                double weight = double.Parse(parts[2].Trim());
                graph.AddEdge(node1, node2, weight);
            }
            else if (!isWeighted && parts.Length >= 2)
            {
                string node1 = parts[0].Trim();
                string node2 = parts[1].Trim();
                graph.AddEdge(node1, node2);
            }
        }

        Console.WriteLine($"Graph loaded successfully with {graph.NodeCount} nodes.");
        return graph;
    }
}