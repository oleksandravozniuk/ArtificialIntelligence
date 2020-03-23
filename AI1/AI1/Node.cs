using System.Collections.Generic;

namespace AI1
{
    public class Node
    {
        public int Level { get; set; }
        //-----------------------------------
        public int Jar1 { get; set; }
        public int Jar2 { get; set; }
        //----------------------------------------------
        public static int limitFirstJar = 9;

        public static int limitSecondJar = 5;
        //----------------------------------------------------
        public static int levelLimit = 21;



        public Node ParentNode { get; set; }

        public List<Node> _children = new List<Node>();

        public Node(int level, int jar1, int jar2, Node parent)
        {
            Level = level;
            Jar1 = jar1;
            Jar2 = jar2;
            ParentNode = parent;
            //MakeChildren();
        }

        public void MakeChildren()
        {
            if (Level <= levelLimit)//tree has 20 or less levels
            {


                    if (Jar2 == 0 && Jar1 != limitFirstJar)//full jar2
                    {
                    _children.Add(new Node(this.Level + 1, Jar1, limitSecondJar, this));
                    }

                    if (Jar1 == 0 && Jar2 != limitSecondJar)//full jar1
                    {
                    _children.Add(new Node(this.Level + 1, limitFirstJar, Jar2, this));
                    }

                    if (Jar1 != limitFirstJar && Jar2 != 0)//from jar2 to jar1
                    {
                        if (Jar2 >= limitFirstJar - Jar1)//jar2 has more water than jar1 can contain
                        {
                            _children.Add(new Node(this.Level + 1, limitFirstJar, Jar2 - (limitFirstJar - Jar1), this));
                        }
                        else
                        {
                            _children.Add(new Node(this.Level + 1, Jar2 + Jar1, 0, this));
                        }
                    }

                    if (Jar2 != limitSecondJar && Jar1 != 0)//from jar1 to jar2
                    {
                        if (Jar1 >= limitSecondJar - Jar2)//jar1 has more water than jar1 can contain
                        {
                            _children.Add(new Node(this.Level + 1, Jar1 - (limitSecondJar - Jar2), limitSecondJar, this));
                        }
                        else
                        {
                            _children.Add(new Node(this.Level + 1, 0, Jar1 + Jar2, this));
                        }
                    }


                    if (Jar1 != 0 && Jar2 != 0)//empty jar1
                    {
                        _children.Add(new Node(this.Level + 1, 0, Jar2, this));
                    }

                    if (Jar1 != 0 && Jar2 != 0)//empty jar2
                    {
                        _children.Add(new Node(this.Level + 1, Jar1, 0, this));
                    }

            }


        }



        public override string ToString()
        {

            string node = "Jar1: " + Jar1.ToString() + " " + "Jar2: " + Jar2.ToString() + " Level: " + Level + "\n";
            
            if (ParentNode != null)
            {
                string parentNode = " Parent: " + ParentNode.Jar1 + " " + ParentNode.Jar2 + " Level: " + ParentNode.Level + "\n";
                return node + parentNode;
            }
            else
                return node;
        }
    }
}
