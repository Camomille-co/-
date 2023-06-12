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
        public Data(Car_DealershipDBEntities cont)
        {
            InitializeComponent();
            context = cont;
        }

        private void SeeClick(object sender, RoutedEventArgs e)
        {
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
