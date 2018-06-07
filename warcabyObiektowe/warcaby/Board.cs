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
        public bool computerTurn = false;

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
                    boardStatus[i, j] = FieldType.BlackQueen;
                    boardStatus[(7 - i), j] = FieldType.Free;
                    if (i < 2)
                    {
                        boardStatus[(i + 3), j] = FieldType.Free;
                    }
                    boardStatus[3, j] = FieldType.WhitePawn;
                    boardStatus[5, j] = FieldType.WhitePawn;
                    boardStatus[7, j] = FieldType.WhitePawn;
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
            DrawBoard();
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
            if ((boardStatus[row, column] == FieldType.Move) && selectedPawn == true)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\checker-white.jpg", button);
                boardStatus[row, column] = FieldType.WhitePawn;

                LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.Free;

                selectedPawn = false;
                computerTurn = true;
            }
            else if ((boardStatus[row, column] == FieldType.HitMove) && selectedPawn == true)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\checker-white.jpg", button);
                boardStatus[row, column] = FieldType.WhitePawn;

                LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.Free;

                CheckPossibleOfPawnHit(row, column);
                if (CheckPossibleOfMultiPawnHit(row, column) != true) { selectedPawn = false; computerTurn = true; }
            }
        }
        private void MoveWhiteQueen(Button button, int row, int column)
        {
            if ((boardStatus[row, column] == FieldType.Move) && selectedQueen == true)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\queen-white.jpg", button);
                boardStatus[row, column] = FieldType.WhiteQueen;

                LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.Free;

                selectedQueen = false;
                computerTurn = true;
            }
            else if ((boardStatus[row, column] == FieldType.HitMove) && selectedQueen == true)
            {
                RemovePossibleOfMoves();
                LoadPicture(".\\img\\jpg\\queen-white.jpg", button);
                boardStatus[row, column] = FieldType.WhiteQueen;

                LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.Free;

                CheckPossibleOfQueenHit(row, column);
                if (CheckPossibleOfMultiQueenHit(row, column) != true) { selectedQueen = false; computerTurn = true; }
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
                    if (almostTop != true)
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
                else if ((top == true) && (rightside == true))
                {
                    if (boardStatus[(row + 1), (column)] == FieldType.BlackPawn || boardStatus[(row + 1), (column)] == FieldType.BlackQueen)
                    {
                        boardStatus[(row + 2), (column - 1)] = FieldType.HitMove;
                        LoadPicture(move, buttonName[(row + 2), (column - 1)]);
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
                    }
                    else
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                    }
                }
                else
                {
                    if ((selCol - column) < 0)
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                    }
                    else
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
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
                    }
                    else if ((selCol - column) < 0 && (selRow - row) > 0) //Capturing right-up
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
                    }
                    else if ((selCol - column) > 0 && (selRow - row) > 0) //Capturing left-up
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                    }
                    else //Capturing left-down
                    {
                        LoadPicture(free, buttonName[(row - 1), selCol]);
                        boardStatus[(row - 1), selCol] = FieldType.Free;
                    }
                }
                else
                {
                    if ((selCol - column) < 0 && (selRow - row) < 0) //Capturing right-down
                    {
                        LoadPicture(free, buttonName[(row - 1), selCol]);
                        boardStatus[(row - 1), selCol] = FieldType.Free;
                    }
                    else if((selCol - column) < 0 && (selRow - row) > 0) //Capturing right-up
                    {
                        LoadPicture(free, buttonName[(row + 1), selCol]);
                        boardStatus[(row + 1), selCol] = FieldType.Free;
                    }
                    else if((selCol - column) > 0 && (selRow - row) > 0) //Capturing left-up
                    {
                        LoadPicture(free, buttonName[(row + 1), column]);
                        boardStatus[(row + 1), column] = FieldType.Free;
                    }
                    else //Capturing left-down
                    {
                        LoadPicture(free, buttonName[(row - 1), column]);
                        boardStatus[(row - 1), column] = FieldType.Free;
                    }
                }
            }
        }
        #endregion

        public int PlayerScore()
        {
            CheckScore();
            return playerScore;
        }
        public int ComputerScore()
        {
            CheckScore();
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

        private bool CheckPossibleOfMultiPawnHit(int row, int column)
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

        private bool CheckPossibleOfMultiQueenHit(int row, int column)
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (boardStatus[i, j] == FieldType.HitMove)
                    {
                        selRow = row;
                        selCol = column;
                        LoadPicture(".\\img\\jpg\\queen-selected.jpg", buttonName[row, column]);
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

        private void DrawBoard()
        {
            for (var i = 0; i < 8; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    if(boardStatus[i,j] == FieldType.BlackPawn) { LoadPicture(".\\img\\jpg\\checker-black.jpg", buttonName[i, j]); }
                    else if (boardStatus[i, j] == FieldType.BlackQueen) { LoadPicture(".\\img\\jpg\\queen-black.jpg", buttonName[i, j]); }
                    else if (boardStatus[i, j] == FieldType.HitMove || boardStatus[i, j] == FieldType.Move) { LoadPicture(".\\img\\jpg\\move.jpg", buttonName[i, j]); }
                    else if (boardStatus[i, j] == FieldType.SelectedPawn) { LoadPicture(".\\img\\jpg\\checker-selected.jpg", buttonName[i, j]); }
                    else if (boardStatus[i, j] == FieldType.SelectedQueen) { LoadPicture(".\\img\\jpg\\queen-selected.jpg", buttonName[i, j]); }
                    else if (boardStatus[i, j] == FieldType.WhitePawn) { LoadPicture(".\\img\\jpg\\checker-white.jpg", buttonName[i, j]); }
                    else if (boardStatus[i, j] == FieldType.WhiteQueen) { LoadPicture(".\\img\\jpg\\queen-white.jpg", buttonName[i, j]); }
                    else { LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[i, j]); }
                }
            }
        }

        private void CheckScore()
        {
            int whitePoint = 12;
            int blackPoint = 12;

            for(var i = 0; i < 8; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    if(boardStatus[i, j] == FieldType.BlackPawn || boardStatus[i, j] == FieldType.BlackQueen)
                    {
                        blackPoint--;
                    }
                    else if (boardStatus[i, j] == FieldType.WhitePawn || boardStatus[i, j] == FieldType.WhiteQueen || boardStatus[i, j] == FieldType.SelectedPawn || boardStatus[i, j] == FieldType.SelectedQueen)
                    {
                        whitePoint--;
                    }
                }
            }

            playerScore = blackPoint;
            computerScore = whitePoint;
        }

        public void ComputerTurn()
        {
            AI();
            DrawBoard();
        }
        #region AI functions
        private void AI()
        {
            Tree tree = new Tree();
            Node[] listLevel1 = null;
            Node[] listLevel2 = null;
            Node[] listLevel3 = null;
            bool block = false;
            FieldType[,] board = BoardTo8x8(boardStatus);
            #region MAX algorithm
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (board[i, j] == FieldType.BlackPawn)
                    {
                        FieldType[,] copyBoard = new FieldType[8, 8];
                        Array.Copy(board, copyBoard, board.Length);
                        BlackPawn blackpawn = new BlackPawn(i, j, copyBoard);
                        if (blackpawn.PossibilityOfMoving() == true)
                        {
                            List<Node> listNode = blackpawn.ReturnNode();
                            foreach (var n in listNode)
                            {
                                tree.InsertLevel1(n);
                            }
                        }
                    }
                    else if (board[i, j] == FieldType.BlackQueen)
                    {
                        FieldType[,] copyBoard = new FieldType[8, 8];
                        Array.Copy(board, copyBoard, board.Length);
                        BlackQueen blackqueen = new BlackQueen(i, j, copyBoard);
                        if (blackqueen.PossibilityOfMoving() == true)
                        {
                            List<Node> listNode = blackqueen.ReturnNode();
                            foreach (var n in listNode)
                            {
                                tree.InsertLevel1(n);
                            }
                        }
                    }
                }
            }
            #endregion
            listLevel1 = tree.ToArrayLevel1();
            if (listLevel1.Length == 0) //Check that is there end of game by blocking possible of moving
            {
                for (var i = 0; i < 8; i++)
                {
                    for (var j = 0; j < 8; j++)
                    {
                        if (board[i, j] == FieldType.BlackPawn)
                        {
                            board[i, j] = FieldType.Free;
                            boardStatus = BoardTo8x4(board);
                            block = true;
                        }
                        else if (board[i, j] == FieldType.BlackQueen)
                        {
                            board[i, j] = FieldType.Free;
                            boardStatus = BoardTo8x4(board);
                            block = true;
                        }
                    }
                }
            }
            for (var k = 0; k < listLevel1.Length; k++)
            {
                #region MIN algorithm
                for (var i = 0; i < 8; i++)
                {
                    for (var j = 0; j < 8; j++)
                    {
                        if (listLevel1[k].currentBoard[i, j] == FieldType.WhitePawn)
                        {
                            FieldType[,] copyBoard = new FieldType[8, 8];
                            Array.Copy(listLevel1[k].currentBoard, copyBoard, listLevel1[k].currentBoard.Length);
                            WhitePawn whitepawn = new WhitePawn(i, j, copyBoard, listLevel1[k]);
                            if (whitepawn.PossibilityOfMoving() == true)
                            {
                                List<Node> listNode = whitepawn.ReturnNode();
                                foreach (var n in listNode)
                                {
                                    n.score = n.score + listLevel1[k].score;
                                    tree.InsertLevel2(n);
                                }
                            }
                        }
                        else if (listLevel1[k].currentBoard[i, j] == FieldType.WhiteQueen)
                        {
                            FieldType[,] copyBoard = new FieldType[8, 8];
                            Array.Copy(listLevel1[k].currentBoard, copyBoard, listLevel1[k].currentBoard.Length);
                            WhiteQueen whitequeen = new WhiteQueen(i, j, copyBoard, listLevel1[k]);
                            if (whitequeen.PossibilityOfMoving() == true)
                            {
                                List<Node> listNode = whitequeen.ReturnNode();
                                foreach (var n in listNode)
                                {
                                    n.score = n.score + listLevel1[k].score;
                                    tree.InsertLevel2(n);
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            listLevel2 = tree.ToArrayLevel2();
            if (listLevel2.Length != 0)
            {
                Node min = listLevel2[0];
                for (var k = 1; k < listLevel2.Length; k++)
                {
                    if (listLevel2[k].parent.score == listLevel2[k - 1].parent.score)
                    {
                        if (listLevel2[k].score < listLevel2[k - 1].score)
                        {
                            min = listLevel2[k];
                        }
                    }
                    else
                    {
                        #region MAX algorithm
                        for (var i = 0; i < 8; i++)
                        {
                            for (var j = 0; j < 8; j++)
                            {
                                if (min.currentBoard[i, j] == FieldType.BlackPawn)
                                {
                                    FieldType[,] copyBoard = new FieldType[8, 8];
                                    Array.Copy(min.currentBoard, copyBoard, min.currentBoard.Length);
                                    BlackPawn blackpawn = new BlackPawn(i, j, copyBoard, min);
                                    if (blackpawn.PossibilityOfMoving() == true)
                                    {
                                        List<Node> listNode = blackpawn.ReturnNode();
                                        foreach (var n in listNode)
                                        {
                                            n.score = n.score + min.score;
                                            tree.InsertLevel3(n);
                                        }
                                    }
                                }
                                else if (min.currentBoard[i, j] == FieldType.BlackQueen)
                                {
                                    FieldType[,] copyBoard = new FieldType[8, 8];
                                    Array.Copy(min.currentBoard, copyBoard, min.currentBoard.Length);
                                    BlackQueen blackqueen = new BlackQueen(i, j, copyBoard, min);
                                    if (blackqueen.PossibilityOfMoving() == true)
                                    {
                                        List<Node> listNode = blackqueen.ReturnNode();
                                        foreach (var n in listNode)
                                        {
                                            n.score = n.score + min.score;
                                            tree.InsertLevel3(n);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        min = listLevel2[k];
                    }

                    if (k + 1 == listLevel2.Length)
                    {
                        #region MAX algorithm
                        for (var i = 0; i < 8; i++)
                        {
                            for (var j = 0; j < 8; j++)
                            {
                                if (min.currentBoard[i, j] == FieldType.BlackPawn)
                                {
                                    FieldType[,] copyBoard = new FieldType[8, 8];
                                    Array.Copy(min.currentBoard, copyBoard, min.currentBoard.Length);
                                    BlackPawn blackpawn = new BlackPawn(i, j, copyBoard, min);
                                    if (blackpawn.PossibilityOfMoving() == true)
                                    {
                                        List<Node> listNode = blackpawn.ReturnNode();
                                        foreach (var n in listNode)
                                        {
                                            n.score = n.score + min.score;
                                            tree.InsertLevel3(n);
                                        }
                                    }
                                }
                                else if (min.currentBoard[i, j] == FieldType.BlackQueen)
                                {
                                    FieldType[,] copyBoard = new FieldType[8, 8];
                                    Array.Copy(min.currentBoard, copyBoard, min.currentBoard.Length);
                                    BlackQueen blackqueen = new BlackQueen(i, j, copyBoard, min);
                                    if (blackqueen.PossibilityOfMoving() == true)
                                    {
                                        List<Node> listNode = blackqueen.ReturnNode();
                                        foreach (var n in listNode)
                                        {
                                            n.score = n.score + min.score;
                                            tree.InsertLevel3(n);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            listLevel3 = tree.ToArrayLevel3();
            if (listLevel3.Length != 0)
            {
                Node maxNode = listLevel3[0];
                foreach (var n in tree.GetLevel3())
                {
                    if (n.score > maxNode.score)
                    {
                        maxNode = n;
                    }
                }

                FieldType[,] newBoard = BoardTo8x4(maxNode.parent.parent.currentBoard);
                boardStatus = newBoard;
            }
            else if (block == false)
            {
                FieldType[,] newBoard = BoardTo8x4(listLevel1[0].currentBoard);
                boardStatus = newBoard;
            }
        }

        private FieldType[,] BoardTo8x8(FieldType[,] board)
        {
            FieldType[,] newBoard = new FieldType[8, 8];
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
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
            return newBoard;
        }

        private FieldType[,] BoardTo8x4(FieldType[,] board)
        {
            FieldType[,] newBoard = new FieldType[8, 4];

            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (i % 2 == 0)
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
        #endregion
    }
}
