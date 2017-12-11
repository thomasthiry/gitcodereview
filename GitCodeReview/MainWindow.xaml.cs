using System.Windows;
using System.Windows.Controls;

namespace GitCodeReview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GitService _gitService;

        public MainWindow()
        {
            InitializeComponent();
            _gitService = new GitService();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            RefreshCommitsList();
        }

        private void RefreshCommitsList()
        {
            var commits = _gitService.GetCommits();

            listView.ItemsSource = null;
            listView.ItemsSource = commits;
        }

        private void ToggleCommitReview(object sender, RoutedEventArgs e)
        {
            var btMarkReviewed = sender as Button;
            var commit = btMarkReviewed?.CommandParameter as GitCommit;

            _gitService.ToggleCommitReviewed(commit);

            RefreshCommitsList();
        }
    }
}
