using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileListViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var files = Directory.GetFiles(@"C:\");
            FileListView.ItemsSource = files;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var fileInfo = new FileInfo(button.DataContext.ToString());
            MessageBox.Show(fileInfo.FullName);
        }

    }
}