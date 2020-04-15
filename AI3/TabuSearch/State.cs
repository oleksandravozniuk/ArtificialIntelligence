using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public class State
    {
        public Jar Barrel { get; set; }//barrel with 12 liters
        public Jar Jar1 { get; set; }//jar with max capacity 5 litres
        public Jar Jar2 { get; set; }//jar with max capacity 7 litres

        public int FitnessFunction { get; set; }

       

        public State(Jar barrel, Jar jar1, Jar jar2)
        {
            Barrel = barrel;
            Jar1 = jar1;
            Jar2 = jar2;
            FitnessFunction = CalculateFitnessFunction();
           
        }

        public int CalculateFitnessFunction()
        {
            if(Barrel.ActualCapacity==6 || Jar2.ActualCapacity==6)//it means we have reached the result
            {
                return 0;
            }
            else
            {
                if (Math.Abs(Barrel.ActualCapacity - 6) <= Math.Abs(Jar2.ActualCapacity - 6))//count difference between result and actual state
                    return Math.Abs(Barrel.ActualCapacity - 6);
                else
                    return Math.Abs(Jar2.ActualCapacity - 6);
            }
        }

       

        public override string ToString()
        {
            string result = "Fitness: " + FitnessFunction + " Barrel: " + Barrel + "\n Jar1 (max 5 litres): " + Jar1 + "\n Jar2 (max 7 litres): " + Jar2 +"\n";
            return result;
        }
    }
}
