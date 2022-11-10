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

        private void buttonNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            BIZ.editOrNewCustomer = new ClassCustomer();
            homeGrid.Children.Add(UCEdit);
            BIZ.isEnabled = false;
        }

        private void buttonEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (BIZ.selectedCustomer.id != 0)
            {
                BIZ.editOrNewCustomer = new ClassCustomer(BIZ.selectedCustomer);

                // A: The customers country isn't correctly selected for whatever reason
                BIZ.editOrNewCustomer.country = BIZ.listCountry.Find(t => t.Id == BIZ.editOrNewCustomer.country.Id);

                homeGrid.Children.Add(UCEdit);
                BIZ.isEnabled = false;
            }
            else
            {
                // A: Message, Window title, Buttons available, Icon
                MessageBox.Show("Du skal vælge en kunde før du kan redigere.", "Manglende valg", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
