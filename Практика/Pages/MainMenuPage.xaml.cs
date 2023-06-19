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
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        Car_DealershipDBEntities context;
        //так как мы находимся на странице, мы не можем обратиться сразу к окну
        //поэтому его получаем из предыдущей страницы
        Window Window;
        public MainMenuPage(Car_DealershipDBEntities cont, Window window)
        {
            InitializeComponent();
            context = cont;
            Window = window;
        }

        /// <summary>
        /// Клик по кнопке "Выход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void exitClick(object sender, RoutedEventArgs e)
        {
            //закрытие окна
            Window.Close();
        }

        /// <summary>
        /// открытие страницы Заказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void zakazClick(object sender, RoutedEventArgs e)
        {
            frameToBasePage.Navigate(new Zakazi(context));
        }

        private void TovarClick(object sender, RoutedEventArgs e)
        {
            frameToBasePage.Navigate(new ProductPage(context));
        }
    }
}
