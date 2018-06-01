using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class WhiteQueen
    {
        private int column;
        private int row;
        private FieldType[,] boardStatus;
        private FieldType[,] boardAfter;
        private int score = 0;

        #region Points 
        private int type = 10;
        private int move = 2;
        private int moveEdge = 3;
        private int hit = 10;
        private int hitEdge = 20;
        private int level4 = 10;
        private int level3 = 4;
        private int level2 = 2;
        private int level1 = 0;
        private int edge3 = 4;
        private int edge2 = 2;
        private int edge1 = 0;
        #endregion

        // Constructor
        public WhiteQueen(int column, int row, FieldType[,] board)
        {
            this.column = column;
            this.row = row;
            this.boardStatus = board;
        }

        public bool PossibilityOfMoving()
        {
            if (Move() + Hit(boardStatus) != 0) { return true; }
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

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row - 1, column + 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = move;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            } // Center
            else if (column == 0 && row == 0)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            }// Left-Top Corner
            else if (column == 7 && row == 7)
            {
                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            } // Right-Bottom Corner
            else if (column == 0)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row - 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            }// Left
            else if (column == 7)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            }// Right
            else if (row == 0)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            }// Top
            else
            {
                if (boardStatus[row - 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column - 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }

                if (boardStatus[row - 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row - 1, column + 1] = FieldType.WhiteQueen;

                    int newScore = points + CountScore();
                    FieldType[,] treeBoard = BoardTo8x4(newBoard);
                }
            }// Bottom
            return points;
        }

        private int Hit(FieldType[,] board)
        {
            int points = 0;

            if (column > 1 && column < 6 && row < 6 && row > 1) // Check if hit haven't make you out of board 
            {
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
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

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                else if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.BlackPawn;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
            } //Left-Top Corner 
            else if (column > 5 && row < 2)
            {
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
            } //Right-Top Corner
            else if (row < 2)
            {
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
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

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
            }// Bottom-Left Corner
            else if (column > 5 && row > 5)
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
            }// Bottom-Right Corner
            else if (row > 5)
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
            }// Bottom
            else if (column < 2)
            {
                if (board[row - 1, column + 1] == FieldType.BlackPawn || board[row - 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column + 1] = FieldType.Free;
                        newBoard[row - 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row + 1, column + 1] == FieldType.BlackPawn || board[row + 1, column + 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
            }// Leftside
            else if (column > 5)
            {
                if (board[row - 1, column - 1] == FieldType.BlackPawn || board[row - 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row - 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row - 1, column - 1] = FieldType.Free;
                        newBoard[row - 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
                    }
                }
                if (board[row + 1, column - 1] == FieldType.BlackPawn || board[row + 1, column - 1] == FieldType.BlackQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.WhiteQueen;

                        int newScore = points + CountScore();
                        FieldType[,] treeBoard = BoardTo8x4(newBoard);
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

        private FieldType[,] BoardTo8x4(FieldType[,] board)
        {
            FieldType[,] newBoard = new FieldType[8, 4];

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (row % 2 == 0)
                    {
                        newBoard[i, j] = board[i, 2 * j];
                    }
                    else
                    {
                        newBoard[i, j] = board[i, (2 * j) + 1];
                    }
                }
            }
            return newBoard;
        }
    }
}
