using ISS.Biblioteca.Client.DTO;
using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            BooksList.Items.Add(null);
            BooksList.Items.Add(null);
            BooksList.Items.Add(null);
            BooksList.Items.Add(null);
            BooksList.Items.Add(null);            
        }

        IClientObserver ClientObserver;        

        public AdminWindow(IClientObserver observer)
        {
            InitializeComponent();
            ClientObserver = observer;
            BooksList.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new BookEditorWindow().Show();
        }

        public static List<string> UserRoles { get; } = new List<string> { "Admin", "Abonat", "Bibliotecar" };


        public ObservableCollection<CarteItem> Carti { get; } = new ObservableCollection<CarteItem>();
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in App.Server.GetCarti()) 
            {
                Carti.Add(new CarteItem(item.Carte, item.NrExemplare));
            }
        }

        private Dictionary<string, Type> UserType = new Dictionary<string, Type>
        {
            { "Admin"      , typeof(AdministratorIT) },
            { "Abonat"     , typeof(Abonat) },
            { "Bibliotecar", typeof(Bibliotecar) }
        };

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userType = UserType[UserTypeBox.SelectedValue as string];
                var utilizator = Activator.CreateInstance(userType) as Utilizator;
                utilizator.Nume = NumeBox.Text;
                utilizator.Cnp = CnpBox.Text;
                utilizator.Adresa = AdresaBox.Text;
                utilizator.Telefon = TelefonBox.Text;
                utilizator = App.Server.Register(utilizator);
                PasswordBox.Text = $"Parola generata : {utilizator.TokenLogare}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }
    }
}
