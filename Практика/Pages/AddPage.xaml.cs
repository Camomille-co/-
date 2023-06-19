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
    /// Так как для добавления и редактирования один и тот же интерфейс,
    /// можно создать одну страницу для двух действий
    /// Но будут различаться конструкторы: один для добавления, второй для редактирвоания
    /// для кнопки также будет создано 2 разных события. 
    /// Первое для добавления привязывается через интерфейс (.xaml)
    /// Второе для редактирования привязывается в соответствующем конструкторе
    /// </summary>
    public partial class AddPage : Page
    {
        Car_DealershipDBEntities context;
        Purchase zakazi;

        /// <summary>
        /// конструктор для Добавления элемента
        /// </summary>
        /// <param name="cont"></param>
        public AddPage(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
            //выводим в раскрывающийся список все элементы
            ModelBox.ItemsSource = context.Product.ToList();
            FIOBox.ItemsSource = context.Client.ToList();
            ManagerBox.ItemsSource = context.Manager.ToList();
            PayBox.ItemsSource = context.Purchase.Distinct().ToList();
            //Purchase a = context.Purchase.ToList().First();
            //string name = a.Client.FIO;
        }
        /// <summary>
        /// конструктор для редактирования.
        /// zak - заказ для редактирования
        /// </summary>
        /// <param name="cont"></param>
        /// <param name="zak"></param>
        public AddPage(Car_DealershipDBEntities cont, Purchase zak)
        {
            InitializeComponent();
            context = cont;
            zakazi = zak;
            //меняем надпись на кнопке
            buttonAU.Content = "Редактировать";
            //привязываем к кнопке другой обработчик нажатия
            buttonAU.Click += UpdateClick;
            //заполняем поля на форме
            ModelBox.ItemsSource = context.Product.ToList();
            FIOBox.ItemsSource = context.Client.ToList();
            ManagerBox.ItemsSource = context.Manager.ToList();
            PayBox.ItemsSource = context.Purchase.Distinct().ToList();
            ModelBox.SelectedItem = zakazi.Product;
            FIOBox.SelectedItem = zakazi.Client;
            ManagerBox.SelectedItem = zakazi.Manager;
            PayBox.SelectedItem = zakazi.PaymentType;
            DateBox.Text = zakazi.DatePuschase.ToString();
            DeliveryBox.Text = zakazi.Delivery == true ? "Да" : "Нет";
            PayBox.Text = zakazi.PaymentType;
        }

        /// <summary>
        /// Нажатие на кнопку "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Product prod = ModelBox.SelectedItem as Product;
                Client cl = FIOBox.SelectedItem as Client;
                Manager man = ManagerBox.SelectedItem as Manager;

                zakazi.CodeProduct = prod.Code;
                zakazi.Product = prod;
                zakazi.IdClient = cl.Id;
                zakazi.idManeger = man.Id;
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

        /// <summary>
        /// Нажатие на кнопку "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CanselClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Нажатие на кнопку "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AddZakazClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем выбранные элементы
                Product prod = ModelBox.SelectedItem as Product;
                Client cl = FIOBox.SelectedItem as Client;
                Manager man = ManagerBox.SelectedItem as Manager;
                //создаем заказ и заполняем его поля
                Purchase pur = new Purchase()
                {
                    Id = context.Purchase.ToList().Last().Id + 1,                // Id = context.Purchase.Count() + 1  - если то не сработает
                    CodeProduct = prod.Code,
                    Product = prod,
                    IdClient = cl.Id,
                    idManeger = man.Id,
                    DatePuschase = Convert.ToDateTime(DateBox.Text),
                    Delivery = DeliveryBox.Text.ToLower().Equals("да") ? true : false,
                    PaymentType = PayBox.Text,
                    Client = cl,
                    Manager = man
                };
                //добавляем заказ в БД
                context.Purchase.Add(pur);
                //сохранение изменений
                context.SaveChanges();
                //переход на страницу Заказы
                NavigationService.Navigate(new Zakazi(context));
            }
            catch (FormatException) //перейдет сюда, если исключение возникло на Convert.To...
            {
                MessageBox.Show("Ошибка вводимых данных!");
            }
            catch //перейдет сюда во всех остальных случаях
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}