using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

class Program
{
    public static void Main(string[] args)
    {
        part2();
    }
    static string[] getInput()
    {
        return File.ReadAllLines("input.txt");
    }
    static void part1()
    {
        // read each line
        string[] lines = getInput();
        int sum = 0;
        foreach (string line in lines)
        {
            int iterator = 1;
            int[] values = Array.ConvertAll(line.Split(" "), int.Parse);
            int previous = values[0];
            // increasing
            if (values[1] - previous > 0)
            {
                foreach (int current in new ArraySegment<int>(values, 1, values.Length - 1))
                {
                    if (current <= previous || current - previous > 3)
                    {
                        iterator = 0;
                        break;
                    }
                    previous = current;
                }
            }
            //decreasing 
            else if (values[1] - previous < 0)
            {
                foreach (int current in new ArraySegment<int>(values, 1, values.Length - 1))
                {
                    if (current >= previous || previous - current > 3)
                    {
                        iterator = 0;
                        break;
                    }
                    previous = current;
                }
            }
            else
            {
                iterator = 0;
            }
            sum += iterator;
        }
        Console.WriteLine(sum);
    }
    static void part2()
    {
        // read each line
        string[] lines = getInput();
        int sum = 0;
        foreach (string line in lines)
        {
            int[] values = Array.ConvertAll(line.Split(" "), int.Parse);

            if(isValidLine([.. values], 0)) {
                sum += 1;
            } 
            
        }

        Console.WriteLine(sum);
    }

    static bool isValidLine(List<int> values, int depth)
    {
        if(depth > 1) {
            return false;
        }

        int previous = values[0];
        // increasing
        if (values[1] - previous > 0)
        {
            for (int i = 1; i < values.Count;i++) 
            {
                if(values[i] <= previous || values[i]-previous > 3) {
                    List<int> removeCurrent = values.GetRange(0, values.Count);
                    List<int> removePrevious = values.GetRange(0, values.Count);

                    removeCurrent.RemoveAt(i);
                    removeCurrent.RemoveAt(i - 1);
                    return isValidLine(removeCurrent, depth + 1) || isValidLine(removePrevious, depth + 1);
                }
                previous = values[i];
            }
        } else if (values[1] - previous < 0) {
            for(int i = 1; i < values.Count;i++) 
            {
                if(values[i] >= previous || previous-values[i] > 3) {
                    List<int> removeCurrent = values.GetRange(0, values.Count);
                    List<int> removePrevious = values.GetRange(0, values.Count);

                    removeCurrent.RemoveAt(i);
                    removeCurrent.RemoveAt(i - 1);
                    return isValidLine(removeCurrent, depth + 1) || isValidLine(removePrevious, depth + 1);
                }
                previous = values[i];
            }
        } else {
            values.RemoveAt(0);
            return isValidLine(values, depth + 1);
        } 

        return true;
    }
}