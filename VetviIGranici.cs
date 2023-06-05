double VetviIGranici(double[,] graph)
{
    int graphSize = graph.GetLength(0);
    // создадим копию матрицы весов графа,
    // добавив последним элементом каждой строки и столбца номер этой строки или столбца (нумеруем с нуля)
    double[,] markedGraph = new double[graphSize + 1, graphSize + 1];
    // копируем массив, пока не заполняя номера
    for (int i = 0; i < graphSize; i++)
    {
        for (int j = 0; j < graphSize; j++)
        {
            markedGraph[i, j] = graph[i, j];
        }
    }
    // помечаем строки и столбцы
    for (int i = 0; i < graphSize; i++)
    {
        markedGraph[i, graphSize] = i;
        markedGraph[graphSize, i] = i;
    }
    markedGraph[graphSize, graphSize] = -1 ;
    double h = 0;
    // далее рекурсивно пересчитываем минимум по строкам и столбцам и уменьшаем матрицу
    return GraphDestroyer(markedGraph, h);
}
double GraphDestroyer(double[,] graph, double h)
{
    int graphSize = graph.GetLength(0) - 1;
    // считаем минимум по строкам, добавляем его к h (нижней границе) и уменьшаем каждый элемент в строке на этот минимум
    for (int i = 0; i < graphSize; i++)
    {
        double minInLine = double.PositiveInfinity;
        for (int j = 0; j < graphSize; j++)
        {
            minInLine = Math.Min(minInLine, graph[i, j]);
        }
        h += minInLine;
        for (int j = 0; j < graphSize; j++)
        {
            graph[i, j] -= minInLine;
        }
    }
    if (graph.GetLength(0) == 3) return h;
    // то же самое по столбцам
    for (int j = 0; j < graphSize; j++)
    {
        double minInColumn = double.PositiveInfinity;
        for (int i = 0; i < graphSize; i++)
        {
            minInColumn = Math.Min(minInColumn, graph[i, j]);
        }
        h += minInColumn;
        for (int i = 0; i < graphSize; i++)
        {
            graph[i, j] -= minInColumn;
        }
    }
    // считаем степени нуля
    Dictionary<(double, double), double> cordsToZerosDegree = new();
    for (int i = 0; i < graphSize; i++)
    {
        for (int j = 0; j < graphSize; j++)
        {
            if (graph[i, j] == 0)
            {
                // находим минимум по строке и по столбцу (степень нуля)
                double minInLine = double.PositiveInfinity;
                double minInColumn = double.PositiveInfinity;
                for (int k = 0; k < graphSize; k++)
                {
                    // идём по строке
                    if (k != j) minInLine = Math.Min(minInLine, graph[i, k]);
                    // идём по столбцу
                    if (k != i) minInColumn = Math.Min(minInColumn, graph[k, j]);
                }
                cordsToZerosDegree[(graph[i, graphSize], graph[graphSize, j])] = minInLine + minInColumn;
            }
        }
    }
    // ищем ноль с наибольшей степенью
    (double, double) maxZeroCords = (-1, -1);
    double maxZero = -1;
    foreach (var cordsAndDegree in cordsToZerosDegree)
    {
        if (maxZero < cordsAndDegree.Value)
        {
            maxZero = cordsAndDegree.Value;
            maxZeroCords = cordsAndDegree.Key;
        }
    }
    // строим новый граф без строки и столбца на пересечении (тоже маркированный)
    double[,] nextGraph = new double[graphSize, graphSize];
    int nextGraphSize = nextGraph.GetLength(0) - 1;
    for (int i = 0; i <= graphSize; i++)
    {
        // не включаем строку, у которой номер совпадает с номером строки нуля
        if (graph[i, nextGraphSize + 1] != maxZeroCords.Item1)
        {
            for (int j = 0; j <= graphSize; j++)
            {
                // то же со столбцом
                if (graph[nextGraphSize + 1, j] != maxZeroCords.Item2)
                {
                    if (i < maxZeroCords.Item1)
                    {
                        if (j < maxZeroCords.Item2)
                        {
                            nextGraph[i, j] = graph[i, j];
                        }
                        else
                        {
                            nextGraph[i, j - 1] = graph[i, j];
                        }
                    }
                    else
                    {
                        if (j < maxZeroCords.Item2)
                        {
                            nextGraph[i - 1, j] = graph[i, j];
                        }
                        else
                        {
                            nextGraph[i - 1, j - 1] = graph[i, j];
                        }
                    }
                }
            }
        }
    }
    // предотвращаем зацикливание
    for (int i = 0; i <= nextGraphSize; i++)
    {
        if (nextGraph[i, nextGraphSize] == maxZeroCords.Item2)
        {
            for (int j = 0; j <= nextGraphSize; j++)
            {
                if (nextGraph[nextGraphSize, j] == maxZeroCords.Item1)
                    nextGraph[i, j] = double.PositiveInfinity;
            }
        }
    }
    return GraphDestroyer(nextGraph, h);
}
void PrintGraph(double[,] graph)
{
    int n = graph.GetLength(0);
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            Console.Write($"{graph[i, j]}\t");
        }
        Console.WriteLine();
    }
}
double[,] graph = { 
{double.PositiveInfinity, 1, 2, 3, 4 },
{14, double.PositiveInfinity, 15, 16, 5 },
{13, 20, double.PositiveInfinity, 17, 6 },
{12, 19, 18, double.PositiveInfinity, 7 },
{11, 10, 9, 8, double.PositiveInfinity } };
Console.WriteLine(VetviIGranici(graph));
