int[,] graph = { {1,1,1,2,2,2,3,3,4,4,4,5,5,5,7,8}, {2,5,4,3,4,5,5,6,5,7,8,6,8,9,8,9}, {15,14,23,19,16,15,14,26,25,23,20,24,27,18,14,18 } };

/* заполнение вручную
int n = Convert.ToInt32(Console.ReadLine());
int[,] graph = new int[3, n];
for (int j = 0; j < 3; j++)
{
    for (int i = 0; i < n; i++)
    {
        graph[j, i] = Convert.ToInt32(Console.ReadLine());
    }
}
*/

void Prima(int [,] lines)
{
    int starting_dot = 1;
    List<int> used_dots = new() { starting_dot };
    int min_weight;
    int weight = 0;
    int i_with_min_weight;

    HashSet<int> dots = new();
    for (int i = 0; i < graph.Length / 3; i++)
    {
        dots.Add(graph[0, i]);
        dots.Add(graph[1, i]);
    }    

    for (int dots_count = 1; dots_count != dots.Count; dots_count++)
    {
        min_weight = int.MaxValue;
        i_with_min_weight = -1;
        for (int i = 0; i < lines.Length / 3; i++)
        {
            if (used_dots.Contains(lines[0, i]) && (used_dots.Contains(lines[0, i]) && used_dots.Contains(lines[1, i])) == false)
            {
                if (min_weight > Math.Min(lines[2, i], min_weight))
                {
                    min_weight = Math.Min(lines[2, i], min_weight);
                    i_with_min_weight = i;
                }
            }
            if (used_dots.Contains(lines[1, i]) && (used_dots.Contains(lines[0, i]) && used_dots.Contains(lines[1, i])) == false)
            {
                if (min_weight > Math.Min(lines[2, i], min_weight))
                {
                    min_weight = Math.Min(lines[2, i], min_weight);
                    i_with_min_weight = i;
                }
            }
        }
        weight += min_weight;
        if (used_dots.Contains(lines[0, i_with_min_weight])) used_dots.Add(lines[1, i_with_min_weight]);
        else used_dots.Add(lines[0, i_with_min_weight]);
    }
    Console.WriteLine(weight);
}

void Kraskala(int[,] lines)
{
    for (int k = 0; k < lines.Length / 3; k++)
    {
        int min_weight = int.MaxValue;
        int i_with_min_weight = -1;
        for (int i = k; i < lines.Length / 3; i++)
        {
            if (min_weight > Math.Min(lines[2, i], min_weight))
            {
                min_weight = Math.Min(lines[2, i], min_weight);
                i_with_min_weight = i;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            int c;
            c = lines[i, k];
            lines[i, k] = lines[i, i_with_min_weight];
            lines[i, i_with_min_weight] = c;
        }
    }

    int weight = 0;
    List<HashSet<int>> sets = new();

    for (int i = 0; i < lines.Length / 3; i++)
    {
        bool cycle = false;
        int first_dot = -1;
        int second_dot = -1;
        for (int j = 0; j < sets.Count; j++)
        {
            if (sets[j].Contains(lines[0, i]) && sets[j].Contains(lines[1, i]))
            {
                cycle = true;
                break;
            }
            else if (sets[j].Contains(lines[0, i])) first_dot = j;
            else if (sets[j].Contains(lines[1, i])) second_dot = j;
        }

        if (cycle == false)
        {
            if (first_dot != -1 && second_dot == -1) sets[first_dot].Add(lines[1, i]);
            else if (first_dot == -1 && second_dot != -1) sets[second_dot].Add(lines[0, i]);
            else if (first_dot == -1 && second_dot == -1) sets.Add(new HashSet<int> { lines[0, i], lines[1, i] });
            else
            {
                sets[first_dot].UnionWith(sets[second_dot]);
                sets.RemoveAt(second_dot);
            }
            weight += lines[2, i];
        }

    }
    Console.WriteLine(weight);
}

Prima(graph);
Kraskala(graph);