using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2
{
    class RBFS
    {
        public State root { get; private set; }

        public List<State> path = new List<State>();
        public RBFS(State root)
        {
            this.root = root;
        }

        

        public void Search(State root)
        {

            if (root.H != 0)
            {
                path.Add(root);//add to path

                root.GenerateChildren();//generate possible states

                foreach (var state in root.Children)//for each children
                {

                    if (!PathHasState(state) && MinNumOfWrongPositions(state))//if path does not have this node and the node has min number of wrong positions
                    {


                        Search(state);//continue recursion with children node


                    }
                }
            }

            else
                path.Add(root);
                        
        }
          

        public bool PathHasState(State n)//check if a concrete node is present in the path
        {
            foreach (var state in path)
            {
                for(var i=0;i<3;i++)
                {
                    for(var j=0;j<3;j++)
                    {
                        if (state.ValueMatrix[i, j] != n.ValueMatrix[i, j])
                            return false;
                    }
                }
            }

            return true;
        }

        public bool MinNumOfWrongPositions(State state)//true if state has min number of wrong positions among all possible
        {

            if(state.parent!=null)
            {
                foreach(var item in state.parent.Children)
                {
       
                        if (state.H > item.H)
                            return false;
                    
                }
            }

            return true;
            
        }

        public override string ToString()
        {
            string str = String.Empty;

            foreach(var item in path)
            {
                str+=item.ToString();
            }

            return str;
        }

    }

    
}
