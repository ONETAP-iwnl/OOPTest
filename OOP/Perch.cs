using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Perch : Fish //Окунь(смешанное питание)
    {
        public Perch(float weight) : base(weight, 7, 2)
        {
            // Дополнительная инициализация для окуня
        }
    }
}
