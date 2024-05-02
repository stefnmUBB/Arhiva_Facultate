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

namespace ISS.Biblioteca.Commons.UI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
        }

        private void AbonatButton_Click(object sender, RoutedEventArgs e)
        {
            var a = new AbonatWindow();            

            a.Show();
        }

        private void BibliotecarButton_Click(object sender, RoutedEventArgs e)
        {
            new BibliotecarWindow().Show();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
        }
    }
}
