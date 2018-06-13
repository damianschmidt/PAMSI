using System;
using System.Collections.Generic;

namespace warcaby
{
    class WhiteQueen
    {
        private int column;
        private int row;
        private FieldType[,] boardStatus;
        private FieldType[,] boardAfter;
        private int score = 0;
        private Node parent;
        private List<Node> listNode;

        #region Points 
        private int type = -5;
        private int move = -2;
        private int moveEdge = -3;
        private int hit = -30;
        private int hitEdge = -40;
        private int hitMulti = -40;
        private int edge3 = -6;
        private int edge2 = -4;
        private int edge1 = 0;
        #endregion

        // Constructor
        public WhiteQueen(int row, int column, FieldType[,] board, Node node = null)
        {
            this.column = column;
            this.row = row;
            boardStatus = board;
            parent = node;
            listNode = new List<Node>();
        }

        public bool PossibilityOfMoving()
        {
            if (Move() + Hit(boardStatus, row, column) != 0) { return true; }
            else { return false; }
        }

        private int CountScore()
        {
            score = PositionEdge() + type;
            return score;
        }

        private int Move()
        {
            int points = 0;

            if (column != 0 && column != 7 && row != 0 && row != 7)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

                if (boardStatus[row - 1, column + 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            } // Center
            else if (column == 0 && row == 0)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }// Left-Top Corner
            else if (column == 7 && row == 7)
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
            } // Right-Bottom Corner
            else if (column == 0)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

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
            }// Left
            else if (column == 7)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

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
            }// Right
            else if (row == 0)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = Clone(boardStatus);
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    Node newNode = new Node(parent, newBoard, newScore); // Make new node
                    listNode.Add(newNode); // Add to list of possible moves
                }
            }// Top
            else
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
            }// Bottom
            return points;
        }

        private int Hit(FieldType[,] board, int row, int column)
        {
            int points = 0;

            if (column > 1 && column < 6 && row < 6 && row > 1) // Check if hit haven't make you out of board 
            {
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Center
            else if (column < 2 && row < 2) // Check if hit haven't make you out of board 
            {
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Left-Top Corner 
            else if (column > 5 && row < 2)
            {
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Right-Top Corner
            else if (row < 2)
            {
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }// Top
            else if (column < 2 && row > 5)
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
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Bottom-Left Corner
            else if (column > 5 && row > 5)
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
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Bottom-Right Corner
            else if (row > 5)
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
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Bottom
            else if (column < 2)
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
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column + 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            } // Leftside
            else if (column > 5)
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
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row - 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = Clone(board);
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;
                        boardAfter = Clone(newBoard);

                        // Multicapturing
                        if (Hit(Clone(boardAfter), row + 2, column - 2) != 0)
                        {
                            points = points + hitMulti;
                        }

                        int newScore = points + CountScore();
                        Node newNode = new Node(parent, boardAfter, newScore); // Make new node
                        listNode.Add(newNode); // Add to list of possible moves
                    }
                }
            }// Rightside
            return points;
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
