using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public class StateComparer : IEqualityComparer<State>
    {
        public bool Equals(State x, State y)
        {
            if (x.Barrel.ActualCapacity == y.Barrel.ActualCapacity && x.Jar1.ActualCapacity == y.Jar1.ActualCapacity && x.Jar2.ActualCapacity == y.Jar2.ActualCapacity)
                return true;
            else
                return false;
        }

        public int GetHashCode(State obj)
        {
            return obj.GetHashCode();
        }
    }
}
