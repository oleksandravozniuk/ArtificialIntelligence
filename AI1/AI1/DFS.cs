using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI1
{

    public class DFS
    {

        private List<Node> path = new List<Node>();

        public List<Node> visited = new List<Node>();

        public static int result = 3;

        public static int depthLimit = 30;

        public DFS()
        {


        }

        public void Search(Node root)
        {
            root.MakeChildren();//generate possible states
           
            foreach (var node in root._children)//for each state
            { 
                if (!IsResult())//if result is not found
                {
                    visited.Add(root);//add to visited nodes

                    if (!PathHasNode(node)  && visited.Count<=depthLimit)//if path don't have this node
                    {
                        path.Add(root);//add to path
                        Search(node);//continue recursion with children node
                    }
                }

            }
        }

        public bool IsResult()//check whether result is in the path or no
        {
            foreach(var node in path)
            {
                if (node.Jar1 == result || node.Jar2 == result)
                {
                    return true;
                }
            }

            return false;
        }

        public void GetPath()//print path
        {
            foreach(var node in path)
            {
               Console.WriteLine(node);
            }
        }

        public bool PathHasNode(Node n)//check if a concrete node is present in the path
        {
            foreach (var node in path)
            {
                if (node.Jar1 == n.Jar1 && node.Jar2 == n.Jar2)
                {
                    return true;
                }
            }

            return false;
        }

        public void printTree(Node root)
        {
            
            
            foreach(var node in root._children)
            {
                Console.WriteLine(node);
                Console.WriteLine();

               
                    printTree(node);
            }
        }




    }
}
