using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI1
{

    public class DFS
    {

        //private List<Node> path = new List<Node>();

        public List<Node> visited = new List<Node>();

        public static int result = 3;

        public static int depthLimit = 21;

        public DFS()
        { }


        public List<Node> Search(Node root, List<Node> path)
        {
            root.MakeChildren();//generate possible states

            if (IsResult(root)) return path;

            visited.Add(root);//add to visited nodes

            foreach (var node in root._children)//for each state
            {
                if (!PathHasNode(node, path) && visited.Count <= depthLimit)//if path don't have this node
                {
                    path.Add(node);//add to path
                    return Search(node, path);//continue recursion with children node
                }
            }
            throw new Exception("Solution cannot be found because the limit is too small");

        }

        public bool IsResult(Node node)//check whether result is in the path or no
        {
            return node.Jar1 == result || node.Jar2 == result;
        }

        public void GetPath(List<Node> path)//print path
        {

            foreach (var node in path)
            {
                Console.WriteLine(node);
            }

        }

        public bool PathHasNode(Node n, List<Node> path)//check if a concrete node is present in the path
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
