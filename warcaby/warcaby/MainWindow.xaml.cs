using System.Windows;

namespace warcaby
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewGame_Button_Click(object sender, RoutedEventArgs e)
        {
            Board.IsSelected = true;
        }

        private void Ranking_Button_Click(object sender, RoutedEventArgs e)
        {
            Ranking.IsSelected = true;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
