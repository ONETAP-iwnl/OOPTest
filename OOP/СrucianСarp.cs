using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class СrucianСarp : Fish //карась(ест только корм)
    {
        public СrucianСarp(float weight) : base(weight, 10, 3)
        {
            // Дополнительная инициализация для карпа
        }

        public override void Eat(Fish prey)
        {
            
        }
    }
}
