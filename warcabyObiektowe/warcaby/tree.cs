using System.Collections.Generic;

namespace warcaby
{
    class Node
    {
        public Node parent { get; set; }
        public FieldType[,] currentBoard { get; set; }
        public int score { get; set; }

 
        public Node(Node parent = null, FieldType[,] currentBoard = null, int score = 0)
        {
            this.parent = parent;
            this.currentBoard = currentBoard;
            this.score = score;
        }
    }

    class Tree
    {
        private List<Node> Level1 = new List<Node>();
        private List<Node> Level2 = new List<Node>();
        private List<Node> Level3 = new List<Node>();

        #region Insert methods
        public void InsertLevel1(Node node)
        {
            Level1.Add(node);
        }

        public void InsertLevel2(Node node)
        {
            Level2.Add(node);
        }

        public void InsertLevel3(Node node)
        {
            Level3.Add(node);
        }
        #endregion

        #region Make array methods
        public Node[] ToArrayLevel1()
        {
            return Level1.ToArray(); 
        }

        public Node[] ToArrayLevel2()
        {
            return Level2.ToArray();
        }

        public Node[] ToArrayLevel3()
        {
            return Level3.ToArray();
        }
        #endregion

        public List<Node> GetLevel1()
        {
            return Level1;
        }

        public List<Node> GetLevel2()
        {
            return Level2;
        }

        public List<Node> GetLevel3()
        {
            return Level3;
        }

    }
}
