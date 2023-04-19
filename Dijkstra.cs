double[] Dijkstra(int start_dot, double[,] graph)
{
    // создаём и заполняем множество точек графа
    HashSet<double> dots = new(); 
    for (int i = 0; i < graph.Length / 3; i++)
    {
        dots.Add(graph[0, i]);
        dots.Add(graph[1, i]);
    }
    // массив длин из заданной точки до каждой точки
    double[] paths = new double[dots.Count];
    // задаём расстояние до каждой точки, кроме стартовой, равное бесконечности
    for (int i = 0; i < paths.Length; i++)
    {
        if (i + 1 != start_dot) paths[i] = double.PositiveInfinity;
    }
    // точка, являющаяся началом дуги в графе, на каждой итерации
    int current_dot = start_dot;
    // пока остались непосещённые точки (точка посещена, если удалена из множества точек)
    while (dots.Count != 0)
    { // проходимся по всем дугам из текущей точки
        for (int i = 0; i < graph.Length / 3; i++)
        { // рассматриваем дуги, которые исходят из текущей точки и которые ведут в ещё не посещённую точку
            if (graph[0, i] == current_dot && dots.Contains((int)graph[1, i]))
            { // если предыдущее расстояние до точки оказывается больше, чем из текущей точки, то заменяем его
                if (paths[(int)graph[1, i] - 1] > paths[current_dot - 1] + graph[2, i])
                {
                    paths[(int)graph[1, i] - 1] = paths[current_dot - 1] + graph[2, i];
                }
            }
        }
        // удаляем посещённую точку из множества
        dots.Remove(current_dot);
        // текущей точкой становится непомеченная точка, расстояние до которой меньше всего
        double min_path = double.PositiveInfinity;
        int min_dot = -1;
        for (int i = 0; i < paths.Length; i++)
        {
            if (min_path > paths[i] && dots.Contains(i+1))
            {
                min_path = paths[i];
                min_dot = i + 1;
            }
        }
        current_dot = min_dot;
    }
    // возвращаем массив кратчайших путей из стартовой точки в каждую
    return paths;
}

double[,] graph = {
    { 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 5, 6, 6, 7, 7, 7, 8, 8, 8, 8, 9, 9, 10, 11, 2, 5, 6, 3, 4, 5, 6, 4, 5, 5, 7, 8, 6, 7, 8, 9, 8, 9, 8, 10, 11, 9, 10, 11, 12, 11, 12, 11, 12},
    { 2, 5, 6, 3, 4, 5, 6, 4, 5, 5, 7, 8, 6, 7, 8, 9, 8, 9, 8, 10, 11, 9, 10, 11, 12, 11, 12, 11, 12, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 4, 5, 5, 5, 5, 6, 6, 7, 7, 7, 8, 8, 8, 8, 9, 9, 10, 11},
    { 7, 9, 2, 5, 4, 8, 2, 2, 9, 3, 8, 3, 3, 5, 1, 7, 6, 1, 6, 4, 4, 2, 7, 8, 5, 6, 1, 10, 3, 7, 9, 2, 5, 4, 8, 2, 2, 9, 3, 8, 3, 3, 5, 1, 7, 6, 1, 6, 4, 4, 2, 7, 8, 5, 6, 1, 10, 3}
};
/*
// Заполнение графа. Граф заполняется по дугам: первое число - точка исхода дуги, второе число - точка захода, третье число - вес
int n = Convert.ToInt32(Console.ReadLine());
double[,] graph = new double[3, n];
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < 3; j++)
    {
        graph[j, i] = Convert.ToDouble(Console.ReadLine());
    }
}
*/
double[] result = Dijkstra(12, graph);
for (int i = 0; i < result.Length; i++)
{
    Console.Write($"{i+1}: {result[i]}\t");
}