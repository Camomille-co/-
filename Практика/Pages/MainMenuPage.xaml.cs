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
        Window Window;
        public MainMenuPage(Car_DealershipDBEntities cont, Window window)
        {
            InitializeComponent();
            context = cont;
            Window = window;
        }

        private void exitClick(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Authorization(context));
            Window.Close();
        }

        private void SotrClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sotrudniki(context));
        }

        private void zakazClick(object sender, RoutedEventArgs e)
        {
            frameToBasePage.Navigate(new Zakazi(context));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("He He");
        }

        private void Bez_Nal_Click(object sender, RoutedEventArgs e)
        {
            Purchase pur = new Purchase();
            //var a = comboBox1.Text;
            //var r = pur.PaymentType.Distinct().ToList();
            //var e = pur.Where(XmlDataProvider => XmlDataProvider.PaymentType == a).ToList();
            //table.ItemsSource = res;
            //context.Purchase.Where(x=>x.PaymentType == a).ToList();



            //удаление
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить данный заказ?","Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Purchase puri = (sender as Hyperlink).DataContext as Purchase;
                    context.Purchase.Remove(puri);
                    context.SaveChanges();
                    //table.ItemsSource = context.Purchase.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
            //редактировать (кнопка Edit у контекстного меню)
            Zakazi zak = (sender as Hyperlink).DataContext as Zakazi;
            NavigationService.Navigate(new AddPage(context, zak));
        }
    }
}
