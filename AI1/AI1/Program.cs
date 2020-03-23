using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI1
{
    class Program
    {
        static void Main(string[] args)
        {

            Node root = new Node(1, 0, 0, null);

            DFS dfs = new DFS();

            List<Node> path = new List<Node>(); 
            
            
            Console.WriteLine("Depth Search with limit");

            Console.WriteLine("Jar1 = 9   Jar2 = 5   Limit= 20");

            try
            {
               path = dfs.Search(root, new List<Node>());
               Console.WriteLine("Path:");
               dfs.GetPath(path);   
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadLine();
        }


      

    }
}
