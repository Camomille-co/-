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
        //вставить в Zakazi.xaml:
        //<ComboBox SelectionChanged="ChangeCategory" Name="categoryBox" Grid.Row="1" Grid.Column="1" Height="20" Width="350" HorizontalAlignment="Right" VerticalAlignment="Bottom" Margin="10">
        
        //<ComboBox SelectionChanged = "ChangeCategory" Name="categoryBox" Grid.Row="1" Grid.Column="1" Height="20" Width="350" HorizontalAlignment="Right" VerticalAlignment="Bottom" Margin="10">
        //    <ComboBox.ItemTemplate>
        //        <DataTemplate>
        //            <TextBlock>
        //                <Run Text = "{Binding PaymentType}" />
        //                < Run Text="{Binding Delivery}"/>
        //            </TextBlock>
        //        </DataTemplate>
        //    </ComboBox.ItemTemplate>
        //</ComboBox>

        private void zakazClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Zakazi(context));
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage(context));
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            Purchase zak = table.SelectedItem as Purchase;
            NavigationService.Navigate(new AddPage(context, zak));
        }

        //удаление
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить данный заказ?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Purchase puri = table.SelectedItem as Purchase;
                    context.Purchase.Remove(puri);
                    context.SaveChanges();
                    //table.ItemsSource = context.Purchase.ToList();
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
                //list = list.Where(x => x.Delivery == pur.Delivery && x.PaymentType == pur.PaymentType).ToList();
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
