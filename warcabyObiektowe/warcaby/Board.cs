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
            //Create a new blank array of free cells
            boardStatus = new FieldType[8, 4];

            //Init default value of fields on board
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    boardStatus[i, j] = FieldType.BlackPawn;
                    boardStatus[(7 - i), j] = FieldType.WhitePawn;
                    if (i < 2)
                    {
                        boardStatus[(i + 3), j] = FieldType.Free;
                    }
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

        public void SelectWhitePawn(Button button, int row, int column) //Temporary public
        {
            if ((boardStatus[row, column] == FieldType.WhitePawn) && (selectedPawn == false))
            {
                //RemoveMove();
                LoadPicture(".\\img\\jpg\\checker-selected.jpg", button);

                selCol = column;
                selRow = row;
                selectedPawn = true;
                boardStatus[row, column] = FieldType.SelectedPawn;
                //CheckFields(row, column);
            }
            else
            {
                //RemoveMove();
                LoadPicture(".\\img\\jpg\\checker-selected.jpg", button);
                boardStatus[row, column] = FieldType.SelectedPawn;

                LoadPicture(".\\img\\jpg\\checker-white.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.WhitePawn;

                //CheckFields(row, column);

                selRow = row;
                selCol = column;
            }
        }
        private void MoveWhitePawn(int row, int column)
        {

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
