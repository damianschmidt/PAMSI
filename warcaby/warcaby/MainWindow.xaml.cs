using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;

namespace warcaby
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Stopwatch stopWatch = new Stopwatch();
        private string currentTime = string.Empty;
        private FieldType[,] boardStatus;
        private bool selected = false;
        private int selCol = 0;
        private int selRow = 0;
        private Button[,] buttonName;

        public MainWindow()
        {
            InitializeComponent();
            //Set values of of ButtonArray
            buttonName = new Button[,] { { B1, B2, B3, B4 }, { B5, B6, B7, B8 }, { B9, B10, B11, B12 }, { B13, B14, B15, B16 }, { B17, B18, B19, B20 }, { B21, B22, B23, B24 }, { B25, B26, B27, B28 }, { B29, B30, B31, B32 } };
        }

        private void NewGame()
        {
            //Set stopwatch to 0
            timer.Content = "00:00";

            //Create a new blank array of free cells
            boardStatus = new FieldType[8,4];

            //init default value of fields on board
            for(var i = 0; i < 3; i++)
            {
                for(var j = 0; j < 4; j++)
                {
                    boardStatus[i, j] = FieldType.BlackPawn;
                    boardStatus[(7 - i), j] = FieldType.WhitePawn;
                    if (i < 2)
                    {
                        boardStatus[(i + 3), j] = FieldType.Free;
                    }
                }
            }

            //reset selected button property
            selected = false;

            //Iterate every button on the grid
            int k = 1;
            FieldsGrid.Children.Cast<Button>().ToList().ForEach(button =>
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
                } else
                {
                    LoadPicture(".\\img\\jpg\\checker-white.jpg", button);
                }
            });

            //init stopwatch
            if (stopWatch.IsRunning)
            {
                stopWatch.Restart();
            }
            else
            {
                dispatcherTimer.Start();
                stopWatch.Start();
            }

            //set scores to 0
            PlayerScore.Content = "0";
            ComputerScore.Content = "0";

        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            Board.IsSelected = true;
            NewGame();
        }

        private void Ranking_Button_Click(object sender, RoutedEventArgs e)
        {
            Ranking.IsSelected = true;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //Board's functions
        #region StopWatch functions
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

                timer.Content = currentTime;
            }
        }
        #endregion

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
            stopWatch.Restart();
        }

        private void Board_Button_Click(object sender, RoutedEventArgs e)
        {
            //Cast the sender tu a button
            var button = (Button)sender;
            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            
            if((boardStatus[row, column] == FieldType.WhitePawn) && (selected == false))
            {
                RemoveMove();
                LoadPicture(".\\img\\jpg\\checker-selected.jpg", button);
                CheckFields(row, column);

                selCol = column;
                selRow = row;
                selected = true;
                boardStatus[row, column] = FieldType.SelectedPawn;
            }
            else if ((boardStatus[row, column] == FieldType.WhitePawn) && (selected == true))
            {
                RemoveMove();
                LoadPicture(".\\img\\jpg\\checker-selected.jpg", button);
                boardStatus[row, column] = FieldType.SelectedPawn;

                LoadPicture(".\\img\\jpg\\checker-white.jpg", buttonName[selRow, selCol]);
                boardStatus[selRow, selCol] = FieldType.WhitePawn;

                CheckFields(row, column);

                selRow = row;
                selCol = column;
            }
            else if (boardStatus[row, column] == FieldType.SelectedPawn)
            {
                return;
            }
            else if (boardStatus[row, column] == FieldType.Move)
            {
                MoveWhite(button, row, column);
            }
        }

        private void MoveWhite(Button button, int row, int column)
        {
            RemoveMove();
            LoadPicture(".\\img\\jpg\\checker-white.jpg", button);
            boardStatus[row, column] = FieldType.WhitePawn;

            LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[selRow, selCol]);
            boardStatus[selRow, selCol] = FieldType.Free;

            selected = false;
        }

        private void CheckFields(int row, int column)
        {
        bool top = false, bottom = false, leftside = false, rightside = false, even = false, odd = false;

            #region checking position
            if (row == 0)
            {
                top = true;
            }
            if(row == 7)
            {
                bottom = true;
            }
            if(column == 0)
            {
                leftside = true;
            }
            if(column == 3)
            {
                rightside = true;
            }
            if(row == 0 || row == 2 || row == 4 || row == 6)
            {
                odd = true;
            }
            if(row == 1 || row == 3 || row == 5 || row == 7)
            {
                even = true;
            }
            #endregion

            if ((top == true) && (leftside == true))
            {
                return; //Temporary do nothing cause not queen condition
            }
            else if (top == true)
            {
                return; //Temporary do nothing cause not queen condition
            }
            else if ((bottom == true) && (rightside == true))
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
            }
            else if (bottom == true)
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
                if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column + 1)]);
                }
            }
            else if ((leftside == true) && (even == true))
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
                if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column + 1)]);
                }
            }
            else if (leftside == true)
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
            }
            else if ((rightside == true) && (odd == true))
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
                if (boardStatus[(row - 1), (column - 1)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column - 1)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column - 1)]);
                }
            }
            else if (rightside == true)
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
            }
            else if (odd == true)
            {
                if (boardStatus[(row - 1), (column - 1)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column - 1)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column - 1)]);
                }
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
            }
            else if (even == true)
            {
                if (boardStatus[(row - 1), (column)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column)]);
                }
                if (boardStatus[(row - 1), (column + 1)] == FieldType.Free)
                {
                    boardStatus[(row - 1), (column + 1)] = FieldType.Move;
                    LoadPicture(".\\img\\jpg\\move.jpg", buttonName[(row - 1), (column + 1)]);
                }
            }
        }

        private void RemoveMove()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if(boardStatus[i,j] == FieldType.Move)
                    {
                        boardStatus[i, j] = FieldType.Free;
                        LoadPicture(".\\img\\jpg\\field-dark.jpg", buttonName[i, j]);
                    }
                }
            }
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


        //Ranking's functions

        private void Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsSelected = true;
        }


    }
}
