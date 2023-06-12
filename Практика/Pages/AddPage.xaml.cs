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
        }
        //для редактирования
        public AddPage(Car_DealershipDBEntities cont, Purchase zak)
        {
            InitializeComponent();
            context = cont;
            zakazi = zak;
            buttonAU.Content = "Редактировать";
            buttonAU.Click += UpdateClick;
            ModelBox.Text = zakazi.CodeProduct;
            FIOBox.Text = zakazi.IdClient;
            ManagerBox.Text = zakazi.IdManeger;
            DateBox.Text = zakazi.DatePuschase;
            DeliveryBox.Text = zakazi.Delivery;
            PayBox.Text = zakazi.PaymentType;
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Product prod = ModelBox.SelectedItem as Product;
                Client cl = FIOBox.SelectedItem as Client;
                Manager man = ManagerBox.SelectesItem as Manager;



                zakazi.CodeProduct = prod.Code;
                zakazi.Product = prod;
                zakazi.IdClient = Convert.ToInt32(cl.FIO);
                zakazi.idManeger = Convert.ToInt32(man.FIO);
                zakazi.DatePuschase = Convert.ToDateTime(DateBox.Text);
                zakazi.Delivery = Convert.ToBoolean(DeliveryBox.Text);
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
                Manager man = ManagerBox.SelectesItem as Manager;



                Purchase pur = new Purchase()
                {
                    Id = context.Purchase.ToList().Last().Id + 1, // Id = context.Purchase.Count() + 1  - если то не сработает
                    CodeProduct = prod.Code,
                    Product = prod,
                    IdClient = Convert.ToInt32(cl.FIO),
                    idManeger = Convert.ToInt32(man.FIO),
                    DatePuschase = Convert.ToDateTime(DateBox.Text),
                    Delivery = Convert.ToBoolean(DeliveryBox.Text),
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
