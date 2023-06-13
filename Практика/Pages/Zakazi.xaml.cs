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
    /// Логика взаимодействия для Zakazi.xaml
    /// </summary>
    public partial class Zakazi : Page
    {
        Car_DealershipDBEntities context;
        public Zakazi(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
            table.ItemsSource = context.Purchase.ToList();
        }
        

        private void sotrClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sotrudniki(context));
        }

        private void zakazClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Zakazi(context));
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage(context));
        }
        //удаление будет здесь
    }
}
