using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class BlackPawn
    {
        private int column;
        private int row;
        private FieldType[,] boardStatus;

        #region Points 
        private int move = 1;
        private int moveMid = 2;
        private int moveEdge = 3;
        private int hit = 10;
        private int hitMid = 15;
        private int level4 = 10;
        private int level3 = 4;
        private int level2 = 2;
        private int level1 = 0;
        private int edge3 = 4;
        private int edge2 = 2;
        private int edge1 = 0;
        #endregion

        // Constructor
        public BlackPawn(int column, int row, FieldType[,] board)
        {
            this.column = column;
            this.row = row;

            FieldType[,] newBoard = new FieldType[8, 8];
            #region Overwrite boardStatus
            for(var i = 0; i < 8; i++)
            {
                for(var j = 0; j<4; j++)
                {
                    if (i % 2 == 0)
                    {
                        newBoard[i, 2 * j] = board[i, j];
                    }
                    else
                    {
                        newBoard[i, (2 * j) + 1] = board[i, j];
                    }
                }
            }
            #endregion

            this.boardStatus = newBoard;
        }

        public bool PossibilityOfMoving()
        {
            if(Move() + Hit() != 0) { return true; }
            else { return false; }
        }

        private int Move()
        {
            int points = 0;

            if(column != 0 && column != 7 && row != 7)
            {
                if(boardStatus[row + 1, column - 1] == FieldType.Free && column > 3)
                {
                    points = moveMid;
                }
                else if (boardStatus[row + 1, column - 1] == FieldType.Free && column < 3)
                {
                    points = move;
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free && column > 3)
                {
                    points = move;
                }
                else if (boardStatus[row + 1, column + 1] == FieldType.Free && column < 3)
                {
                    points = moveMid;
                }
            }
            else if(column == 0 && row != 7)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;
                }
            }
            else if(column == 0 && row != 7)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;
                }
            }

            return points;
        }

        private int Hit()
        {
            int points = 0;

            if(column > 1 && column < 6 && row < 6) // Check if hit haven't make you out of board 
            {
               if(boardStatus[row + 1, column - 1] == FieldType.WhitePawn || boardStatus[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (boardStatus[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hit;
                    }
                }
                if (boardStatus[row + 1, column + 1] == FieldType.WhitePawn || boardStatus[row + 1, column + 1] == FieldType.WhiteQueen)
                {
                    if (boardStatus[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitMid;
                    }
                }
            }
            return points;
        }

        private int PositionLevel()
        {
            if (row > 5)
            {
                return level4;
            }
            else if(row > 3)
            {
                return level3;
            }
            else if(row > 1)
            {
                return level2;
            }
            else
            {
                return level1;
            }
        }

        private int PositionEdge()
        {
            if(column == 0 || column == 7 || row == 0 || row == 7)
            {
                return edge3;
            }
            else if(column == 1 || column == 6 || row == 1 || row == 6)
            {
                return edge2;
            }
            else
            {
                return edge1;
            }
        }
    }
}
