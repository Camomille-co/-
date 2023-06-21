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
            var categoryList = context.Purchase.ToList();
            categoryList.Insert(0, new Purchase() { PaymentType = "Все" });
            //categoryList.Insert(0, new Purchase() { Delivery = true && false });
            categoryBox.ItemsSource = categoryList;
            categoryBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Нажатие на кнопку + (добавить заказ)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AddClick(object sender, RoutedEventArgs e)
        {
            //вызываем страницу Добавление заказа
            NavigationService.Navigate(new AddPage(context));
        }

        /// <summary>
        /// клик по надписи "Редактировать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            //получаем заказ для редактирования
            Purchase zak = table.SelectedItem as Purchase;
            //вызываем форму "Добавить заказ", но передаем в нее объект для редактирования
            NavigationService.Navigate(new AddPage(context, zak));
        }

        /// <summary>
        /// Нажатие на гиперссылку "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            //Запрос пользователю, точно ли он хочет удалить
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить данный заказ?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try //если в этом блоке произойдет исключение, программа не вылетит, а перейдет в блок catch
                {
                    //получаем заказ, по которому лкикнули "Удалить"
                    Purchase puri = table.SelectedItem as Purchase;
                    //удаляем
                    context.Purchase.Remove(puri);
                    //сохраняем изменения
                    context.SaveChanges();
                    //снова выводим список в таблицу
                    table.ItemsSource = context.Purchase.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
        }

        void FilterData()
        {
            var list = context.Purchase.ToList();
            if (categoryBox.SelectedIndex != 0)
            {
                Purchase pur = categoryBox.SelectedItem as Purchase;
                list = list.Where(x => x.PaymentType == pur.PaymentType).ToList();
            }
            if (!string.IsNullOrWhiteSpace(poiskBox.Text))
            {
                list = list.Where(x => x.Client.FIO.ToLower().Contains(poiskBox.Text.ToLower())).ToList();
            }
            table.ItemsSource = list;
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
