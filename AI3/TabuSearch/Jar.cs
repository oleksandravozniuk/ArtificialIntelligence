using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public struct Jar
    {
        public int MaxCapacity { get; set; }
        public int ActualCapacity { get; set; }

        public Jar(int maxCapacity, int actualCopacity)
        {
            MaxCapacity = maxCapacity;
            ActualCapacity = actualCopacity;
        }

        public override string ToString() => $"({ActualCapacity})";
    }
}
