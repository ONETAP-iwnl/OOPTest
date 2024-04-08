using System;
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
            //питание окуня
            if (prey is СrucianСarp || prey is Perch)
            {
                Weight += prey.Weight * 0.1f; //окунь увеличивает свой вес на 10% от съеденной рыбы
            }
        }
    }
}
