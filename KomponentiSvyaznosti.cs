int ConnectivityСomponents(int[,] graph)
{
    int dotsAmount = graph.GetLength(0);
    SortedSet<int> dots = new(); 
    for (int i = 0; i < dotsAmount; i++) dots.Add(i); // составляем множество всех точек графа (нумеруем с нуля)
    int componentsAmount = 0;
    Queue<int> dotsToWork = new();
    int currentDot = 0; 
    dotsToWork.Enqueue(currentDot);
    while (dots.Count != 0) // пока не обработаем все точки
    {
        currentDot = dots.First(); // указываем начальную точку
        dotsToWork.Enqueue(currentDot); // ставим её в начало очереди
        while (dotsToWork.Count != 0) // пока текущая очередь не закончится
        {
            for (int i = 0; i < dotsAmount; i++)
            {
                if (graph[currentDot, i] == 1 && dots.Contains(i)) // если из текущей точки добираемся до i-ой и она ещё не обработана,
                {
                    dotsToWork.Enqueue(i); // то добавляем её в очередь на обработку
                }
            }
            dots.Remove(currentDot); // удаляем обработанную точку
            currentDot = dotsToWork.Dequeue(); // делаем точку из начала очереди текущей
        }
        componentsAmount++;
    }
    return componentsAmount;
}
// тесты
int[,] graph = new int[,]
{
    {0, 1, 0, 0, 0, 0, 0},
    {1, 0, 1, 0, 0, 0, 0},
    {0, 1, 0, 1, 0, 0, 0},
    {0, 0, 1, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 1},
    {0, 0, 0, 0, 0, 1, 0}
};
int[,] graph2 = new int[,]
{
    {0, 1, 0, 0, 0 },
    {1, 0, 1, 0, 1 },
    {0, 1, 0, 1, 0 },
    {0, 0, 1, 0, 0 },
    {0, 1, 0, 0, 0 },
};
Console.WriteLine(ConnectivityСomponents(graph2));
