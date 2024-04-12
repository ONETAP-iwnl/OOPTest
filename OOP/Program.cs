using OOP.OOP;
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
            Pond pond = new Pond(1000, 2000, 900);
            Console.WriteLine("Введите количество рыб каждого вида:");

            foreach (var fishType in Pond.FishBiomass.Keys)
            {
                Console.Write($"{fishType}: ");
                int count = int.Parse(Console.ReadLine());
                pond.AddFish(fishType, count);
            }

            string userInput;
            int daysToSimulate = 1;

            do
            {
                Console.WriteLine($"Выберите действие:");   
                Console.WriteLine("Введите '+' для следующего дня, или введите число для выбора конкретного дня:");
                userInput = Console.ReadLine();

                if (userInput == "+")
                {
                    Console.WriteLine($"День {daysToSimulate}:");
                    pond.SimulateDay();
                    daysToSimulate++;
                }
                else if (int.TryParse(userInput, out int selectedDay) && selectedDay > 0)
                {
                    for (int i = daysToSimulate; i <= selectedDay; i++)
                    {
                        Console.WriteLine($"День {i}:");
                        pond.SimulateDay();
                    }
                    daysToSimulate = selectedDay + 1;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }

                Console.WriteLine();
            } while (true);
        }
    }
}
