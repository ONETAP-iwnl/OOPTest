using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Pond //пруд
    {
        // Свойства пруда
        public float MaxFishBiomass { get; set; }
        public float CurrentFishBiomass { get; set; }
        public int TotalFishCount { get; set; }
        public Dictionary<string, int> FishCountByType { get; set; }
        public float MaxFoodBiomass { get; set; }
        public float CurrentFoodBiomass { get; set; }
        public float DailyFoodGrowth { get; set; }



        // Таблица биомассы рыбы по типам
        private static readonly Dictionary<string, float> FishBiomass = new Dictionary<string, float>
        {
            { "Carp", 0.5f },
            { "Perch", 1.0f },
            { "Pike", 2.0f }
        };




        private Random random;

        public Pond(float maxFishBiomass, float maxFoodBiomass, float dailyFoodGrowth)
        {
            MaxFishBiomass = maxFishBiomass;
            CurrentFishBiomass = 0;
            TotalFishCount = 0;
            FishCountByType = new Dictionary<string, int>();
            MaxFoodBiomass = maxFoodBiomass;
            CurrentFoodBiomass = maxFoodBiomass / 2; // Начальное количество корма - половина максимального
            DailyFoodGrowth = dailyFoodGrowth;
            random = new Random();
        }

        
        public void AddFish(string fishType, int count)
        {
            if (!FishCountByType.ContainsKey(fishType))
            {
                FishCountByType.Add(fishType, count);
            }
            else
            {
                FishCountByType[fishType] += count;
            }

            TotalFishCount += count;
        }

        
        public void SimulateDay()
        {
            // копирую коллекцию для итерации симуляции дня
            var fishTypes = new List<string>(FishCountByType.Keys);

            //хищники
            foreach (var fishType in fishTypes)
            {
                if (fishType == "Pike") // щука
                {
                    float eatenFish = random.Next(0, 11) / 100.0f * CurrentFishBiomass;
                    CurrentFishBiomass -= eatenFish;
                }
            }

            //травоядные
            float totalFishFoodNeeded = 0;
            foreach (var fishType in fishTypes)
            {
                if (fishType != "Pike") // если не щука
                {
                    totalFishFoodNeeded += FishCountByType[fishType] * FishCountByType[fishType] * 0.05f;
                }
            }
            if (CurrentFoodBiomass >= totalFishFoodNeeded)
            {
                CurrentFoodBiomass -= totalFishFoodNeeded;
            }
            else
            {
                // Часть рыбы умирает от голода
                float deathFraction = 1 - (CurrentFoodBiomass / totalFishFoodNeeded);
                foreach (var fishType in fishTypes)
                {
                    if (fishType != "Pike") // если не щука
                    {
                        FishCountByType[fishType] -= (int)Math.Round(FishCountByType[fishType] * deathFraction);
                        TotalFishCount -= (int)Math.Round(FishCountByType[fishType] * deathFraction);
                    }
                }
                CurrentFoodBiomass = 0;
            }

            // Увеличение количества корма
            CurrentFoodBiomass = Math.Min(MaxFoodBiomass, CurrentFoodBiomass + DailyFoodGrowth);

            // Проверка смертности рыб
            foreach (var fishType in fishTypes)
            {
                for (int i = 0; i < FishCountByType[fishType]; i++)
                {
                    if (random.Next(100) < 10) // Вероятность смерти - 10%
                    {
                        FishCountByType[fishType]--;
                        TotalFishCount--;
                    }
                }
            }


            CurrentFishBiomass = 0;
            foreach (var fishType in FishCountByType.Keys)
            {
                float fishTypeBiomass = FishCountByType[fishType] * FishBiomass[fishType];
                CurrentFishBiomass += fishTypeBiomass;
            }

            Console.WriteLine($"Статистика пруда:");
            Console.WriteLine($"Текущая биомасса рыбы: {CurrentFishBiomass}");
            Console.WriteLine($"Текущее количество рыбы: {TotalFishCount}");
            Console.WriteLine($"Текущая биомасса корма: {CurrentFoodBiomass}");
        }

    }
    
}
