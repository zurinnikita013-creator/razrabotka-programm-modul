using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LabWork32
{
    public partial class MainWindow : Window
    {
        // 5.3.1
        private List<Product> allProducts;

        private ObservableCollection<Product> cart = new ObservableCollection<Product>();

        public class AvailabilityOption
        {
            public string Name { get; set; }
            public bool IsSelected { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            LoadData();
            LoadCategories();
            LoadAvailability();

            // 5.3.2 
            lvProducts.ItemsSource = allProducts;
            lbCart.ItemsSource = cart;

            SetListViewStyle();
        }

        // 5.3.2
        private void LoadData()
        {
            allProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Ноутбук Lenovo ThinkPad", Category = "Электроника", Price = 58999 },
                new Product { Id = 2, Name = "Мышь Logitech MX Master", Category = "Электроника", Price = 5990 },
                new Product { Id = 3, Name = "Книга 'WPF на примерах'", Category = "Книги", Price = 850 },
                new Product { Id = 4, Name = "Футболка Nike Dry", Category = "Одежда", Price = 1990 },
                new Product { Id = 5, Name = "Смартфон Xiaomi 13", Category = "Электроника", Price = 45990 },
                new Product { Id = 6, Name = "Кроссовки Adidas Ultraboost", Category = "Обувь", Price = 12990 },
                new Product { Id = 7, Name = "Наушники Sony WH-1000XM5", Category = "Электроника", Price = 28990 },
                new Product { Id = 8, Name = "Смарт-часы Apple Watch", Category = "Аксессуары", Price = 39990 },
                new Product { Id = 9, Name = "Рюкзак Deuter Giga", Category = "Сумки", Price = 7990 },
                new Product { Id = 10, Name = "Зарядное устройство Baseus", Category = "Электроника", Price = 1290 }
            };
        }

        // 5.1.3
        private void LoadCategories()
        {
            var categories = new List<string>
            {
                "Все",
                "Электроника",
                "Книги",
                "Одежда",
                "Обувь",
                "Аксессуары",
                "Сумки",
                "Игрушки",
                "Спорт",
                "Красота"
            };

            lbCategories.ItemsSource = categories;

            if (lbCategories.Items.Count > 0)
            {
                lbCategories.SelectedIndex = 0;
            }
        }

        // 5.1.2
        private void LoadAvailability()
        {
            var options = new List<AvailabilityOption>
            {
                new AvailabilityOption { Name = "в наличии", IsSelected = true },
                new AvailabilityOption { Name = "под заказ: сегодня", IsSelected = true },
                new AvailabilityOption { Name = "под заказ: завтра", IsSelected = true },
                new AvailabilityOption { Name = "под заказ: позже", IsSelected = true },
                new AvailabilityOption { Name = "нет в продаже", IsSelected = false }
            };

            lbAvailability.ItemsSource = options;
        }

        // 5.2.1
        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSort.SelectedItem is ComboBoxItem item && item.Content != null)
            {
                string selectedSort = item.Content.ToString();
                tbSort.Text = $"Сортировка: {selectedSort}";
                ApplySorting(selectedSort);
            }
        }

        // Сортировка
        private void ApplySorting(string sortBy)
        {
            if (allProducts == null) return;

            IEnumerable<Product> sortedProducts = allProducts;

            switch (sortBy)
            {
                case "сначала недорогие":
                    sortedProducts = allProducts.OrderBy(p => p.Price);
                    break;
                case "сначала дорогие":
                    sortedProducts = allProducts.OrderByDescending(p => p.Price);
                    break;
                case "по новинкам":
                    sortedProducts = allProducts.OrderByDescending(p => p.Id);
                    break;
                case "по скидке":
                    sortedProducts = allProducts.OrderBy(p => p.Price * 0.85m);
                    break;
                case "по количеству отзывов":
                    sortedProducts = allProducts.OrderByDescending(p => p.Id % 100);
                    break;
                case "сначала с лучшей оценкой":
                    sortedProducts = allProducts.OrderByDescending(p => p.Id % 50);
                    break;
                default:
                    sortedProducts = allProducts;
                    break;
            }

            lvProducts.ItemsSource = sortedProducts.ToList();
        }

        // 5.2.2
        private void LbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategories.SelectedItem != null)
            {
                string selectedCategory = lbCategories.SelectedItem.ToString();
                tbCategory.Text = $"Категория: {selectedCategory}";
                FilterProductsByCategory(selectedCategory);
            }
        }

        // Фильтрация
        private void FilterProductsByCategory(string category)
        {
            if (allProducts == null) return;

            IEnumerable<Product> filteredProducts;

            if (category == "Все")
            {
                filteredProducts = allProducts;
            }
            else
            {
                filteredProducts = allProducts.Where(p => p.Category == category);
            }

            if (cbSort.SelectedItem is ComboBoxItem item && item.Content != null)
            {
                string currentSort = item.Content.ToString();
                filteredProducts = ApplySortingToCollection(filteredProducts, currentSort);
            }

            lvProducts.ItemsSource = filteredProducts.ToList();
        }

        private IEnumerable<Product> ApplySortingToCollection(IEnumerable<Product> products, string sortBy)
        {
            switch (sortBy)
            {
                case "сначала недорогие":
                    return products.OrderBy(p => p.Price);
                case "сначала дорогие":
                    return products.OrderByDescending(p => p.Price);
                case "по новинкам":
                    return products.OrderByDescending(p => p.Id);
                case "по скидке":
                    return products.OrderBy(p => p.Price * 0.85m);
                case "по количеству отзывов":
                    return products.OrderByDescending(p => p.Id % 100);
                case "сначала с лучшей оценкой":
                    return products.OrderByDescending(p => p.Id % 50);
                default:
                    return products;
            }
        }

        // 5.2.3
        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {

            var selectedOptions = new List<string>();

            foreach (var item in lbAvailability.Items)
            {
                var option = item as AvailabilityOption;
                if (option != null && option.IsSelected)
                {
                    selectedOptions.Add(option.Name);
                }
            }

            string result = string.Join(", ", selectedOptions);

            MessageBox.Show($"Выбранные фильтры наличия:\n{result}",
                          "Применено",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);
        }

        // 5.4.2
        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (lvProducts.SelectedItem is Product selectedProduct)
            {
                cart.Add(selectedProduct);
            }
            else
            {
                MessageBox.Show("Выберите товар для покупки", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // 5.4.3 
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbCart.SelectedItems.Count > 0)
            {

                var selectedItems = lbCart.SelectedItems.Cast<Product>().ToList();

                foreach (var item in selectedItems)
                {
                    cart.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Выберите товары для удаления из корзины", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // 5.5.3 
        private void BtnList_Click(object sender, RoutedEventArgs e)
        {
            btnList.IsChecked = true;
            btnTile.IsChecked = false;
            SetListViewStyle();
        }

        private void BtnTile_Click(object sender, RoutedEventArgs e)
        {
            btnTile.IsChecked = true;
            btnList.IsChecked = false;
            SetTileStyle();
        }


        private void SetListViewStyle()
        {
            lvProducts.ItemsPanel = (ItemsPanelTemplate)FindResource("ListPanelTemplate");
            lvProducts.ItemTemplate = (DataTemplate)FindResource("ListTemplate");
        }


        private void SetTileStyle()
        {
            lvProducts.ItemsPanel = (ItemsPanelTemplate)FindResource("TilePanelTemplate");
            lvProducts.ItemTemplate = (DataTemplate)FindResource("CardTemplate");
        }
    }
}