using System;
using System.Collections.Generic;

namespace warcaby
{
    class Node
    {
        private Node parent;
        private Board currentBoard;
        private int score;

        public int parent { get; set; }
        public int currentBoard { get; set; }
        public int score { get; set; }

        public Node(Board currentBoard, int score)
        {
            this.parent = null;
            this.currentBoard = currentBoard;
            this.score = score;
        }

        public Node(Node parent, Board currentBoard, int score)
        {
            this.parent = parent;
            this.currentBoard = currentBoard;
            this.score = score;
        }
    }

    class Tree
    {
        public List<Node> Level1 = new List<Node>();
        public List<Node> Level2 = new List<Node>();
        public List<Node> Level3 = new List<Node>();

        #region Insert method's
        public void InsertL1(Board currentBoard, int score)
        {
            Node newNode = new Node();
            newNode.currentBoard = currentBoard;
            newNode.score = score;
            Level1.Add(newNode);
        }

        public void InsertL2(Node parent, Board currentBoard, int score)
        {
            Node newNode = new Node();
            newNode.parent = parent;
            newNode.currentBoard = currentBoard;
            newNode.score = score;
            Level2.Add(newNode);
        }

        public void InsertL3(Node parent, Board currentBoard, int score)
        {
            Node newNode = new Node();
            newNode.parent = parent;
            newNode.currentBoard = currentBoard;
            newNode.score = score;
            Level3.Add(newNode);
        }
        #endregion
    }
}