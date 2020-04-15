using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var barrel = new Jar(12, 12);
            var jar1 = new Jar(5, 0);
            var jar2 = new Jar(7, 0);

            var startState = new State(barrel, jar1, jar2);

            var tabu = new Tabu();

            tabu.Search(startState);

            Console.ReadKey();
        }
    }
}
