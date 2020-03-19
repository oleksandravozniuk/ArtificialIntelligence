using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI2
{
    class State
    {
        public int[,] ValueMatrix { get; private set; }

        public int[,] ResultMatrix { get; private set; }

        public State parent { get; private set; }

        public List<State> Children = new List<State>();

        public int H { get; private set; }//the number of cells that are not on their right positions


        public State(int[,] valueMatrix, int[,] resultMatrix, State parent)
        {
            this.ValueMatrix = Copy(valueMatrix);
            this.ResultMatrix = Copy(resultMatrix);
            this.parent = parent;
            this.H = SearchWrongPosition();
        }

        public void GenerateChildren()
        {
            var emptyCell = (x: SearchForEmptyCell().Item1, y: SearchForEmptyCell().Item2);

            for(var i=0;i<3;i++)
            {
                for(var j=0;j<3;j++)
                {
                    if(i==emptyCell.x && j==emptyCell.y)
                    {
                        if(i-1>=0)//move an empty cell up
                        {

                        ChangeCells((i - 1, j), emptyCell);
                        
                        }

                        if(j+1 <=2)//move an empty cell right
                        {
                            ChangeCells((i, j + 1), emptyCell);

                        }
                        
                        if(i+1<=2)//move an empty cell down
                        {
                            ChangeCells((i + 1, j), emptyCell);

                        }

                        if(j-1>=0)//move an empty cell left
                        {
                            ChangeCells((i, j - 1), emptyCell);

                        }
                    }
                    
                }
            }

            Children.OrderBy(i => H);//when all children are generated sort the list of children by ascending H
        }

        private (int,int) SearchForEmptyCell()//looks for an empty cell in the valueMatrix
        {
            var coordinates = (x:0, y:0);
            
            for(int i=0; i<3; i++)
            {
                for(int j=0;j<3;j++)
                {
                    if(ValueMatrix[i,j]==0)
                    {
                        coordinates.x = i;
                        coordinates.y = j;
                    }
                }
            }

            return coordinates;
        }

        private void ChangeCells((int,int) notEmpty, (int,int) empty)//Adds new children changing two cells: empty and not empty (with some value from 1 to 8)
        {
            int[,] newValueMatrix = CopyMatrix();

            int value = newValueMatrix[notEmpty.Item1, notEmpty.Item2];

            newValueMatrix[notEmpty.Item1, notEmpty.Item2] = 0;
            newValueMatrix[empty.Item1, empty.Item2] = value;

            if(parent==null || !CompareTwoMatrixes(newValueMatrix, this.parent.ValueMatrix))//if it is a root or a new state has'n been existed before as grandparent of this state
            {
                State newState = new State(newValueMatrix,ResultMatrix, this);

                Children.Add(newState);
            }
           
        }

        private int[,] CopyMatrix()//copies valueMatrix to the new matrix in order to make a new child
        {
            int[,] matrix = new int[3, 3];

            for(var i=0;i<3;i++)
            {
                for(var j=0;j<3;j++)
                {
                    matrix[i, j] = ValueMatrix[i, j];
                }
            }

            return matrix;
        }

        private int SearchWrongPosition()//how many cells have wrong positions
        {
            int numPositions = 0;//now we have 0 wrong positions

            for (var i = 0; i<3;i++)
            {
                for(var j=0;j<3;j++)
                {
                    if (ValueMatrix[i, j] != ResultMatrix[i, j])
                        numPositions++;
                }
            }

            

            return numPositions;
        }

        public static int[,] Copy(int[,] matrix)
        {
            int[,] m = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    m[i, j] = matrix[i, j];
                }
            }
            return m;
        }

        public bool CompareTwoMatrixes(int[,] matrix1, int[,] matrix2)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                        return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string str = String.Empty;

            str += "Number of cells on wrong positions: " + H +"\n";

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    str+=ValueMatrix[i, j] + " ";
                }
                str += "\n";
            }

            return str;
        }


    }
}
