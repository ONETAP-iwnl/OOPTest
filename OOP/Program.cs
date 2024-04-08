using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pond pond = new Pond(1000, 2000, 100);
            pond.AddFish("Carp", 50);
            pond.AddFish("Perch", 30);
            pond.AddFish("Pike", 10);
            for (int i = 1; i <= 30; i++)
            {
                Console.WriteLine($"День {i}:");
                pond.SimulateDay();
                Console.WriteLine();
            }
        }
    }
}
