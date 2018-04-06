﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
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
        private Game G;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            BoardTab.IsSelected = true;

            //Init game
            Stopwatch();
            var G = new Game(this);
            G.InitGame();
        }

        private void Ranking_Button_Click(object sender, RoutedEventArgs e)
        {
            RankingTab.IsSelected = true;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            MenuTab.IsSelected = true;
        }

        private void Restart_Button_Click(object sender, RoutedEventArgs e)
        {
            //Init game again 
            Stopwatch();
            var G = new Game(this);
            G.InitGame();
        }

        private void Board_Button_Click(object sender, RoutedEventArgs e)
        {
            //Cast the sender tu a button
            var button = (Button)sender;
            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            G.Select(button, row, column);
        }

        //Stopwatch
        private void Stopwatch()

        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Restart();
            }
            else
            {
                dispatcherTimer.Start();
                stopWatch.Start();
            }
        }
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
    }
}
