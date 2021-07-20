using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEG
{
    record Continent
    {
        public string Name { get; init; }
        public int Bonus { get; init; }
        public int Size { get; init; }
        public Continent(string name, int bonus, int size)
        {
            Name = name;
            Bonus = bonus;
            Size = size;
        }
    }
}
