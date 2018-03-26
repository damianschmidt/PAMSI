using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        //private Button[,] buttonName = new Button[,] { { B1, B2, B3, B4 }, { B5, B6, B7, B8 }, { B9, B10, B11, B12 }, { B13, B14, B15, B16 },
        //                                              { B17, B18, B19, B20 }, { B21, B22, B23, B24 }, { B25, B26, B27, B28 }, { B29, B30, B31, B32 }};

        public MainWindow()
        {
            InitializeComponent();
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

            //Iterate every button on the grid
            int k = 1;
            FieldsGrid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                if (k < 13)
                {
                    Uri resourceUri = new Uri(".\\img\\jpg\\checker-black.jpg", UriKind.Relative);
                    StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                    BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                    var brush = new ImageBrush();
                    brush.ImageSource = temp;
                    button.Background = brush;
                    k++;
                }
                else if (k < 21)
                {
                    Uri resourceUri = new Uri(".\\img\\jpg\\field-dark.jpg", UriKind.Relative);
                    StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                    BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                    var brush = new ImageBrush();
                    brush.ImageSource = temp;
                    button.Background = brush;
                    k++;
                } else
                {
                    Uri resourceUri = new Uri(".\\img\\jpg\\checker-white.jpg", UriKind.Relative);
                    StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                    BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                    var brush = new ImageBrush();
                    brush.ImageSource = temp;
                    button.Background = brush;
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

            if((boardStatus[row, column] == FieldType.WhitePawn) && selected == false)
            {
                Uri resourceUri = new Uri(".\\img\\jpg\\checker-selected.jpg", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                var brush = new ImageBrush();
                brush.ImageSource = temp;
                button.Background = brush;

                selCol = column;
                selRow = row;
                selected = true;
                boardStatus[row, column] = FieldType.SelectedPawn;
            }
            else if (boardStatus[row, column] == FieldType.SelectedPawn)
            {
                return;
            }
            else
            {
                Uri resourceUri = new Uri(".\\img\\jpg\\checker-black.jpg", UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
                var brush = new ImageBrush();
                brush.ImageSource = temp;
                button.Background = brush;
            }
        }


        //Ranking's functions

        private void Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsSelected = true;
        }


    }
}
