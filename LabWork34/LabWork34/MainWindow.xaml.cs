using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LabWork34
{
    public partial class MainWindow : Window
    {
        private string currentFilePath = null;
        private bool isUpdatingZoom = false;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация после загрузки всех компонентов
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Устанавливаем начальные значения
            if (FontSizeCombo != null)
                FontSizeCombo.SelectedIndex = 1;

            if (ZoomSlider != null)
                ZoomSlider.Value = 12;

            UpdateZoomPercent();
        }

        // === Файл ===
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = "Текстовые файлы|*.txt" };
            if (dialog.ShowDialog() == true)
            {
                MainTextBox.Text = File.ReadAllText(dialog.FileName);
                currentFilePath = dialog.FileName;
            }
        }

        private void SaveCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                var dialog = new SaveFileDialog { Filter = "Текстовые файлы|*.txt" };
                if (dialog.ShowDialog() == true)
                    currentFilePath = dialog.FileName;
                else return;
            }
            File.WriteAllText(currentFilePath, MainTextBox.Text);
        }

        private void CanSaveCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !string.IsNullOrEmpty(MainTextBox.Text);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Печать (не реализовано)", "Информация");
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        // === Строка состояния ===
        private void ToggleStatusBar_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null && StatusBarPanel != null)
            {
                StatusBarPanel.Visibility = menuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CharCountText != null)
            {
                CharCountText.Text = $"Символов: {MainTextBox.Text.Length}";
            }
        }

        // === Масштаб ===
        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox == null) return;

            double newSize = MainTextBox.FontSize + 2;
            if (newSize <= 40)
            {
                MainTextBox.FontSize = newSize;
                UpdateZoomUI();
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox == null) return;

            double newSize = MainTextBox.FontSize - 2;
            if (newSize >= 8)
            {
                MainTextBox.FontSize = newSize;
                UpdateZoomUI();
            }
        }

        private void ZoomReset_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox == null) return;

            MainTextBox.FontSize = 12;
            UpdateZoomUI();
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isUpdatingZoom) return;
            if (MainTextBox == null) return;

            isUpdatingZoom = true;
            MainTextBox.FontSize = e.NewValue;
            UpdateZoomPercent();
            isUpdatingZoom = false;
        }

        private void UpdateZoomUI()
        {
            if (isUpdatingZoom) return;
            if (MainTextBox == null || ZoomSlider == null) return;

            isUpdatingZoom = true;
            ZoomSlider.Value = MainTextBox.FontSize;
            UpdateZoomPercent();
            isUpdatingZoom = false;
        }

        private void UpdateZoomPercent()
        {
            if (MainTextBox == null || ZoomPercent == null) return;

            double percent = (MainTextBox.FontSize / 12.0) * 100;
            ZoomPercent.Text = $"{percent:F0}%";
        }

        // === Панель инструментов ===
        private void FontSize_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (MainTextBox == null) return;
            if (FontSizeCombo.SelectedItem is ComboBoxItem item &&
                double.TryParse(item.Content.ToString(), out double size))
            {
                MainTextBox.FontSize = size;
                UpdateZoomUI();
            }
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox == null) return;
            MainTextBox.FontWeight = BoldButton.IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox == null) return;
            MainTextBox.FontStyle = ItalicButton.IsChecked == true ? FontStyles.Italic : FontStyles.Normal;
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox == null) return;
            MainTextBox.TextDecorations = UnderlineButton.IsChecked == true ? TextDecorations.Underline : null;
        }

        private void Align_Left(object sender, RoutedEventArgs e)
        {
            if (MainTextBox != null) MainTextBox.TextAlignment = TextAlignment.Left;
        }

        private void Align_Center(object sender, RoutedEventArgs e)
        {
            if (MainTextBox != null) MainTextBox.TextAlignment = TextAlignment.Center;
        }

        private void Align_Right(object sender, RoutedEventArgs e)
        {
            if (MainTextBox != null) MainTextBox.TextAlignment = TextAlignment.Right;
        }

        private void Align_Justify(object sender, RoutedEventArgs e)
        {
            if (MainTextBox != null) MainTextBox.TextAlignment = TextAlignment.Justify;
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            var colorMenu = new ContextMenu();
            var colors = new (string, Color)[]
            {
                ("Черный", Colors.Black),
                ("Красный", Colors.Red),
                ("Синий", Colors.Blue),
                ("Зеленый", Colors.Green),
                ("Оранжевый", Colors.Orange),
                ("Фиолетовый", Colors.Purple)
            };

            foreach (var color in colors)
            {
                var item = new MenuItem { Header = color.Item1 };
                item.Click += (s, args) =>
                {
                    if (MainTextBox != null)
                        MainTextBox.Foreground = new SolidColorBrush(color.Item2);
                };
                colorMenu.Items.Add(item);
            }
            colorMenu.IsOpen = true;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Текстовый редактор\nЛабораторная работа №34", "О программе");
        }

        // === Контекстное меню ===
        private void ContextClear_Click(object sender, RoutedEventArgs e)
        {
            if (MainTextBox != null) MainTextBox.Clear();
        }

        private void ContextSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MainTextBox.Text))
                SaveCommand(sender, null);
        }

        private void ContextExit_Click(object sender, RoutedEventArgs e) => Close();

        // === Гамбургер ===
        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            if (NewLabel == null || OpenLabel == null || SaveLabel == null || HamburgerPanel == null) return;

            if (NewLabel.Visibility == Visibility.Visible)
            {
                NewLabel.Visibility = Visibility.Collapsed;
                OpenLabel.Visibility = Visibility.Collapsed;
                SaveLabel.Visibility = Visibility.Collapsed;
                HamburgerPanel.Width = 45;
            }
            else
            {
                NewLabel.Visibility = Visibility.Visible;
                OpenLabel.Visibility = Visibility.Visible;
                SaveLabel.Visibility = Visibility.Visible;
                HamburgerPanel.Width = 150;
            }
        }

        private void SaveCommand(object sender, RoutedEventArgs e) => SaveCommand(sender, null);
    }
}