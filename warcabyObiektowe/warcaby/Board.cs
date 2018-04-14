using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace warcaby
{
    class Board
    {
        private FieldType[,] boardStatus;
        private bool selectedPawn;
        private bool selectedQueen;
        private int selCol = 0;
        private int selRow = 0;
        private int playerScore, computerScore;
        private Button[,] buttonName;
        private MainWindow mainWindow;

        public Board(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitButtonName();
        }

        private void InitButtonName()
        {
            buttonName = new Button[,] { { this.mainWindow.B1, this.mainWindow.B2, this.mainWindow.B3, this.mainWindow.B4 }, { this.mainWindow.B5, this.mainWindow.B6, this.mainWindow.B7, this.mainWindow.B8 }, { this.mainWindow.B9, this.mainWindow.B10, this.mainWindow.B11, this.mainWindow.B12 }, { this.mainWindow.B13, this.mainWindow.B14, this.mainWindow.B15, this.mainWindow.B16 }, { this.mainWindow.B17, this.mainWindow.B18, this.mainWindow.B19, this.mainWindow.B20 }, { this.mainWindow.B21, this.mainWindow.B22, this.mainWindow.B23, this.mainWindow.B24 }, { this.mainWindow.B25, this.mainWindow.B26, this.mainWindow.B27, this.mainWindow.B28 }, { this.mainWindow.B29, this.mainWindow.B30, this.mainWindow.B31, this.mainWindow.B32 } };
        }

        public void InitBoard()
        {
            //Reset score counter
            playerScore = 0;
            computerScore = 0;

            //Create a new blank array of free cells
            boardStatus = new FieldType[8, 4];

            //Init default value of fields on board
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    boardStatus[i, j] = FieldType.Free;
                    boardStatus[(7 - i), j] = FieldType.WhitePawn;
                    if (i < 2)
                    {
                        boardStatus[(i + 3), j] = FieldType.Free;
                    }
                    boardStatus[6, j] = FieldType.Free;
                    boardStatus[5, j] = FieldType.BlackPawn;
                    boardStatus[4, j] = FieldType.Free;
                    boardStatus[3, j] = FieldType.BlackPawn;
                }
            }

            #region Iterate every button on the grid
            int k = 1;
            this.mainWindow.FieldsGrid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                if (k < 13)
                {
                    LoadPicture(".\\img\\jpg\\checker-black.jpg", button);
                    k++;
                }
                else if (k < 21)
                {
                    LoadPicture(".\\img\\jpg\\field-dark.jpg", button);
                    k++;
                }
                else
                {
                    LoadPicture(".\\img\\jpg\\checker-white.jpg", button);
                }
            });
            #endregion

            //Reset selected button property
            selectedPawn = false;
            selectedQueen = false;
        }

        public void Select(Button button, int row, int column)
        {
            SelectWhitePawn(button, row, column);
            SelectWhiteQueen(button, row, column);
        }
        #region Partial funcions of Select
        private void SelectWhitePawn(Button button, int row, int column)
        {
            if ((boardStatus[row, column] == FieldType.WhitePawn) && (selectedPawn == false))
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\checker-selected.jpg", button);
                if (boardStatus[selRow, selCol] == FieldType.SelectedQueen)
                {
                    LoadPicture(".\\img\\jpg\\queen-white.jpg", buttonName[selRow, selCol]);
                    boardStatus[selRow, selCol] = FieldType.WhiteQueen;
                    selectedQueen = false;
                }

                selRow = row;
                selCol = column;
                selectedPawn = true;
                boardStatus[row, column] = FieldType.SelectedPawn;
                CheckPossibleOfPawnMoves(row, column);
            }
            else if (boardStatus[row, column] == FieldType.WhitePawn)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\checker-selected.jpg", button);
                boardStatus[row, column] = FieldType.SelectedPawn;

                LoadPicture(".\\img\\jpg\\checker-white.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.WhitePawn;

                selRow = row;
                selCol = column;
                CheckPossibleOfPawnMoves(row, column);
            }
            else if (boardStatus[row, column] != FieldType.WhitePawn && boardStatus[row, column] != FieldType.WhiteQueen && boardStatus[row, column] != FieldType.Move && boardStatus[row, column] != FieldType.HitMove && selectedPawn == true)
            {
                RemovePossibleOfMoves();
                boardStatus[selRow, selCol] = FieldType.WhitePawn;
                LoadPicture(".\\img\\jpg\\checker-white.jpg", buttonName[selRow, selCol]);
                selectedPawn = false;
            }
        }
        private void SelectWhiteQueen(Button button, int row, int column)
        {
            if ((boardStatus[row, column] == FieldType.WhiteQueen) && (selectedQueen == false))
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\queen-selected.jpg", button);
                if (boardStatus[selRow, selCol] == FieldType.SelectedPawn)
                {
                    LoadPicture(".\\img\\jpg\\checker-white.jpg", buttonName[selRow, selCol]);
                    boardStatus[selRow, selCol] = FieldType.WhitePawn;
                    selectedPawn = false;
                }

                selRow = row;
                selCol = column;
                selectedQueen = true;
                boardStatus[row, column] = FieldType.SelectedQueen;
                CheckPossibleOfQueenMoves(row, column);
            }
            else if (boardStatus[row, column] == FieldType.WhiteQueen)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\queen-selected.jpg", button);
                boardStatus[row, column] = FieldType.SelectedQueen;

                LoadPicture(".\\img\\jpg\\queen-white.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.WhiteQueen;

                selRow = row;
                selCol = column;
                CheckPossibleOfQueenMoves(row, column);
            }
            else if(boardStatus[row, column] != FieldType.WhitePawn && boardStatus[row, column] != FieldType.WhiteQueen && boardStatus[row, column] != FieldType.Move && boardStatus[row, column] != FieldType.HitMove && selectedQueen == true)
            {
                RemovePossibleOfMoves();
                boardStatus[selRow, selCol] = FieldType.WhiteQueen;
                LoadPicture(".\\img\\jpg\\queen-white.jpg", buttonName[selRow, selCol]);
                selectedQueen = false;
            }
        }
        private void CheckPossibleOfPawnMoves(int row, int column)
        {
            bool top = false, bottom = false, leftside = false, rightside = false, even = false, odd = false;
            string move = ".\\img\\jpg\\move.jpg";
            #region checking position
            if (row == 0) top = true;
            if (row == 7) bottom = true;
            if (column == 0) leftside = true;
            if (column == 3) rightside = true;
            if (row == 0 || row == 2 || row == 4 || row == 6) odd = true;
            if (row == 1 || row == 3 || row == 5 || row == 7) even = true;
            #endregion

            if (selectedPawn == true && top != true)
            {
                if ((bottom == true) && (rightside == true))
                {

                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                }
                else if (bottom == true && leftside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                }
                else if (bottom == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                }
                else if ((leftside == true) && (even == true))
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                }
                else if (leftside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                }
                else if ((rightside == true) && (odd == true))
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column - 1)]);
                    }
                }
                else if (rightside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                }
                else if (odd == true)
                {
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column - 1)]);
                    }
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }       
                }
                else if (even == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                }
            }
        }
        private void CheckPossibleOfQueenMoves(int row, int column)
        {
            bool top = false, bottom = false, leftside = false, rightside = false, even = false, odd = false;
            string move = ".\\img\\jpg\\move.jpg";

            #region checking position
            if (row == 0) top = true;
            if (row == 7) bottom = true;
            if (column == 0) leftside = true;
            if (column == 3) rightside = true;
            if (row == 0 || row == 2 || row == 4 || row == 6) odd = true;
            if (row == 1 || row == 3 || row == 5 || row == 7) even = true;
            #endregion

            if (selectedQueen == true)
            {
                if ((top == true) && (leftside == true))
                {
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                }
                else if (top == true)
                {
                    if (boardStatus[(row + 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column - 1)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                }
                else if ((bottom == true) && (rightside == true))
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                }
                else if (bottom == true && leftside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                }
                else if (bottom == true)
                {  
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                }
                else if ((leftside == true) && (even == true))
                {           
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                    if (boardStatus[(row + 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column + 1)]);
                    }
                }
                else if (leftside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                }
                else if ((rightside == true) && (odd == true))
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column - 1)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                    if (boardStatus[(row + 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column - 1)]);
                    }
                }
                else if (rightside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                }
                else if (odd == true)
                {
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column - 1)]);
                    }
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row + 1), (column - 1)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column - 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column - 1)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                }
                else if (even == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column)]);
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row - 1), (column + 1)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column)]);
                    }
                    if (boardStatus[(row + 1), (column + 1)] == FieldType.Free)
                    {
                        boardStatus[(row + 1), (column + 1)] = FieldType.Move;
                        LoadPicture(move, buttonName[(row + 1), (column + 1)]);
                    }
                }
            }
        }
        #endregion

        public void Move(Button button, int row, int column)
        {
            MoveWhitePawn(button, row, column);
            MoveWhiteQueen(button, row, column);
            ChangePawnToQueen();
        }
        #region Partial funcions of Move
        private void MoveWhitePawn(Button button, int row, int column)
        {
            if ((boardStatus[row, column] == FieldType.Move || boardStatus[row, column] == FieldType.HitMove) && selectedPawn == true)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\checker-white.jpg", button);
                boardStatus[row, column] = FieldType.WhitePawn;

                LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.Free;

                CheckPossibleOfPawnHit(row, column);
                if(CheckPosibilityOfMultiPawnHit(row, column) != true) selectedPawn = false;
            }
        }
        private void MoveWhiteQueen(Button button, int row, int column)
        {
            if ((boardStatus[row, column] == FieldType.Move || boardStatus[row, column] == FieldType.HitMove) && selectedQueen == true)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\queen-white.jpg", button);
                boardStatus[row, column] = FieldType.WhiteQueen;

                LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.Free;

                selectedQueen = false;
            }
        }
        private void ChangePawnToQueen()
        {
            for(var i = 0; i < 4; i++)
            {
                if (boardStatus[0, i] == FieldType.WhitePawn)
                {
                    boardStatus[0, i] = FieldType.WhiteQueen;
                    LoadPicture(".\\img\\jpg\\queen-white.jpg", buttonName[0, i]);
                }
                if (boardStatus[7, i] == FieldType.BlackPawn)
                {
                    boardStatus[7, i] = FieldType.BlackQueen;
                    LoadPicture(".\\img\\jpg\\queen-black.jpg", buttonName[7, i]);
                }
            }
        }
        #endregion

        public void Hit(int row, int column)
        {
            CheckPossibleOfPawnHit(row, column);
            HitWhitePawn(row, column);
            CheckPossibleOfQueenHit(row, column);
            HitWhiteQueen(row, column);
        }
        #region Partial funcions of Hit
        private void CheckPossibleOfPawnHit(int row, int column)
        {
            bool top = false, bottom = false, leftside = false, rightside = false, even = false, odd = false, almostTop = false;
            string move = ".\\img\\jpg\\move.jpg";

            #region checking position
            if (row == 0) top = true;
            if (row == 1) almostTop = true;
            if (row == 7) bottom = true;
            if (column == 0) leftside = true;
            if (column == 3) rightside = true;
            if (row == 0 || row == 2 || row == 4 || row == 6) odd = true;
            if (row == 1 || row == 3 || row == 5 || row == 7) even = true;
            #endregion

            if (selectedPawn == true && top != true)
            {
                if ((bottom == true) && (rightside == true))
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                }
                else if (bottom == true && leftside == true)
                {
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                }
                else if (bottom == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                }
                else if ((leftside == true) && (even == true))
                {
                    if ((boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen))
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                            }
                        }
                    }    
                }
                else if (leftside == true)
                {  
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                }
                else if ((rightside == true) && (odd == true))
                {
                    if (almostTop != true )
                    {
                        if (boardStatus[(row - 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column - 1)] == FieldType.BlackQueen)
                        {
                            if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                            }
                        }
                    }
                }
                else if (rightside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {

                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }

                    }
                }
                else if (odd == true)
                {
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column - 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                }
                else if (even == true)
                {  
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                            }
                        }
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                            }
                        }
                    }
                }
            }
        }
        private void CheckPossibleOfQueenHit(int row, int column)
        {
            bool top = false, bottom = false, leftside = false, rightside = false, even = false, odd = false, almostTop = false, almostBottom = false;
            string move = ".\\img\\jpg\\move.jpg";

            #region checking position
            if (row == 0) top = true;
            if (row == 1) almostTop = true;
            if (row == 7) bottom = true;
            if (row == 6) almostBottom = true;
            if (column == 0) leftside = true;
            if (column == 3) rightside = true;
            if (row == 0 || row == 2 || row == 4 || row == 6) odd = true;
            if (row == 1 || row == 3 || row == 5 || row == 7) even = true;
            #endregion

            if (selectedQueen == true)
            {
                if ((top == true) && (leftside == true))
                {
                        if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                        {
                            boardStatus[(row + 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row + 2), (column + 1)]);
                        }
                }
                else if (top == true)
                {
                    if (boardStatus[(row + 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row + 1), (column - 1)] == FieldType.BlackQueen)
                    {
                        boardStatus[(row + 2), (column - 1)] = FieldType.HitMove;
                        LoadPicture(move, buttonName[(row + 2), (column - 1)]);
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                    {
                        boardStatus[(row + 2), (column + 1)] = FieldType.HitMove;
                        LoadPicture(move, buttonName[(row + 2), (column + 1)]);
                    }
                }
                else if((bottom == true) && (rightside == true))
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                }
                else if (bottom == true && leftside == true)
                {
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                }
                else if (bottom == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                }
                else if ((leftside == true) && (even == true))
                {
                    if ((boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen))
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                            }
                        }
                    }
                    if ((boardStatus[(row + 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row + 1), (column + 1)] == FieldType.BlackQueen))
                    {
                        if (boardStatus[(row + 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row + 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row + 2), (column + 1)]);
                        }
                    }
                }
                else if (leftside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                    if (almostBottom != true)
                    {
                        if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                        {
                            if (boardStatus[(row + 2), (column + 1)] == FieldType.Free)
                            {
                                boardStatus[(row + 2), (column + 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row + 2), (column + 1)]);
                            }
                        }
                    }
                }
                else if ((rightside == true) && (odd == true))
                {
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column - 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                    if (almostBottom != true)
                    {
                        if (boardStatus[(row + 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row + 1), (column - 1)] == FieldType.BlackQueen)
                        {
                            if (boardStatus[(row + 2), (column - 1)] == FieldType.Free)
                            {
                                boardStatus[(row + 2), (column - 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row + 2), (column - 1)]);
                            }
                        }
                    }
                }
                else if (rightside == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                            }
                        }
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                    { 
                        if (boardStatus[(row + 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row + 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row + 2), (column - 1)]);
                        }
                    }
                }
                else if (odd == true)
                {
                    if (boardStatus[(row - 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column - 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                        }
                    }
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                        }
                    }
                    if (almostBottom != true)
                    {
                        if (boardStatus[(row + 1), (column - 1)] == FieldType.BlackPawn || boardStatus[(row + 1), (column - 1)] == FieldType.BlackQueen)
                        {
                            if (boardStatus[(row + 2), (column - 1)] == FieldType.Free)
                            {
                                boardStatus[(row + 2), (column - 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row + 2), (column - 1)]);
                            }
                        }
                        if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                        {
                            if (boardStatus[(row + 2), (column + 1)] == FieldType.Free)
                            {
                                boardStatus[(row + 2), (column + 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row + 2), (column + 1)]);
                            }
                        }
                    }
                }
                else if (even == true)
                {
                    if (boardStatus[(row - 1), (column)] == FieldType.BlackPawn || boardStatus[(row - 1), (column)] == FieldType.BlackQueen)
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column - 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column - 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column - 1)]);
                            }
                        }
                    }
                    if (boardStatus[(row - 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row - 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (almostTop != true)
                        {
                            if (boardStatus[(row - 2), (column + 1)] == FieldType.Free)
                            {
                                boardStatus[(row - 2), (column + 1)] = FieldType.HitMove;
                                LoadPicture(move, buttonName[(row - 2), (column + 1)]);
                            }
                        }
                    }
                    if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row + 2), (column - 1)] == FieldType.Free)
                        {
                            boardStatus[(row + 2), (column - 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row + 2), (column - 1)]);
                        }
                    }
                    if (boardStatus[(row + 1), (column + 1)] == FieldType.BlackPawn || boardStatus[(row + 1), (column + 1)] == FieldType.BlackQueen)
                    {
                        if (boardStatus[(row + 2), (column + 1)] == FieldType.Free)
                        {
                            boardStatus[(row + 2), (column + 1)] = FieldType.HitMove;
                            LoadPicture(move, buttonName[(row + 2), (column + 1)]);
                        }
                    }
                }
            }
        }
        private void HitWhitePawn(int row, int column)
        {
            if(boardStatus[row, column] == FieldType.HitMove && selectedPawn == true)
            {
                string free = ".\\img\\jpg\\field-dark.jpg";

                if (selRow % 2 == 1)
                {
                    if ((selCol - column) < 0)
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
                        playerScore++;
                    }
                    else
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                        playerScore++;
                    }
                }
                else
                {
                    if ((selCol - column) < 0)
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                        playerScore++;
                    }
                    else
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
                        playerScore++;
                    }
                }
            }
        }
        private void HitWhiteQueen(int row, int column)
        {
            if (boardStatus[row, column] == FieldType.HitMove && selectedQueen == true)
            {
                string free = ".\\img\\jpg\\field-dark.jpg";

                if (selRow % 2 == 1)
                {
                    if ((selCol - column) < 0 && (selRow - row) < 0) //Capturing right-down
                    {
                        LoadPicture(free, buttonName[(row - 1), column]);
                        boardStatus[(row - 1), column] = FieldType.Free;
                        playerScore++;
                    }
                    else if ((selCol - column) < 0 && (selRow - row) > 0) //Capturing right-up
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
                        playerScore++;
                    }
                    else if ((selCol - column) > 0 && (selRow - row) > 0) //Capturing left-up
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                        playerScore++;
                    }
                    else //Capturing left-down
                    {
                        LoadPicture(free, buttonName[(row - 1), selCol]);
                        boardStatus[(row - 1), selCol] = FieldType.Free;
                        playerScore++;
                    }
                }
                else
                {
                    if ((selCol - column) < 0 && (selRow - row) < 0) //Capturing right-down
                    {
                        LoadPicture(free, buttonName[(row - 1), selCol]);
                        boardStatus[(row - 1), selCol] = FieldType.Free;
                        playerScore++;
                    }
                    else if((selCol - column) < 0 && (selRow - row) > 0) //Capturing right-up
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                        playerScore++;
                    }
                    else if((selCol - column) > 0 && (selRow - row) > 0) //Capturing left-up
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
                        playerScore++;
                    }
                    else //Capturing left-down
                    {
                        LoadPicture(free, buttonName[(row - 1), column]);
                        boardStatus[(row - 1), column] = FieldType.Free;
                        playerScore++;
                    }
                }
            }
        }
        #endregion

        public int PlayerScore()
        {
            return playerScore;
        }
        public int ComputerScore()
        {
            return computerScore;
        }

        private void RemovePossibleOfMoves()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (boardStatus[i, j] == FieldType.Move || boardStatus[i, j] == FieldType.HitMove)
                    {
                        boardStatus[i, j] = FieldType.Free;
                        LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[i, j]);
                    }
                }
            }
        }

        private bool CheckPosibilityOfMultiPawnHit(int row, int column)
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (boardStatus[i, j] == FieldType.HitMove)
                    {
                        selRow = row;
                        selCol = column;
                        LoadPicture(".\\img\\jpg\\checker-selected.jpg", buttonName[row, column]);
                        return true;
                    }
                }
            }
            return false;
        }

        private void LoadPicture(string source, Button button)
        {
            Uri resourceUri = new Uri(source, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            var brush = new ImageBrush();
            brush.ImageSource = temp;
            button.Background = brush;
        }
    }
}
