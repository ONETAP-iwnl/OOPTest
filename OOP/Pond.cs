using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Pond //пруд
    {
        public float MaxFishBiomass { get; set; }
        public float CurrentFishBiomass { get; set; }
        public int TotalFishCount { get; set; }
        public Dictionary<string, int> FishCountByType { get; set; }
        public float MaxFoodBiomass { get; set; }
        public float CurrentFoodBiomass { get; set; }
        public float DailyFoodGrowth { get; set; }
        private readonly Random random;


        private static readonly Dictionary<string, float> FishBiomass = new Dictionary<string, float>
        {
            { "Carp", 0.5f },
            { "Perch", 1.0f },
            { "Pike", 2.0f }
        }; //коллекция по биомассе рыб

        public Pond(float maxFishBiomass, float maxFoodBiomass, float dailyFoodGrowth)
        {
            this.MaxFishBiomass = maxFishBiomass;
            this.CurrentFishBiomass = 0;
            this.TotalFishCount = 0;
            this.FishCountByType = new Dictionary<string, int>();
            this.MaxFoodBiomass = maxFoodBiomass;
            this.CurrentFoodBiomass = maxFoodBiomass / 2; //начальное количество корма - половина максимального
            this.DailyFoodGrowth = dailyFoodGrowth;
            this.random = new Random();
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
            //копирую коллекцию для итерации симуляции дня
            var fishTypes = new List<string>(FishCountByType.Keys);

            //хищники
            foreach (var fishType in fishTypes)
            {
                if (fishType == "Pike") //щука
                {
                    float eatenFish = random.Next(0, 11) / 100.0f * CurrentFishBiomass; //количество съеденной рыбы
                    CurrentFishBiomass -= eatenFish; //уменьшаем биомассу рыбы
                }
            }

            foreach (var fishType in fishTypes)
            {
                if (fishType == "Perch") // окунь
                {
                    // Рыба
                    float eatenFish = random.Next(0, 11) / 100.0f * CurrentFishBiomass; // количество съеденной рыбы
                    CurrentFishBiomass -= eatenFish; // уменьшаем биомассу рыбы

                    // Корм
                    float eatenFood = Math.Min(CurrentFoodBiomass, FishCountByType[fishType] * FishCountByType[fishType] * 0.05f); // количество съеденного корма
                    CurrentFoodBiomass -= eatenFood; // уменьшаем количество корма
                }
            }

            //травоядные
            float totalFishFoodNeeded = 0;
            foreach (var fishType in fishTypes)
            {
                if (fishType != "Pike") //если не щука
                {
                    totalFishFoodNeeded += FishCountByType[fishType] * FishCountByType[fishType] * 0.05f; //общее количество корма
                }
            }
            if (CurrentFoodBiomass >= totalFishFoodNeeded)
            {
                CurrentFoodBiomass -= totalFishFoodNeeded; //уменьшаем количество корма
            }
            else
            {
                float deathFraction = 1 - (CurrentFoodBiomass / totalFishFoodNeeded); //если нехватает корма то чатсь рыбы умирает от голода
                foreach (var fishType in fishTypes)
                {
                    if (fishType != "Pike") //если не щука
                    {
                        FishCountByType[fishType] -= (int)Math.Round(FishCountByType[fishType] * deathFraction); //расчитываем количество умершей рыбы
                        TotalFishCount -= (int)Math.Round(FishCountByType[fishType] * deathFraction);
                    }
                }
                CurrentFoodBiomass = 0;
            }


            //увеличение количества корма
            CurrentFoodBiomass = Math.Min(MaxFoodBiomass, CurrentFoodBiomass + DailyFoodGrowth);

            //проверка смертности рыб
            foreach (var fishType in fishTypes)
            {
                for (int i = 0; i < FishCountByType[fishType]; i++)
                {
                    if (random.Next(100) < 10) //вероятность смерти - 10%
                    {
                        FishCountByType[fishType]--;
                        TotalFishCount--;
                    }
                }
            }
            CurrentFishBiomass = 0; //расчет общей биомассы рыбы по типам
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
