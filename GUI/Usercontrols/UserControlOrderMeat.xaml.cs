using Repository;
using BIZ;
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
    /// Interaction logic for UserControlOrderMeat.xaml
    /// </summary>
    public partial class UserControlOrderMeat : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;
        UserControlOrderMeatEdit UCEdit;

        public UserControlOrderMeat(ClassBIZ inBIZ, Grid inGrid, UserControlOrderMeatEdit inEdit)
        {
            InitializeComponent();
            BIZ = inBIZ;
            homeGrid = inGrid;
            UCEdit = inEdit;
        }

        private void buttonEditMeat_Click(object sender, RoutedEventArgs e)
        {
            // Create new editListMeat
            BIZ.editListMeat = new List<ClassMeat>();
            // Add 6 empty elements
            for (int i = 0; i < 6; i++) BIZ.editListMeat.Add(new ClassMeat());

            homeGrid.Children.Add(UCEdit);
            BIZ.isEnabled = false;
        }

        private void buttonSellToCustomer_Click(object sender, RoutedEventArgs e)
        {
            BIZ.SaveSaleInDB();
        }
    }
}
