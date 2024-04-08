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
            // Дополнительная инициализация для щуки
        }
    }
}
