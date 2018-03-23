using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            Board.IsSelected = true;
            dispatcherTimer.Start(); //Init stopwatch at board
            stopWatch.Start();
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
            stopWatch.Restart();
        }
    }
}
