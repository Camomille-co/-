using System;
using System.Collections.Generic;
using System.IO;
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

namespace Практика
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Car_DealershipDBEntities context;

        public MainWindow()
        {
            InitializeComponent();
            //DownloadPictures();
            context = new Car_DealershipDBEntities();
            myFrame.Navigate(new Pages.Authorization(context, this));
        }

        //public void DownloadPictures()
        //{
        //    using (Car_DealershipDBEntities context = new Car_DealershipDBEntities())
        //    {
        //        foreach (var item in context.Product.ToList())
        //        {
        //            item.Image = File.ReadAllBytes($"C:/Users/admin/Music/{item.Code}.jpg");
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }
}
