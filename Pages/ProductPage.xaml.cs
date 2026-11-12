using demka.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demka.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private List<Products> _all = new List<Products>();

       
        private bool _loading;
        public ProductPage()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            _loading = true;

            _all = AppConnect.modeldb.Products.ToList();

            cmbSort.Items.Clear();
            cmbSort.Items.Add("Без сортировки");
            cmbSort.Items.Add("Цена по возрастанию");
            cmbSort.Items.Add("Цена по убыванию");
            cmbSort.Items.Add("Количество по возрастанию");
            cmbSort.Items.Add("Количество по убыванию");
            cmbSort.SelectedIndex = 0;

            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("Все категории");
            foreach (var c in AppConnect.modeldb.Categories.ToList())
                cmbFilter.Items.Add(c.category);
            cmbFilter.SelectedIndex = 0;

            _loading = false;
            Apply();
        }

        private void Apply()
        {
            if (_loading) return;

            IEnumerable<Products> list = _all;

           
            string q = txtSearch.Text.Trim().ToLower();
            if (q.Length > 0)
                list = list.Where(p =>
                    p.ProductName.ToLower().Contains(q) ||
                    p.description.ToLower().Contains(q) ||
                    p.structure.ToLower().Contains(q) ||
                    p.Producer.producer1.ToLower().Contains(q) ||
                    p.StockItems.Manufacturers.manufacturer.ToLower().Contains(q) ||
                    p.Categories.category.ToLower().Contains(q));

          
            if (cmbFilter.SelectedIndex > 0)
            {
                string cat = cmbFilter.SelectedItem.ToString();
                list = list.Where(p => p.Categories.category == cat);
            }

            
            switch (cmbSort.SelectedIndex)
            {
                case 1: list = list.OrderBy(p => p.price); break;
                case 2: list = list.OrderByDescending(p => p.price); break;
                case 3: list = list.OrderBy(p => p.Quantity); break;
                case 4: list = list.OrderByDescending(p => p.Quantity); break;
            }

            var result = list.ToList();
            listProduct.ItemsSource = result;
            txtCount.Text = "Показано " + result.Count + " из " + _all.Count;

            if (result.Count == 0 && _all.Count > 0)
                txtCount.Text = "Ничего не найдено";
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Apply();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Apply();

        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Apply();

        }

        private void cmbFilter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Apply();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new Pages.AuthorizationPage());

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            Apply();

        }
    }
}
