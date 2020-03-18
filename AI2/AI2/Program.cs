using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] initialMatrix = new int[,] { { 2,8,3} ,{1,6,4}, {7,0,5 } };
            int[,] resultMatrix = new int[,] { {1,2,3 },{ 8,0,4},{7,6,5 } };

            State initialState = new State(initialMatrix,resultMatrix, null);

            RBFS rbfs = new RBFS(initialState);

            rbfs.Search(initialState);

            Console.WriteLine(rbfs.ToString());

            Console.ReadKey();

        }

        //public static void PrintState(State state)
        //{
        //    for(var i=0;i<3;i++)
        //    {
        //        for(var j=0;j<3;j++)
        //        {
        //            Console.Write(state.ValueMatrix[i,j] + " ");
        //        }
        //        Console.WriteLine();
        //    }

        //    Console.WriteLine();
        //}
    }
}
