using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    namespace OOP
    {
        public class Pond //пруд
        {
            public float MaxFishBiomass { get; } // Максимальная биомасса рыбы, задается при создании пруда
            public float CurrentFishBiomass { get; private set; } // Текущая биомасса рыбы
            public int TotalFishCount { get; private set; } // Общее количество рыб
            public Dictionary<string, int> FishCountByType { get; } // Количество рыб по типам
            public Dictionary<string, int> DeadFishCountByType { get; } // Количество умерших рыб по типам
            public float MaxFoodBiomass { get; } // Максимальная биомасса корма
            public float CurrentFoodBiomass { get; private set; } // Текущая биомасса корма
            public float DailyFoodGrowth { get; } // Ежедневный прирост корма
            private readonly Random random; // Генератор случайных чисел для моделирования случайных событий

            // Коллекция по биомассе рыб
            public static readonly Dictionary<string, float> FishBiomass = new Dictionary<string, float>
        {
            { "Carp", 0.5f },
            { "Perch", 1.0f },
            { "Pike", 2.0f }
        };

            // Конструктор пруда
            public Pond(float maxFishBiomass, float maxFoodBiomass, float dailyFoodGrowth)
            {
                MaxFishBiomass = maxFishBiomass;
                CurrentFishBiomass = 0;
                TotalFishCount = 0;
                FishCountByType = new Dictionary<string, int>();
                DeadFishCountByType = new Dictionary<string, int>();
                MaxFoodBiomass = maxFoodBiomass;
                CurrentFoodBiomass = maxFoodBiomass;
                DailyFoodGrowth = dailyFoodGrowth * 2; // Увеличиваем прирост корма в 2 раза
                random = new Random();
            }

            public void AddFish(string fishType, int count)
            {
                float addedBiomass = count * FishBiomass[fishType];
                if (CurrentFishBiomass + addedBiomass > MaxFishBiomass)
                {
                    Console.WriteLine("Превышена максимальная биомасса рыбы в пруду!");
                    return;
                }

                if (!FishCountByType.ContainsKey(fishType))
                {
                    FishCountByType[fishType] = count;
                }
                else
                {
                    FishCountByType[fishType] += count;
                }

                TotalFishCount += count;
                CurrentFishBiomass += addedBiomass;
            }

            public void SimulateDay()
            {
                var fishTypes = new List<string>(FishCountByType.Keys);
                foreach (var fishType in fishTypes)
                {
                    switch (fishType)
                    {
                        case "Pike":
                            HandlePike();
                            break;
                        case "Perch":
                            HandlePerch();
                            break;
                        default:
                            HandleHerbivores(fishType);
                            break;
                    }
                }
                RandomFishDeath();
                UpdateFood();
                UpdateFishCount();
                PrintStatistics();
            }
            private void HandlePike()
            {
                if (CurrentFishBiomass <= 0) return;

                float eatenFish = random.Next(0, 11) / 100.0f * CurrentFishBiomass;
                CurrentFishBiomass -= eatenFish;

                foreach (var fishType in FishCountByType.Keys.ToList())
                {
                    if (fishType != "Pike")
                    {
                        int preyCount = FishCountByType[fishType];
                        int eatenPreyCount = (int)(preyCount * eatenFish / CurrentFishBiomass);

                        FishCountByType[fishType] -= eatenPreyCount;
                        TotalFishCount -= eatenPreyCount;

                        Console.WriteLine($"Pike ate {eatenPreyCount} {fishType}(s)");
                    }
                }
            }

            private void HandlePerch()
            {
                if (CurrentFishBiomass <= 0 || CurrentFoodBiomass <= 0) return;

                float eatenFish = random.Next(0, 11) / 100.0f * CurrentFishBiomass;
                CurrentFishBiomass -= eatenFish;
                float eatenFood = Math.Min(CurrentFoodBiomass, FishCountByType["Perch"] * FishCountByType["Perch"] * 0.05f);
                CurrentFoodBiomass -= eatenFood;
                FishBiomass["Perch"]++;

                if (CurrentFishBiomass > MaxFishBiomass)
                {
                    CurrentFishBiomass = MaxFishBiomass;
                }
            }

            private void HandleHerbivores(string fishType)
            {
                if (CurrentFoodBiomass <= 0) return;

                float totalFoodNeeded = FishCountByType[fishType] * FishCountByType[fishType] * 0.01f;
                float availableFood = Math.Min(CurrentFoodBiomass, totalFoodNeeded);

                if (availableFood < totalFoodNeeded)
                {
                    int deathCount = (int)Math.Round(FishCountByType[fishType] * (1 - (availableFood / totalFoodNeeded)));
                    DeadFishCountByType[fishType] = deathCount;
                    FishCountByType[fishType] -= deathCount;
                    TotalFishCount -= deathCount;
                }

                CurrentFoodBiomass -= totalFoodNeeded;

                if (CurrentFishBiomass > MaxFishBiomass)
                {
                    CurrentFishBiomass = MaxFishBiomass;
                }
            }

            private void UpdateFood()
            {
                float totalFishFoodNeeded = 0;
                foreach (var fishType in FishCountByType.Keys.ToList())
                {
                    if (fishType != "Pike")
                    {
                        totalFishFoodNeeded += FishCountByType[fishType] * FishCountByType[fishType] * 0.05f;
                    }
                }

                List<string> fishToRemove = new List<string>();

                if (CurrentFoodBiomass < totalFishFoodNeeded)
                {
                    float deathFraction = 1 - (CurrentFoodBiomass / totalFishFoodNeeded);
                    foreach (var fishType in FishCountByType.Keys.ToList())
                    {
                        if (fishType != "Pike")
                        {
                            int deadFishCount = (int)Math.Round(FishCountByType[fishType] * deathFraction);
                            DeadFishCountByType[fishType] = deadFishCount;
                            FishCountByType[fishType] -= deadFishCount;
                            TotalFishCount -= deadFishCount;

                            if (FishCountByType[fishType] <= 0)
                            {
                                fishToRemove.Add(fishType);
                            }
                        }
                    }
                    CurrentFoodBiomass = 0;
                }
                else
                {
                    CurrentFoodBiomass -= totalFishFoodNeeded;
                }

                foreach (var fishTypeToRemove in fishToRemove)
                {
                    FishCountByType.Remove(fishTypeToRemove);
                }
            }


            public void RandomFishDeath()
            {
                List<string> fishToRemove = new List<string>(); // Создаем временную коллекцию для удаления ключей

                foreach (var fishType in FishCountByType.Keys.ToList())
                {
                    int fishCount = FishCountByType[fishType];
                    for (int i = 0; i < fishCount; i++)
                    {
                        double randomChance = random.NextDouble(); // случайное число от 0 до 1
                        if (randomChance <= 0.01) // 1% шанс
                        {
                            FishCountByType[fishType]--;
                            TotalFishCount--;
                            Console.WriteLine($"Рыбу отловили: {fishType}");
                        }
                    }
                    if (FishCountByType[fishType] <= 0)
                    {
                        fishToRemove.Add(fishType);
                    }
                }

                // Удаляем ключи из FishCountByType
                foreach (var fishTypeToRemove in fishToRemove)
                {
                    FishCountByType.Remove(fishTypeToRemove);
                }
            }
            private void UpdateFishCount()
            {
                CurrentFishBiomass = 0;
                foreach (var fishType in FishCountByType.Keys)
                {
                    CurrentFishBiomass += FishCountByType[fishType] * FishBiomass[fishType];
                }
            }

            private void PrintStatistics()
            {
                Console.WriteLine($"Статистика пруда:");
                Console.WriteLine($"Текущая биомасса рыбы: {CurrentFishBiomass}");
                Console.WriteLine($"Текущее количество рыбы: {TotalFishCount}");
                Console.WriteLine($"Текущая биомасса корма: {CurrentFoodBiomass}");
                Console.WriteLine($"Количество умерших рыб: {CalculateTotalDeadFishCount()}");
            }

            private int CalculateTotalDeadFishCount()
            {
                int totalDeadFishCount = 0;
                foreach (var deadFishCount in DeadFishCountByType.Values)
                {
                    totalDeadFishCount += deadFishCount;
                }
                return totalDeadFishCount;
            }
        }
    }
}
