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
            if (prey is СrucianСarp || prey is Perch)
            {
                float eatenMass = prey.Weight * 0.2f; // 20% от массы съеденной рыбы
                Weight += eatenMass * 0.1f; //увеличиваем биомассу текущей рыбы на 10% от массы съеденной рыбы
            }
        }
    }
}
