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
    /// Interaction logic for UserControlOrderMeatEdit.xaml
    /// </summary>
    public partial class UserControlOrderMeatEdit : UserControl
    {
        ClassBIZ BIZ;
        Grid homeGrid;

        public UserControlOrderMeatEdit(ClassBIZ inBIZ, Grid inGrid)
        {
            InitializeComponent();
            BIZ = inBIZ;
            homeGrid = inGrid;
        }

        private void buttonExitUpdate_Click(object sender, RoutedEventArgs e)
        {
            homeGrid.Children.Remove(this);
            BIZ.isEnabled = true;
        }

        private void SaveGris(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNewMeatPrice(0);
        }

        private void SaveKalv(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNewMeatPrice(1);
        }

        private void SaveOkse(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNewMeatPrice(2);
        }

        private void SaveKylling(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNewMeatPrice(3);
        }

        private void SaveKalkun(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNewMeatPrice(4);
        }

        private void SaveHest(object sender, RoutedEventArgs e)
        {
            BIZ.SaveNewMeatPrice(5);
        }
    }
}
