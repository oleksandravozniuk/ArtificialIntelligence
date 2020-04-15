using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    class Tabu
    {
        public Queue<State> TabuList;
        //public State ActualState { get; set; }

        public Tabu()
        {
            //ActualState = actualState;
            TabuList = new Queue<State>();
        }

        public void Search(State state)
        {
            Console.WriteLine(state);

                state.GenerateChildrenStates();
            if (state.FitnessFunction != 0)
            {
                for (int i = 0; i < state.ChildrenStates.Count; i++)
                {
                    if (state.FitnessFunction != 0)
                    {
                        if (!IsTaboo(state.ChildrenStates[i]))
                        {
                            TabuList.Enqueue(state);
                            //PrintTabuList();
                            Search(state.ChildrenStates[i]);
                        }
                    }
                    else
                    {
                        Console.WriteLine(state.ChildrenStates[i]);
                        Console.WriteLine("Answer is found!");
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
            Console.WriteLine("Tabu list");
            for (int i = 0; i < TabuList.Count; i++)
                Console.WriteLine(TabuList.Peek());
        }


    }
}
