using ISS.Biblioteca.Client;
using ISS.Biblioteca.Commons.Domain;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var cnp = CnpBox.Text;
            var token = TokenBox.Text;
            var clientObserver = new ClientObserver();
            Utilizator utilizator;
            try
            {
                utilizator = App.Server.Login(cnp, token, clientObserver);                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }          
            
            if(utilizator is AdministratorIT)
            {
                var window = new AdminWindow(clientObserver);
                window.Show();
                this.Close();
            }
            else if (utilizator is Abonat abonat) 
            {
                var window = new AbonatWindow(clientObserver, abonat);
                window.Show();
                this.Close();
            }
            else if(utilizator is Bibliotecar bibliotecar)
            {
                var window = new BibliotecarWindow(clientObserver, bibliotecar);
                window.Show();
                this.Close();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
