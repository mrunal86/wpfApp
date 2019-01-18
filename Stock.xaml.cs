using MVVM_demo;
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
using System.Text.RegularExpressions;

namespace MVVM_demo
{
    /// <summary>
    /// Interaction logic for Stock.xaml
    /// </summary>
    public partial class Stock : Page
    {
        ProductLinqClassesDataContext prod_context = new ProductLinqClassesDataContext();
          Stock_ stock_member = new  Stock_();
        public Stock()
        {
            InitializeComponent();
            Bind_Data();
        }
        ProductLinqClassesDataContext stock_context = new ProductLinqClassesDataContext();

        public List<Product> Combo_Product { get; set; }
        private void Bind_Data()
        {
            var query = from p_id in prod_context.Products
                        
                        select p_id.Product_Id;
//for more than 1 column  select new { Product_Id = p_id.Product_Id, Product_Name = p_id.Product_Name }).ToList();

            // Product_code.ItemsSource = query.ToLis
            foreach (var v in query)
            {
                Product_code.Items.Add(v);
              //  Product_name.Items.Add(v.Product_Name);
            }
        }

        private void auto_number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Stock_ classObj = DG_Stock.SelectedItem as Stock_;
            int id = classObj.Product_Id;
            var query = from m in stock_context.Stock_s
                        where m.Product_Id == id
                        select m;
            foreach (var q in query)
            {
                Product_code.Text = Convert.ToString(q.Product_Id);
                Product_name.Text = q.Product_Name;
                Transaction_date.SelectedDate = q.Trans_Date;
                auto_number.Text = Convert.ToString(q.Quantity);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DG_Stock.ItemsSource = stock_context.Stock_s.ToList();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (ProductLinqClassesDataContext p_linq = new ProductLinqClassesDataContext())
            {
                stock_member.Product_Id  = Convert.ToInt32(Product_code.Text);
                stock_member.Product_Name = Product_name.Text;
                stock_member.Trans_Date=Transaction_date.SelectedDate;
                stock_member.Quantity = Convert.ToInt32( auto_number.Text);

                p_linq.Stock_s.InsertOnSubmit(stock_member);
                p_linq.SubmitChanges();
                DG_Stock.ItemsSource = p_linq.Stock_s.ToList();

                Product_code.Text = Product_name.Text = auto_number.Text = "";
                Transaction_date.SelectedDate = System.DateTime.Now;
            }
        }
        int ID;
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Stock.CurrentCell.Item != null)
            {
                using (ProductLinqClassesDataContext p_linq = new ProductLinqClassesDataContext())
                {
                    Stock_ classObj = DG_Stock.SelectedItem as Stock_;
                    ID = classObj.Product_Id;
                    stock_member = p_linq.Stock_s.Where(w => w.Product_Id == ID).FirstOrDefault();
                    stock_member.Product_Name = Product_name.Text;
                    stock_member.Trans_Date = Transaction_date.SelectedDate;
                    stock_member.Quantity = Convert.ToInt32(auto_number.Text);
                    p_linq.SubmitChanges();

                    DG_Stock.ItemsSource = p_linq.Stock_s.ToList();
                    Product_name.Text = auto_number.Text= Product_code.Text="";
                    Transaction_date.SelectedDate = System.DateTime.Now;
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Stock_ classObj = DG_Stock.SelectedItem as Stock_;
            ID = classObj.Product_Id;
            stock_member = stock_context.Stock_s.Where(w => w.Product_Id == ID).FirstOrDefault();
            stock_context.Stock_s.DeleteOnSubmit(stock_member);
            stock_context.SubmitChanges();
            DG_Stock.ItemsSource = stock_context.Stock_s.ToList();
            if (ID <= DG_Stock.Items.Count - 1)
            {
                DG_Stock.SelectedIndex = ID;
            }
            Product_name.Text = auto_number.Text = Product_code.Text = "";
            Transaction_date.SelectedDate = System.DateTime.Now;
        
    }

        private void Product_code_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
            int Prod_code = Convert.ToInt32(Product_code.SelectedItem);
            var query2 = from p_id in prod_context.Products
                         where p_id.Product_Id == Prod_code
                        select p_id.Product_Name;
            foreach(var q2 in query2)
            { 
              Product_name.Text = q2;
            }
        }
    }
}
