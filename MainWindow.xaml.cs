using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

 namespace MVVM_demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xamlAsWA
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Dictionary<string, string> _mdiChildren = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Home_Selected(object sender, RoutedEventArgs e)
        {
            
        }
        private void Product_Selected(object sender, RoutedEventArgs e)
        {
           MyGrid.Content = new Product_Detail();
        }

        private void Stock_Selected(object sender, RoutedEventArgs e)
        {
            MyGrid.Content = new Stock();
        }

 
        

        
    }
}
