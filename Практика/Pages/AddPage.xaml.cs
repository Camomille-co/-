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

namespace ��������.Pages
{
    /// <summary>
    /// ��� ��� ��� ���������� � �������������� ���� � ��� �� ���������,
    /// ����� ������� ���� �������� ��� ���� ��������
    /// �� ����� ����������� ������������: ���� ��� ����������, ������ ��� ��������������
    /// ��� ������ ����� ����� ������� 2 ������ �������. 
    /// ������ ��� ���������� ������������� ����� ��������� (.xaml)
    /// ������ ��� �������������� ������������� � ��������������� ������������
    /// </summary>
    public partial class AddPage : Page
    {
        Car_DealershipDBEntities context;
        Purchase zakazi;

        /// <summary>
        /// ����������� ��� ���������� ��������
        /// </summary>
        /// <param name="cont"></param>
        public AddPage(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
            //������� � �������������� ������ ��� ��������
            ModelBox.ItemsSource = context.Product.ToList();
            FIOBox.ItemsSource = context.Client.ToList();
            ManagerBox.ItemsSource = context.Manager.ToList();
            PayBox.ItemsSource = context.Purchase.Distinct().ToList();
            //Purchase a = context.Purchase.ToList().First();
            //string name = a.Client.FIO;
        }
        /// <summary>
        /// ����������� ��� ��������������.
        /// zak - ����� ��� ��������������
        /// </summary>
        /// <param name="cont"></param>
        /// <param name="zak"></param>
        public AddPage(Car_DealershipDBEntities cont, Purchase zak)
        {
            InitializeComponent();
            context = cont;
            zakazi = zak;
            //������ ������� �� ������
            buttonAU.Content = "�������������";
            //����������� � ������ ������ ���������� �������
            buttonAU.Click += UpdateClick;
            //��������� ���� �� �����
            ModelBox.ItemsSource = context.Product.ToList();
            FIOBox.ItemsSource = context.Client.ToList();
            ManagerBox.ItemsSource = context.Manager.ToList();
            PayBox.ItemsSource = context.Purchase.Distinct().ToList();
            ModelBox.SelectedItem = zakazi.Product;
            FIOBox.SelectedItem = zakazi.Client;
            ManagerBox.SelectedItem = zakazi.Manager;
            PayBox.SelectedItem = zakazi.PaymentType;
            DateBox.Text = zakazi.DatePuschase.ToString();
            DeliveryBox.Text = zakazi.Delivery == true ? "��" : "���";
            PayBox.Text = zakazi.PaymentType;
        }

        /// <summary>
        /// ������� �� ������ "�������������"
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
                zakazi.Delivery = DeliveryBox.Text.ToLower().Equals("��") ? true : false;
                zakazi.PaymentType = PayBox.Text;
                zakazi.Client = cl;
                zakazi.Manager = man;
                context.SaveChanges();
                NavigationService.Navigate(new Zakazi(context));
            }
            catch
            {
                MessageBox.Show("������!");
            }

        }

        /// <summary>
        /// ������� �� ������ "������"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void CanselClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// ������� �� ������ "��������"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void AddZakazClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //�������� ��������� ��������
                Product prod = ModelBox.SelectedItem as Product;
                Client cl = FIOBox.SelectedItem as Client;
                Manager man = ManagerBox.SelectedItem as Manager;
                //������� ����� � ��������� ��� ����
                Purchase pur = new Purchase()
                {
                    Id = context.Purchase.ToList().Last().Id + 1,                // Id = context.Purchase.Count() + 1  - ���� �� �� ���������
                    CodeProduct = prod.Code,
                    Product = prod,
                    IdClient = cl.Id,
                    idManeger = man.Id,
                    DatePuschase = Convert.ToDateTime(DateBox.Text),
                    Delivery = DeliveryBox.Text.ToLower().Equals("��") ? true : false,
                    PaymentType = PayBox.Text,
                    Client = cl,
                    Manager = man
                };
                //��������� ����� � ��
                context.Purchase.Add(pur);
                //���������� ���������
                context.SaveChanges();
                //������� �� �������� ������
                NavigationService.Navigate(new Zakazi(context));
            }
            catch (FormatException) //�������� ����, ���� ���������� �������� �� Convert.To...
            {
                MessageBox.Show("������ �������� ������!");
            }
            catch //�������� ���� �� ���� ��������� �������
            {
                MessageBox.Show("������!");
            }
        }
    }
}