﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Perch : Fish //окунь(смешанное питание)
    {
        public Perch(float weight) : base(weight, 7, 2)
        {
            
        }

        public override void Eat(Fish prey)
        {
            if (prey is СrucianСarp || prey is Perch)
            {
                float eatenMass = prey.Weight * 0.1f; // 10% от массы съеденной рыбы
                Weight += eatenMass * 0.1f; //увеличиваем биомассу текущей рыбы на 10% от массы съеденной рыбы
            }
        }
    }
}
