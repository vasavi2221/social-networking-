using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Social Network Influence Score Calculator ===\n");

        // Ask user for graph type
        Console.WriteLine("Select graph type:");
        Console.WriteLine("1. Unweighted");
        Console.WriteLine("2. Weighted");
        Console.Write("Enter choice (1 or 2): ");
        string typeChoice = Console.ReadLine();
        bool isWeighted = typeChoice == "2";

        // Ask user for file path
        Console.Write("\nEnter the path to your CSV file: ");
        string filePath = Console.ReadLine();

        // Load the graph
        Graph graph = FileLoader.LoadGraph(filePath, isWeighted);

        if (graph.NodeCount == 0)
        {
            Console.WriteLine("Graph is empty. Please check your file.");
            return;
        }

        // Calculate influence scores for all nodes
        Console.WriteLine("\n=== Influence Scores ===\n");

        string topNode = "";
        double topScore = -1;

        foreach (var node in graph.GetNodes())
        {
            double score = isWeighted
                ? Dijkstra.CalculateInfluenceScore(graph, node)
                : BFS.CalculateInfluenceScore(graph, node);

            Console.WriteLine($"Node: {node,-15} Influence Score: {score:F4}");

            if (score > topScore)
            {
                topScore = score;
                topNode = node;
            }
        }

        Console.WriteLine($"\n=== Top Influencer: {topNode} with score {topScore:F4} ===");
    }
}