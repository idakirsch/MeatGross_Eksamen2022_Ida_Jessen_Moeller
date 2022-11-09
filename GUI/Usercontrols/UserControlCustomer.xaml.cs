using BIZ;
using Repository;
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

namespace GUI.Usercontrols
{
    /// <summary>
    /// Interaction logic for UserControlCustomer.xaml
    /// </summary>
    public partial class UserControlCustomer : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;
        UserControlCustomerEdit UCEdit;

        public UserControlCustomer(ClassBIZ inBIZ, Grid inGrid, UserControlCustomerEdit inEdit)
        {
            InitializeComponent();
            BIZ = inBIZ;
            homeGrid = inGrid;
            UCEdit = inEdit;
        }

        private void Button_Create_Customer_Click(object sender, RoutedEventArgs e)
        {
            BIZ.editOrNewCustomer = new ClassCustomer();
            homeGrid.Children.Add(UCEdit);
            BIZ.isEnabled = false;
        }

        private void Button_Edit_Customer_Click(object sender, RoutedEventArgs e)
        {
            BIZ.editOrNewCustomer = new ClassCustomer(BIZ.selectedCustomer);
            homeGrid.Children.Add(UCEdit);
            BIZ.isEnabled = false;
        }
    }
}
