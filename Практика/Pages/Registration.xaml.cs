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
using System.Windows.Shapes;

namespace Практика.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Car_DealershipDBEntities context;
        public Registration(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            User user = new User() 
            { 
                Login = loginBox.Text, 
                Password = passwordBox.Text, 
                TabNumber = Convert.ToInt32(numberBox.Text), 
                FIO = fioBox.Text, 
                Post = postBox.Text 
            };
            context.User.Add(user);
            context.SaveChanges();
            this.Close();
        }
    }
}
