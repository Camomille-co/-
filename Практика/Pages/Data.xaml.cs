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
    /// Логика взаимодействия для Data.xaml
    /// </summary>
    public partial class Data : Window
    {
        Car_DealershipDBEntities context;
        //User _user;
        public Data(Car_DealershipDBEntities cont)
        //public Data(Car_DealershipDBEntities cont, User user)
        {
            InitializeComponent();
            context = cont;
            //_user = user;
        }

        private void SeeClick(object sender, RoutedEventArgs e)
        {
            //if (_user.Login == loginBox.Text && _user.TabNumber == Convert.ToInt32(numBox.Text))
            //    MessageBox.Show($"Ваш пароль: {_user.Password}", "Пароль", MessageBoxButton.OK, MessageBoxImage.Information);

            string log = loginBox.Text;
            int num = Convert.ToInt32(numBox.Text);
            User user = context.User.Find(log);
            if (user != null)
            {
                if (user.TabNumber == num)
                {
                    MessageBox.Show(user.Password, "Пароль");
                }
                else
                {
                    MessageBox.Show("Вы ввели неправильный номер!!!");
                }
            }
            this.Close();
        }
    }
}
