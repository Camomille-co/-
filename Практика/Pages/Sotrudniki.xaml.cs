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
    public partial class Sotrudniki : Page
    {
        Car_DealershipDBEntities context;
        public Sotrudniki(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void zakazClick(object sender, RoutedEventArgs e)
        {
            frameToBasePage.Navigate(new Zakazi(context));
        }

        private void sotrClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sotrudniki(context));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            table.ItemsSource = AppData.db.Manager.ToList();
        }
    }
}
