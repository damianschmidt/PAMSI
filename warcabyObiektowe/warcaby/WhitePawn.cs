using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class WhitePawn
    {
        private int column;
        private int row;
        private FieldType[,] boardStatus;
        private FieldType[,] boardAfter;
        private int score = 0;
        private Node parent;
        private List<Node> listNode;

        #region Points 
        private int type = -1;
        private int move = -1;
        private int moveMid = -2;
        private int moveEdge = -3;
        private int hit = -20;
        private int hitMid = -25;
        private int hitEdge = -30;
        private int level4 = -10;
        private int level3 = -4;
        private int level2 = -2;
        private int level1 = 0;
        private int edge3 = -4;
        private int edge2 = -2;
        private int edge1 = 0;
        #endregion

        // Constructor
        public WhitePawn(int row, int column, FieldType[,] board, Node node = null)
        {
            this.column = column;
            this.row = row;
            this.boardStatus = board;
            this.parent = node;
            this.listNode = new List<Node>();
        }
        public bool PossibilityOfMoving()
        {
            if (Move() + Hit(boardStatus) != 0) { return true; }
            else { return false; }
        }

        private int CountScore()
        {
            score = PositionLevel() + PositionEdge() + type;
            return score;
        }

        private int Move()
        {
            int points = 0;

            if (column != 0 && column != 7 && row > 1)
            {
                if (boardStatus[row - 1, column - 1] == FieldType.Free && column > 3)
                {
                    points = moveMid;
                    
                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhitePawn;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
                else if (boardStatus[row - 1, column - 1] == FieldType.Free && column < 4)
                {
                    points = move;
                    
                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhitePawn;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

                if (boardStatus[row - 1, column + 1] == FieldType.Free && column > 3)
                {
                    points = move;
                    
                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhitePawn;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
                else if (boardStatus[row - 1, column + 1] == FieldType.Free && column < 4)
                {
                    points = moveMid;
                    
                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhitePawn;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }
            else if (column == 0 && row > 1)
            {
                if (boardStatus[row - 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;
                    
                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhitePawn;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }
            else if (column == 7 && row > 1)
            {
                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;
                    
                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhitePawn;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }
            else if (column != 0 && column != 7 && row == 1)
            {
                if (boardStatus[row - 1, column - 1] == FieldType.Free && column > 3)
                {
                    points = moveMid;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
                else if (boardStatus[row - 1, column - 1] == FieldType.Free && column < 4)
                {
                    points = move;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

                if (boardStatus[row - 1, column + 1] == FieldType.Free && column > 3)
                {
                    points = move;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
                else if (boardStatus[row - 1, column + 1] == FieldType.Free && column < 4)
                {
                    points = moveMid;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }
            else if (column == 0 && row == 1)
            {
                if (boardStatus[row - 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }
            else if (column == 7 && row == 1)
            {
                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }
            return points;
        }

        private int Hit(FieldType[,] board)
        {
            int points = 0;

            if (column > 1 && column < 4 && row > 2) // Check if hit haven't make you out of board 
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hit;
                        
                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhitePawn;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                else if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitMid;
                        
                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhitePawn;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (column > 3 && column < 6 && row > 2) // Check if hit haven't make you out of board 
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitMid;
                        
                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhitePawn;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                else if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hit;
                        
                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhitePawn;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (column < 2 && row > 2)
            {
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhitePawn;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (row > 2)
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;
                        
                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhitePawn;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (column > 1 && column < 4 && row == 2) // Check if hit haven't make you out of board 
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                else if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitMid;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (column > 3 && column < 6 && row == 2) // Check if hit haven't make you out of board 
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitMid;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                else if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (column < 2 && row == 2)
            {
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            else if (row == 2)
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = newBoard;

                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }
            return points;
        }

        private int PositionLevel()
        {
            if (row > 5)
            {
                return level1;
            }
            else if (row > 3)
            {
                return level2;
            }
            else if (row > 1)
            {
                return level3;
            }
            else
            {
                return level4;
            }
        }

        private int PositionEdge()
        {
            if (column == 0 || column == 7 || row == 0 || row == 7)
            {
                return edge3;
            }
            else if (column == 1 || column == 6 || row == 1 || row == 6)
            {
                return edge2;
            }
            else
            {
                return edge1;
            }
        }

        public List<Node> ReturnNode()
        {
            return listNode;
        }

        public FieldType[,] Clone(FieldType[,] board)
        {
            FieldType[,] copyBoard = new FieldType[8, 8];
            Array.Copy(board, copyBoard, board.Length);
            return copyBoard;
        }
    }
}
