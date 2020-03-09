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

            dfs.GetPath();

            Console.WriteLine();

            foreach (var n in dfs.visited)
                Console.WriteLine(n);


            Console.WriteLine();

            //dfs.printTree(root);
            
            
            Console.ReadLine();
        }


      

    }
}
