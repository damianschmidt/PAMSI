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
        private bool end = false;
        public Ranking ranking;

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
            board.Select(button, row, column);
            board.Hit(row, column);
            board.Move(button, row, column);
            Score();
            if (board.computerTurn == true && end != true)
            {
                board.ComputerTurn();
                board.computerTurn = false;
                Score();
            }
        }

        private void Score()
        {
            string username, currentTime;
            playerScore = board.PlayerScore();
            computerScore = board.ComputerScore();

            ((Label)this.mainWindow.FindName("PlayerScore")).Content = playerScore.ToString();
            ((Label)this.mainWindow.FindName("ComputerScore")).Content = computerScore.ToString();

            if(playerScore == 12)
            {
                ranking = new Ranking();
                this.mainWindow.stopWatch.Stop();
                //MessageBox.Show("Gratulacje, wygrałeś!", "Koniec gry!");
                username = Microsoft.VisualBasic.Interaction.InputBox("Gratulacje, wygrałeś ! Jeśli chcesz znaleźć się w rankingu, wpisz swoje imię poniżej:", "Koniec gry", "Imię");
                currentTime = this.mainWindow.getCurrentTime();
                ranking.GetUsername(username);
                ranking.SaveResults(username, currentTime);
                MessageBox.Show(username);      // To check if get name correctly
                MessageBox.Show(currentTime);   // To chcek if get time correctly
                var readrank = ranking.ReadResults();
                ranking.SaveToList(readrank);
                end = true;
            }
            else if(computerScore == 12)
            {
                this.mainWindow.stopWatch.Stop();
                MessageBox.Show("Przegrałeś!", "Koniec gry!");
                end = true;
            }
        }

        public bool EndOfGame()
        {
            if(playerScore == 12 || computerScore == 12)
            {
                playerScore = 0;
                computerScore = 0;
                end = false;
                return true;
            }
            return false;
        }
    }
}
