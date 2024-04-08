using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Pike: Fish //щука (ест только рыбу)
    {
        public Pike(float weight) : base(weight, 15, 4)
        {
            
        }

        public override void Eat(Fish prey)
        {
            // Питание щуки
            if (prey is СrucianСarp || prey is Perch)
            {
                Weight += prey.Weight * 0.2f; // Щука увеличивает свой вес на 20% от съеденной рыбы
            }
        }
    }
}
