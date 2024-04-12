using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Pike: Fish //щука (ест только рыбу)
    {
        public Pike(float weight) : base(weight, 15, 2)
        {
            
        }

        public override void Eat(Fish prey)
        {
            if (prey is СrucianСarp || prey is Perch)
            {
                float eatenMass = prey.Weight * 0.1f; // 10% от массы съеденной рыбы
                prey.Weight -= eatenMass; // Уменьшаем вес съеденной рыбы у жертвы
                this.Weight += eatenMass * 0.5f; // Увеличиваем биомассу щуки в 2 раза
            }
        }

        
    }
}
