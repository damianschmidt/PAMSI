using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace warcaby
{
    class Game
    {
        private int playerScore { get; set; } = 0;
        private int computerScore { get; set; } = 0;
        private MainWindow mainWindow;
        private Board board;

        public Game(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            board = new Board(mainWindow);
        }

        public void InitGame()
        {
            //Set stopwatch to 0
            ((Label)this.mainWindow.FindName("timer")).Content = "00:00";

            //Set scores to 0
            ((Label)this.mainWindow.FindName("PlayerScore")).Content = "0";
            ((Label)this.mainWindow.FindName("ComputerScore")).Content = "0";

            //Init board
            board.InitBoard();
        }

        public void Action(Button button, int row, int column)
        {
            /* Example of using tree   ( 2 sons and 1 vater )
             Tree tree = new Tree();
             tree.Root = new Node(1);
             tree.Root.Left = new Node(2);
             tree.Root.Right = new Node(3);
             */
            board.Select(button, row, column);
            board.Hit(row, column);
            board.Move(button, row, column);
            Score();
        }

        private void Score()
        {
            playerScore = board.PlayerScore();
            computerScore = board.ComputerScore();

            ((Label)this.mainWindow.FindName("PlayerScore")).Content = playerScore.ToString();
            ((Label)this.mainWindow.FindName("ComputerScore")).Content = computerScore.ToString();
        }
    }
}
