using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public abstract class Fish
    {
        public float Weight { get; set; }
        public int CurrentAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsHungry { get; set; }
        public int MaxDaysWithoutFood { get; set; }
        public int DaysWithoutFood { get; set; }

        
        public Fish(float weight, int maxAge, int maxDaysWithoutFood)
        {
            Weight = weight;
            CurrentAge = 0;
            MaxAge = maxAge;
            IsHungry = true;
            MaxDaysWithoutFood = maxDaysWithoutFood;
            DaysWithoutFood = 0;
        }

        public virtual void Eat(Fish prey)
        {
           
        }
    }
}
