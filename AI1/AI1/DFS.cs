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

        public DFS()
        {


        }

        public void Search(Node root)
        {
            visited.Add(root);

            root.MakeChildren();
           
            foreach (var node in root._children)
            { 
                if (!IsResult())
                {
                    
                    if (PathHasNode(node) == false)
                    {
                        path.Add(root);
                        Search(node);
                    }
                }

            }
        }

        public bool IsResult()
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

        public void GetPath()
        {
            foreach(var node in path)
            {
               Console.WriteLine(node);
            }
        }

        public bool PathHasNode(Node n)
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




    }
}
