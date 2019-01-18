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

namespace MVVM_demo
{
    /// <summary>
    /// Interaction logic for Product_Detail.xaml
    /// </summary>
    public partial class Product_Detail : Page
    {
        ProductLinqClassesDataContext p_linq = new ProductLinqClassesDataContext(Properties.Settings.Default.Stock_ManagementConnectionString);
        Product p = new Product();
        int id;
        public Product_Detail()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DG_Product.ItemsSource = p_linq.Products;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (ProductLinqClassesDataContext p_linq = new ProductLinqClassesDataContext())
            {
                p.Product_Name = PName.Text;
                p.Product_Description = PDescription.Text;
                p.Product_Status = PStatus.Text;

                p_linq.Products.InsertOnSubmit(p);
                p_linq.SubmitChanges();
                DG_Product.ItemsSource = p_linq.Products.ToList();

                PName.Text = PDescription.Text = PStatus.Text = "";
            }
        }       
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DG_Product.CurrentCell.Item != null)
            {
               using (ProductLinqClassesDataContext p_linq = new ProductLinqClassesDataContext())
                {
                    Product classObj = DG_Product.SelectedItem as Product;
                    id = classObj.Product_Id;
                    p = p_linq.Products.Where(w => w.Product_Id == id).FirstOrDefault();
                    p.Product_Name = PName.Text;
                    p.Product_Description = PDescription.Text;
                    p.Product_Status = PStatus.Text;
                    p_linq.SubmitChanges();

                    DG_Product.ItemsSource = p_linq.Products.ToList();

                }
           }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //ProductLinqClassesDataContext p_linq = new ProductLinqClassesDataContext();
            Product classObj = DG_Product.SelectedItem as Product;
            id = classObj.Product_Id;
            p = p_linq.Products.Where(w => w.Product_Id == id).FirstOrDefault();
            p_linq.Products.DeleteOnSubmit(p);
            p_linq.SubmitChanges();
            DG_Product.ItemsSource = p_linq.Products.ToList();
            if(id<=DG_Product.Items.Count-1)
                {
                  DG_Product.SelectedIndex = id;
                }
        }
        private void DG_Product_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //index = DG_Product.Items.IndexOf(DG_Product.CurrentItem) + 1;
            Product classObj = DG_Product.SelectedItem as Product;
            int id = classObj.Product_Id;
            var query = from m in p_linq.Products
                        where m.Product_Id == id
                        select m;
            foreach(var q in query)
            {
                PName.Text = q.Product_Name;
                PDescription.Text = q.Product_Description;
                PStatus.Text = q.Product_Status;
            }
       }
    }
}
    

