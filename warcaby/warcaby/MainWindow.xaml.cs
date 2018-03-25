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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame()
        {
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
            dispatcherTimer.Start();
            stopWatch.Start();

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

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
            stopWatch.Restart();
        }


        //Ranking's functions

        private void Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsSelected = true;
        }


    }
}
