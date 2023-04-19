double[,] graph = {
{ 0, 1, double.PositiveInfinity, double.PositiveInfinity, 6, 3},
{ 1, 0, 6, double.PositiveInfinity, double.PositiveInfinity, 5},
{ double.PositiveInfinity, double.PositiveInfinity, 1, 0, 2, double.PositiveInfinity },
{ double.PositiveInfinity, 6, 0, 1, 3, 3 },
{6, double.PositiveInfinity, 3, 2, 0, 4 },
{3, 5, 3, double.PositiveInfinity, 4, 0} 
};
double[,] FloydsAlgorithm(double[,] graph)
{
    double n = Math.Sqrt(graph.Length);
    for (int k = 0; k < n; k++)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i != j) graph[i, j] = Math.Min(graph[i, j], graph[i, k] + graph[k, j]);
            }
        }
    }
    return graph;
}

void PrintArray(double[,] arr)
{
    double n = Math.Sqrt(graph.Length);
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            Console.Write($"{arr[i, j]}\t");
        }
        Console.WriteLine();
    }
}

void AddArray()
{
    int n = Convert.ToInt32(Console.ReadLine());
    double[,] graph = new double[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            graph[i, j] = Convert.ToInt32(Console.ReadLine());
        }
    }
}

PrintArray(FloydsAlgorithm(graph));