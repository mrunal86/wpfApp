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

namespace MVVM_demo
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Admin_Login : Window
    {
        LoginLinqClassDataContext login_class = new LoginLinqClassDataContext();
        public Admin_Login()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var query = from login_var in login_class.Logins
                    where login_var.Username == txtUsername.Text & login_var.Password== txtPassword.Password
                    select login_var;
            int count = query.Count();
            if (count == 1)
            {
               
                MainWindow m = new MainWindow();
                m.Show();
                Close();
            }
            else
                MessageBox.Show("Username and Password is invalid");
            txtUsername.Text = "";
            txtPassword.Password = "";
        }
    }
}
