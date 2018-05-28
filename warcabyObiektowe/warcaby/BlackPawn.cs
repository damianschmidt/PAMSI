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
        private FieldType[,] boardAfter;
        private int score = 0;

        #region Points 
        private int type = 1;
        private int move = 1;
        private int moveMid = 2;
        private int moveEdge = 3;
        private int hit = 10;
        private int hitMid = 15;
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
            if(Move() + Hit(boardStatus) != 0) { return true; }
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

            if(column != 0 && column != 7 && row < 6)
            {
                if(boardStatus[row + 1, column - 1] == FieldType.Free && column > 3)
                {
                    points = moveMid;

                    // Tutaj tworzę galaz, ktora przyjmuje dwa argumenty: wynik + tablice stanow zmieniona, ale nie wiem jak , wiec tu to napraw xD
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.BlackPawn;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                    //Pozniej to najwyzej w jakas funckej zamkne zeby bylo mniej linijek kodu, ale na razie jest roboczo tak
                }
                else if (boardStatus[row + 1, column - 1] == FieldType.Free && column < 4)
                {
                    points = move;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.BlackPawn;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free && column > 3)
                {
                    points = move;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.BlackPawn;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
                else if (boardStatus[row + 1, column + 1] == FieldType.Free && column < 4)
                {
                    points = moveMid;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.BlackPawn;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
            }
            else if(column == 0 && row < 6)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.BlackPawn;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
            }
            else if(column == 7 && row < 6)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.BlackPawn;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
            }
            else if (column != 0 && column != 7 && row == 6)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free && column > 3)
                {
                    points = moveMid;

                    // Tutaj tworzę galaz, ktora przyjmuje dwa argumenty: wynik + tablice stanow zmieniona, ale nie wiem jak , wiec tu to napraw xD
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.BlackQueen;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                    //Pozniej to najwyzej w jakas funckej zamkne zeby bylo mniej linijek kodu, ale na razie jest roboczo tak
                }
                else if (boardStatus[row + 1, column - 1] == FieldType.Free && column < 4)
                {
                    points = move;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.BlackQueen;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }

                if (boardStatus[row + 1, column + 1] == FieldType.Free && column > 3)
                {
                    points = move;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.BlackQueen;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
                else if (boardStatus[row + 1, column + 1] == FieldType.Free && column < 4)
                {
                    points = moveMid;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.BlackQueen;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
            }
            else if (column == 0 && row == 6)
            {
                if (boardStatus[row + 1, column + 1] == FieldType.Free)
                {
                    points = moveEdge;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column + 1] = FieldType.BlackQueen;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
            }
            else if (column == 7 && row == 6)
            {
                if (boardStatus[row + 1, column - 1] == FieldType.Free)
                {
                    points = moveEdge;

                    // Jak wyzej
                    FieldType[,] newBoard = boardStatus;
                    newBoard[row, column] = FieldType.Free;
                    newBoard[row + 1, column - 1] = FieldType.BlackQueen;

                    int newScore = points + CountScore(); //tu masz punkty
                    FieldType[,] treeBoard = BoardTo8x4(newBoard); //a tutaj ta tablice
                }
            }
            return points;
        } //FILIP ZAJRZYJ TU!!!

        private int Hit(FieldType[,] board)
        {
            int points = 0;

            if(column > 1 && column < 4 && row < 5) // Check if hit haven't make you out of board 
            {
               if(board[row + 1, column - 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.BlackPawn;
                        boardAfter = newBoard;
                        
                        //Wielokrotne bicie sprawdzenie
                        if(Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }
                        
                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
                else if (board[row + 1, column + 1] == FieldType.WhitePawn || board[row + 1, column + 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitMid;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.BlackPawn;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if (column > 3 && column < 6 && row < 5) // Check if hit haven't make you out of board 
            {
                if (board[row + 1, column - 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitMid;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.BlackPawn;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
                else if (board[row + 1, column + 1] == FieldType.WhitePawn || board[row + 1, column + 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.BlackPawn;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if(column < 2 && row < 5)
            {
                if (board[row + 1, column + 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.BlackPawn;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if(row < 5)
            {
                if (board[row + 1, column - 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.BlackPawn;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if (column > 1 && column < 4 && row == 5) // Check if hit haven't make you out of board 
            {
                if (board[row + 1, column - 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hit;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.BlackQueen;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
                else if (board[row + 1, column + 1] == FieldType.WhitePawn || board[row + 1, column + 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitMid;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.BlackQueen;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if (column > 3 && column < 6 && row == 5) // Check if hit haven't make you out of board 
            {
                if (board[row + 1, column - 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitMid;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.BlackQueen;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
                else if (board[row + 1, column + 1] == FieldType.WhitePawn || board[row + 1, column + 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hit;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        newBoard[row + 2, column + 2] = FieldType.BlackQueen;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if (column < 2 && row == 5)
            {
                if (board[row + 1, column + 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column + 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column + 1] = FieldType.Free;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            else if (row == 5)
            {
                if (board[row + 1, column - 1] == FieldType.WhitePawn || board[row + 1, column - 1] == FieldType.WhiteQueen)
                {
                    if (board[row + 2, column - 2] == FieldType.Free)
                    {
                        points = hitEdge;

                        // Jak wyzej
                        FieldType[,] newBoard = board;
                        newBoard[row, column] = FieldType.Free;
                        newBoard[row + 1, column - 1] = FieldType.Free;
                        newBoard[row + 2, column - 2] = FieldType.BlackQueen;
                        boardAfter = newBoard;

                        //Wielokrotne bicie sprawdzenie
                        if (Hit(newBoard) != 0)
                        {
                            points = points + Hit(newBoard);
                        }

                        int newScore = points + CountScore(); //tu masz punkty
                        FieldType[,] treeBoard = BoardTo8x4(boardAfter); //a tutaj ta tablice
                    }
                }
            }
            return points;
        } //FILIP ZAJRZYJ TU!!!

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

        private FieldType[,] BoardTo8x4(FieldType[,] board)
        {
            FieldType[,] newBoard = new FieldType[8, 4];
            
            for(var i = 0; i < 8; i++)
            {
                for(var j = 0; j < 4; j++)
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
