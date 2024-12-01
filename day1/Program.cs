class Program
{
    static void Main(string[] args)
    {
        part2();
    }

    static string[] getLines()
    {
        return File.ReadAllLines("input.txt");
    }

    static void part1()
    {
        // create two min heaps, pop from them both until they are empty
        PriorityQueue<int, int> left = new();
        PriorityQueue<int, int> right = new();

        string[] lines = getLines();

        // process both lists at the same time
        foreach (string line in lines)
        {
            string[] split = line.Split("   ");
            left.Enqueue(int.Parse(split[0]), int.Parse(split[0]));
            right.Enqueue(int.Parse(split[1]), int.Parse(split[1]));
        }

        int sum = 0;

        while (left.Count > 0 && right.Count > 0)
        {
            int currentLeft = left.Dequeue();
            int currentRight = right.Dequeue();

            sum += Math.Abs(currentLeft - currentRight);
        }

        Console.WriteLine(sum);
    }

    static void part2()
    {
        // frequency map of the second list
        int sum = 0;

        string[] lines = getLines();

        Dictionary<string, int> right = [];

        // first populate the frequency map
        foreach (string line in lines)
        {
            string[] split = line.Split("   ");
            right[split[1]] = right.GetValueOrDefault(split[1], 0) + 1;

        }


        // now multiply it by the left list
        foreach (string line in lines)
        {
            string[] split = line.Split("   ");
            sum += int.Parse(split[0]) * right.GetValueOrDefault(split[0], 0);
        }

        Console.WriteLine(sum);

    }

}