using ISS.Biblioteca.Client;
using ISS.Biblioteca.Client.DTO;
using ISS.Biblioteca.Commons.Domain;
using ISS.Biblioteca.Commons.Networking.Requests;
using ISS.Biblioteca.Commons.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ISS.Biblioteca.Commons.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AbonatWindow : Window
    {
        public AbonatWindow()
        {
            InitializeComponent();          
        }

        private ClientObserver ClientObserver;

        private Abonat Abonat;

        public AbonatWindow(ClientObserver observer, Abonat abonat)
        {
            Abonat = abonat;
            ClientObserver = observer;
            ClientObserver.ImprumutUpdate += ClientObserver_ImprumutUpdate;
            ClientObserver.ReturUpdate += ClientObserver_ReturUpdate;
            InitializeComponent();
            BooksList.DataContext = this;
            WishListView.DataContext = this;
        }

        private void ClientObserver_ReturUpdate(Retur retur)
        {
            var carte = retur.CarteReturnata.Carte;            
            var item = CartiDisponibile.Where(_ => _.Carte.CodCarte == carte.CodCarte).FirstOrDefault();
            if (item == null)            
                return;
            item.NrExemplare++;
            CollectionViewSource.GetDefaultView(CartiDisponibile).Refresh();            
        }

        private void ClientObserver_ImprumutUpdate(Imprumut imprumut)
        {
            var carte = imprumut.CarteImprumutata.Carte;
            var item = CartiDisponibile.Where(_ => _.Carte.CodCarte == carte.CodCarte).FirstOrDefault();
            if (item == null)                         
                return;            
            item.NrExemplare--;
            Console.WriteLine("Here");
            CollectionViewSource.GetDefaultView(BooksList.ItemsSource).Refresh();
        }

        public ObservableCollection<CarteItem> CartiDisponibile { get; } = new ObservableCollection<CarteItem>();

        public WishList WishList { get; } = new WishList();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in App.Server.GetCartiDisponibile())
            {
                CartiDisponibile.Add(new CarteItem(item.Carte, item.NrExemplare));
            }
        }

        private void AddToWishListButton_Click(object sender, RoutedEventArgs e)
        {
            var carteItem = (sender as Button).DataContext as CarteItem;            
            WishList.AddExemplar(carteItem.Carte);
        }

        private void ImprumutaButton_Click(object sender, RoutedEventArgs e)
        {
            if (WishList.BooksCount + App.Server.GetImprumuturiByAbonat(Abonat).Count() > 5) 
            {
                MessageBox.Show("Nu puteti avea mai mult de 5 carti imprumutate si nereturnate!");
                return;
            }

            List<string> errors = new List<string>();
            var list = WishList.ToList();
            
            ErrorBox.Text = "Please wait";
            this.IsEnabled = false;

            Task.Run(() =>
            {

                foreach (var item in list)
                {
                    for (int i = 0; i < item.NrExemplare; i++)
                    {
                        try
                        {
                            var exemplar = App.Server.GetDisponibleExemplarOf(item.Carte);
                            App.Server.EfectueazaImprumut(Abonat, exemplar);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex.Message);
                        }
                    }
                }                
                Dispatcher.Invoke(() =>
                {
                    if (errors.Count > 0)
                    {
                        ErrorBox.Text = "Some errors occured:\n" + string.Join("\n", errors);
                        WishList.Clear();
                    }
                    else
                    {
                        ErrorBox.Text = "Done.";
                        WishList.Clear();
                    }
                    this.IsEnabled = true;
                });
            });
        }

        private void ClearImprumutButton_Click(object sender, RoutedEventArgs e)
        {
            WishList.Clear();
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

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Server.Logout(Abonat);
        }
    }
}
