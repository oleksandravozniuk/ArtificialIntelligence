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

        public List<State> ChildrenStates;

        public State(Jar barrel, Jar jar1, Jar jar2)
        {
            Barrel = barrel;
            Jar1 = jar1;
            Jar2 = jar2;
            FitnessFunction = CalculateFitnessFunction();
            ChildrenStates = new List<State>();
        }

        public int CalculateFitnessFunction()
        {
            if(Barrel.ActualCapacity==6 || Jar2.ActualCapacity==6)
            {
                return 0;
            }
            else
            {
                if (Math.Abs(Barrel.ActualCapacity - 6) <= Math.Abs(Jar2.ActualCapacity - 6))
                    return Math.Abs(Barrel.ActualCapacity - 6);
                else
                    return Math.Abs(Jar2.ActualCapacity - 6);
            }
        }

        public void GenerateChildrenStates()
        {
            Jar barrel;
            Jar jar1;
            Jar jar2;

            //for Barrel
            if(Barrel.ActualCapacity!=Barrel.MaxCapacity)//if a barrel is not full
            {
                if(Jar1.ActualCapacity!=0)//if Jar1 is not empty
                {
                    if(Jar1.ActualCapacity>=(Barrel.MaxCapacity - Barrel.ActualCapacity))
                    {
                        barrel = new Jar(Barrel.MaxCapacity,Barrel.MaxCapacity);
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity - (Barrel.MaxCapacity - Barrel.ActualCapacity));
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity+Jar1.ActualCapacity);
                        jar1 = new Jar(Jar1.MaxCapacity, 0);
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }

                if (Jar2.ActualCapacity != 0)//if Jar2 is not empty
                {
                    if (Jar2.ActualCapacity >= (Barrel.MaxCapacity - Barrel.ActualCapacity))
                    {
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.MaxCapacity);
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity - (Barrel.MaxCapacity - Barrel.ActualCapacity));
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity + Jar2.ActualCapacity);
                        jar2 = new Jar(Jar2.MaxCapacity, 0);
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }
            }
            //------------------------------------------------------------------------------------------------------------------------------------
            //For Jar1
            if (Jar1.ActualCapacity != Jar1.MaxCapacity)//if a jar1 is not full
            {
                if (Barrel.ActualCapacity != 0)//if Barrel is not empty
                {
                    if (Barrel.ActualCapacity >= (Jar1.MaxCapacity - Jar1.ActualCapacity))
                    {
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.MaxCapacity);
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity - (Jar1.MaxCapacity - Jar1.ActualCapacity));
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity + Barrel.ActualCapacity);
                        barrel = new Jar(Barrel.MaxCapacity, 0);
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }

                if (Jar2.ActualCapacity != 0)//if Jar2 is not empty
                {
                    if (Jar2.ActualCapacity >= (Jar1.MaxCapacity - Jar1.ActualCapacity))
                    {
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.MaxCapacity);
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity - (Jar1.MaxCapacity - Jar1.ActualCapacity));
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity + Jar2.ActualCapacity);
                        jar2 = new Jar(Jar2.MaxCapacity, 0);
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }
            }
            //------------------------------------------------------------------------------------------------------------------------------------------
            //For Jar2
            if (Jar2.ActualCapacity != Jar2.MaxCapacity)//if a jar2 is not full
            {
                if (Barrel.ActualCapacity != 0)//if Barrel is not empty
                {
                    if (Barrel.ActualCapacity >= (Jar2.MaxCapacity - Jar2.ActualCapacity))
                    {
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.MaxCapacity);
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity - (Jar2.MaxCapacity - Jar2.ActualCapacity));
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity + Barrel.ActualCapacity);
                        barrel = new Jar(Barrel.MaxCapacity, 0);
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }

                if (Jar1.ActualCapacity != 0)//if Jar1 is not empty
                {
                    if (Jar1.ActualCapacity >= (Jar2.MaxCapacity - Jar2.ActualCapacity))
                    {
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.MaxCapacity);
                        jar1 = new Jar(Jar1.MaxCapacity, Jar1.ActualCapacity - (Jar2.MaxCapacity - Jar2.ActualCapacity));
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar2 = new Jar(Jar2.MaxCapacity, Jar2.ActualCapacity + Jar1.ActualCapacity);
                        jar1 = new Jar(Jar1.MaxCapacity, 0);
                        barrel = new Jar(Barrel.MaxCapacity, Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------
            ChildrenStates.OrderBy(f => f.FitnessFunction);
        }

        public override string ToString()
        {
            string result = "Fitness: " + FitnessFunction + " Barrel: " + Barrel + "\n Jar1 (max 5 litres): " + Jar1 + "\n Jar2 (max 7 litres): " + Jar2;
            return result;
        }
    }
}
