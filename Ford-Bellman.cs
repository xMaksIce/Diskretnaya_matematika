double[,] AddGraph()
{
    int n = Convert.ToInt32(Console.ReadLine());
    double[,] graph = new double[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            Console.Clear();
            Console.WriteLine($"({i + 1};{j + 1})");
            try
            {
                graph[i, j] = Convert.ToDouble(Console.ReadLine());
            }
            catch (System.FormatException) // чтобы указать, что дуги из вершины в вершину нет (вес равен бесконечности), можно ввести любую нечисловую строку
            {
                graph[i, j] = double.PositiveInfinity;
            }
        }
    }
    Console.Clear();
    return graph;
}
double[] FordBellman(double[,] graph, int start)
{
    double[] distance = new double[graph.GetLength(0)];
    Array.Fill(distance, double.PositiveInfinity);
    distance[start - 1] = 0;
    double[] prevDistance = new double[distance.Length];

    for (int k = 0; k <= distance.Length; k++)
    {
        Array.Copy(distance, prevDistance, distance.Length);
        for (int i = 0; i < distance.Length; i++)
        {
            for (int j = 0; j < distance.Length; j++)
            {
                if (prevDistance[j] + graph[j, i] < distance[i])
                {
                    distance[i] = prevDistance[j] + graph[j, i];
                }
            }
        }
        if (k == distance.Length && !Enumerable.SequenceEqual(prevDistance, distance))
        {
            Console.WriteLine("В графе имеется цикл отрицательной длины");
            return distance;
        }
    }
    return distance;
}

double[,] graph = {
    {double.PositiveInfinity, 1, double.PositiveInfinity, double.PositiveInfinity, 3 },
    {double.PositiveInfinity, double.PositiveInfinity, 8, 7, 1 },
    {double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1, -5 },
    {double.PositiveInfinity, double.PositiveInfinity, 2, double.PositiveInfinity, double.PositiveInfinity },
    {double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 4, double.PositiveInfinity } };

int start = Convert.ToInt32(Console.ReadLine());

double[] result = FordBellman(graph, start);
for (int i = 0; i < result.Length; i++)
{
    Console.WriteLine(result[i]);
}