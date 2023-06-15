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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        Car_DealershipDBEntities context;
        Purchase zakazi;
        public AddPage( Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
            ModelBox.ItemsSource = context.Product.ToList();
            FIOBox.ItemsSource = context.Client.ToList();
            ManagerBox.ItemsSource = context.Manager.ToList();
            PayBox.ItemsSource = context.Purchase.Distinct().ToList();
            //Purchase a = context.Purchase.ToList().First();
            //string name = a.Client.FIO;
        }
        //для редактирования
        public AddPage(Car_DealershipDBEntities cont, Purchase zak)
        {
            InitializeComponent();
            context = cont;
            zakazi = zak;
            buttonAU.Content = "Редактировать";
            buttonAU.Click += UpdateClick;
            ModelBox.SelectedItem = zakazi.Product;
            FIOBox.SelectedItem = zakazi.Client;
            ManagerBox.SelectedItem = zakazi.Manager;
            DateBox.Text = zakazi.DatePuschase.ToString();
            DeliveryBox.Text = zakazi.Delivery == true ? "Да" : "Нет";
            PayBox.Text = zakazi.PaymentType;
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Product prod = ModelBox.SelectedItem as Product;
                Client cl = FIOBox.SelectedItem as Client;
                Manager man = ManagerBox.SelectedItem as Manager;

                zakazi.CodeProduct = prod.Code;
                zakazi.Product = prod;
                zakazi.IdClient = Convert.ToInt32(cl.FIO);
                zakazi.idManeger = Convert.ToInt32(man.FIO);
                zakazi.DatePuschase = Convert.ToDateTime(DateBox.Text);
                zakazi.Delivery = DeliveryBox.Text.ToLower().Equals("да") ? true : false;
                zakazi.PaymentType = PayBox.Text;
                zakazi.Client = cl;
                zakazi.Manager = man;
                context.SaveChanges();
                NavigationService.Navigate(new Zakazi(context));
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
            
        }

        private void CanselClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddZakazClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Product prod = ModelBox.SelectedItem as Product;
                Client cl = FIOBox.SelectedItem as Client;
                Manager man = ManagerBox.SelectedItem as Manager;

                Purchase pur = new Purchase()
                {
                    Id = context.Purchase.ToList().Last().Id + 1,                // Id = context.Purchase.Count() + 1  - если то не сработает
                    CodeProduct = prod.Code,
                    Product = prod,
                    IdClient = Convert.ToInt32(cl.FIO),
                    idManeger = Convert.ToInt32(man.FIO),
                    DatePuschase = Convert.ToDateTime(DateBox.Text),             //10.05.2023 0:00:00 вот так вводить
                    Delivery = DeliveryBox.Text.ToLower().Equals("да") ? true : false,
                    PaymentType = PayBox.Text, 
                    Client = cl,
                    Manager = man
                };
                context.Purchase.Add(pur);
                context.SaveChanges();
                NavigationService.Navigate(new Zakazi(context));
            }
            catch(FormatException)
            {
                MessageBox.Show("Ошибка вводимых данных!");
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
