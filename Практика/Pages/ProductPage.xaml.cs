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

namespace Практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для Sotrudniki.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Car_DealershipDBEntities context;
        public ProductPage(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
            listProducts.ItemsSource = context.Product.ToList().Distinct();

            var categoryList = context.Product.ToList();
            categoryList.Insert(0, new Product() { Country = "Все" });
            categoryBox.ItemsSource = categoryList;
            categoryBox.SelectedIndex = 0;
        }

        void FilterData()
        {
            var list = context.Product.ToList();
            if (categoryBox.SelectedIndex != 0)
            {
                Product tovar = categoryBox.SelectedItem as Product;
                list = list.Where(x=>x.Country == tovar.Country).ToList();
            }
            if(!string.IsNullOrWhiteSpace(searchBox.Text))
            {
                list = list.Where(x => x.Mark.Title.ToLower().Contains(searchBox.Text.ToLower())).ToList();
            }
            listProducts.ItemsSource = list;
        }

        private void ChangeCategory(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void SearchChange(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }
    }
}
