using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    class Tabu
    {
        public LimitedQueue<State> TabuList;

        public Tabu()
        {
            TabuList = new LimitedQueue<State>(15);
        }

        public void Search(State state)
        {

            TabuList.Enqueue(state);//add to tabu list

            Console.WriteLine(state);

            if(state.FitnessFunction==0)
            {
                Console.WriteLine("Aswer is found!");
            }
            else
            {
                var childrenStates = GenerateChildrenStates(state);
                foreach(var item in childrenStates)
                {
                    if (!IsTaboo(item) /*&& item.FitnessFunction<state.FitnessFunction*/)//if it is not in the tabu list
                    {
                       // PrintTabuList();
                        Search(item);
                        break;
                    }
                }

          
            }

            
        }

        public bool IsTaboo(State state)
        {
            var containsState = TabuList.Any(o =>o.Barrel.ActualCapacity == state.Barrel.ActualCapacity
            && o.Jar1.ActualCapacity == state.Jar1.ActualCapacity && o.Jar2.ActualCapacity==state.Jar2.ActualCapacity );

            return containsState;
        }

        public void PrintTabuList()
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Tabu list");
            for (int i = 0; i < TabuList.Count; i++)
                Console.WriteLine(TabuList.ToArray()[i]);
            Console.WriteLine("-----------------------------------");
        }

        public List<State> GenerateChildrenStates(State state)
        {
            List<State> ChildrenStates = new List<State>();

            Jar barrel;
            Jar jar1;
            Jar jar2;

            //for state.Barrel
            if (state.Barrel.ActualCapacity != state.Barrel.MaxCapacity)//if a barrel is not full
            {
                if (state.Jar1.ActualCapacity != 0)//if state.Jar1 is not empty
                {
                    if (state.Jar1.ActualCapacity >= (state.Barrel.MaxCapacity - state.Barrel.ActualCapacity))//if jar1 has more water than needed
                    {
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.MaxCapacity);
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity - (state.Barrel.MaxCapacity - state.Barrel.ActualCapacity));
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else//jar1 has less water than needed
                    {
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity + state.Jar1.ActualCapacity);
                        jar1 = new Jar(state.Jar1.MaxCapacity, 0);
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }

                if (state.Jar2.ActualCapacity != 0)//if state.Jar2 is not empty
                {
                    if (state.Jar2.ActualCapacity >= (state.Barrel.MaxCapacity - state.Barrel.ActualCapacity))//if jar2 has more water than needed
                    {
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.MaxCapacity);
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity - (state.Barrel.MaxCapacity - state.Barrel.ActualCapacity));
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else//if jar2 has less water than needed
                    {
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity + state.Jar2.ActualCapacity);
                        jar2 = new Jar(state.Jar2.MaxCapacity, 0);
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }
            }
            //------------------------------------------------------------------------------------------------------------------------------------
            //For state.Jar1
            if (state.Jar1.ActualCapacity != state.Jar1.MaxCapacity)//if a jar1 is not full
            {
                if (state.Barrel.ActualCapacity != 0)//if state.Barrel is not empty
                {
                    if (state.Barrel.ActualCapacity >= (state.Jar1.MaxCapacity - state.Jar1.ActualCapacity))
                    {
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.MaxCapacity);
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity - (state.Jar1.MaxCapacity - state.Jar1.ActualCapacity));
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity + state.Barrel.ActualCapacity);
                        barrel = new Jar(state.Barrel.MaxCapacity, 0);
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }

                if (state.Jar2.ActualCapacity != 0)//if state.Jar2 is not empty
                {
                    if (state.Jar2.ActualCapacity >= (state.Jar1.MaxCapacity - state.Jar1.ActualCapacity))
                    {
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.MaxCapacity);
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity - (state.Jar1.MaxCapacity - state.Jar1.ActualCapacity));
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity + state.Jar2.ActualCapacity);
                        jar2 = new Jar(state.Jar2.MaxCapacity, 0);
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }
            }
            //------------------------------------------------------------------------------------------------------------------------------------------
            //For state.Jar2
            if (state.Jar2.ActualCapacity != state.Jar2.MaxCapacity)//if a jar2 is not full
            {
                if (state.Barrel.ActualCapacity != 0)//if state.Barrel is not empty
                {
                    if (state.Barrel.ActualCapacity >= (state.Jar2.MaxCapacity - state.Jar2.ActualCapacity))
                    {
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.MaxCapacity);
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity - (state.Jar2.MaxCapacity - state.Jar2.ActualCapacity));
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity + state.Barrel.ActualCapacity);
                        barrel = new Jar(state.Barrel.MaxCapacity, 0);
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }

                if (state.Jar1.ActualCapacity != 0)//if state.Jar1 is not empty
                {
                    if (state.Jar1.ActualCapacity >= (state.Jar2.MaxCapacity - state.Jar2.ActualCapacity))
                    {
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.MaxCapacity);
                        jar1 = new Jar(state.Jar1.MaxCapacity, state.Jar1.ActualCapacity - (state.Jar2.MaxCapacity - state.Jar2.ActualCapacity));
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                    else
                    {
                        jar2 = new Jar(state.Jar2.MaxCapacity, state.Jar2.ActualCapacity + state.Jar1.ActualCapacity);
                        jar1 = new Jar(state.Jar1.MaxCapacity, 0);
                        barrel = new Jar(state.Barrel.MaxCapacity, state.Barrel.ActualCapacity);
                        ChildrenStates.Add(new State(barrel, jar1, jar2));
                    }
                }
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------
            ChildrenStates.OrderBy(f => f.FitnessFunction);//sort children states by ascending of the fitness function

            return ChildrenStates;
        }


    }
}
