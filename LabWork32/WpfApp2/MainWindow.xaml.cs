using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LabWork32
{
    public partial class MainWindow : Window
    {
        private List<Product> allProducts;
        private ObservableCollection<Product> cartProducts;

        public MainWindow()
        {
            InitializeComponent();

            // Заполнение категорий программно
            var categories = new List<string> {
                "Электроника", "Одежда", "Книги", "Игрушки", "Спорт",
                "Дом", "Красота", "Авто", "Мебель", "Продукты"
            };
            foreach (var cat in categories)
                CategoryListBox.Items.Add(cat);

            // Создание товаров
            allProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Ноутбук", Category = "Электроника", Price = 50000 },
                new Product { Id = 2, Name = "Футболка", Category = "Одежда", Price = 1500 },
                new Product { Id = 3, Name = "Война и мир", Category = "Книги", Price = 800 },
                new Product { Id = 4, Name = "LEGO", Category = "Игрушки", Price = 3000 },
                new Product { Id = 5, Name = "Мяч", Category = "Спорт", Price = 1200 }
            };

            cartProducts = new ObservableCollection<Product>();
            CartListBox.ItemsSource = cartProducts;
            ProductsListView.ItemsSource = allProducts;

            // Установка начального шаблона (плитка)
            TileViewButton.IsChecked = true;
            ProductsListView.ItemsPanel = (ItemsPanelTemplate)FindResource("TilePanelTemplate");
            ProductsListView.ItemTemplate = (DataTemplate)FindResource("TileTemplate");
        }

        // 5.2.1 Отображение выбранной сортировки
        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            SortTextBlock.Text = $"Сортировка: {selected}";
        }

        // 5.2.2 Отображение выбранной категории
        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = CategoryListBox.SelectedItem?.ToString();
            CategoryTextBlock.Text = $"Категория: {selected ?? "не выбрана"}";
        }

        // 5.2.3 Вывод отмеченных флажков наличия
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAvailabilities = new List<string>();
            foreach (var item in AvailabilityListBox.Items)
            {
                if (item is CheckBox checkBox && checkBox.IsChecked == true)
                    selectedAvailabilities.Add(checkBox.Content.ToString());
            }
            AvailabilityTextBlock.Text = $"Наличие: {string.Join(", ", selectedAvailabilities)}";
        }

        // 5.4.2 Добавление в корзину
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsListView.SelectedItem is Product selectedProduct)
                cartProducts.Add(selectedProduct);
        }

        // 5.4.3 Удаление из корзины (с конца, чтобы избежать ошибок индексации)
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = CartListBox.SelectedItems.Cast<Product>().ToList();
            foreach (var item in selectedItems)
                cartProducts.Remove(item);
        }

        // 5.5.3 Переключение на плитку
        private void TileViewButton_Click(object sender, RoutedEventArgs e)
        {
            TileViewButton.IsChecked = true;
            ListViewButton.IsChecked = false;
            ProductsListView.ItemsPanel = (ItemsPanelTemplate)FindResource("TilePanelTemplate");
            ProductsListView.ItemTemplate = (DataTemplate)FindResource("TileTemplate");
        }

        // 5.5.3 Переключение на список
        private void ListViewButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewButton.IsChecked = true;
            TileViewButton.IsChecked = false;
            ProductsListView.ItemsPanel = (ItemsPanelTemplate)FindResource("ListPanelTemplate");
            ProductsListView.ItemTemplate = (DataTemplate)FindResource("ListTemplate");
        }
    }
}