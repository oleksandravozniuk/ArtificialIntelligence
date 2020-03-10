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

            dfs.Search(root);

            Console.WriteLine("Depth Search with limit");

            Console.WriteLine("Jar1 = 9   Jar2=5   Limit=20");

            Console.WriteLine("Path:");

            dfs.GetPath();

            Console.WriteLine();

            Console.WriteLine("All visited vertexes:");

            foreach (var n in dfs.visited)
                Console.WriteLine(n);


            Console.WriteLine();

            //dfs.printTree(root);
            
            
            Console.ReadLine();
        }


      

    }
}
