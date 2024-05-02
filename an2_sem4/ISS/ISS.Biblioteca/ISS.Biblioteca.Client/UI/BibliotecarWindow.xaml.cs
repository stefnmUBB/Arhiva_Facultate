using ISS.Biblioteca.Client;
using ISS.Biblioteca.Client.DTO;
using ISS.Biblioteca.Commons.Domain;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ISS.Biblioteca.Commons.UI
{
    /// <summary>
    /// Interaction logic for BibliotecarWindow.xaml
    /// </summary>
    public partial class BibliotecarWindow : Window
    {
        public BibliotecarWindow()
        {
            InitializeComponent();
            BooksView.Items.Add(null);
            BooksView.Items.Add(null);
            BooksView.Items.Add(null);
            BooksView.Items.Add(null);
        }
        private ClientObserver ClientObserver;
        private Bibliotecar Bibliotecar;

        public ObservableCollection<ExemplarItem> CartiImprumutate { get; } = new ObservableCollection<ExemplarItem>();

        public BibliotecarWindow(ClientObserver observer, Bibliotecar bibliotecar)
        {
            ClientObserver = observer;
            ClientObserver.ImprumutUpdate += ClientObserver_ImprumutUpdate;
            ClientObserver.ReturUpdate += ClientObserver_ReturUpdate;
            Bibliotecar = bibliotecar;
            InitializeComponent();
            BooksView.DataContext = this;
        }
                       

        private void ClientObserver_ReturUpdate(Retur retur)
        {            
            Console.WriteLine(retur.CarteReturnata?.ToString() ?? "NULLLL");

            var item = CartiImprumutate.ToList().Where(_ => _.Imprumut.CarteImprumutata.CodExemplar == retur.CarteReturnata.CodExemplar).FirstOrDefault();
            Console.WriteLine(item?.ToString() ?? "NULLLLLLLL");
            if (item != null)
            {                
                Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() => CartiImprumutate.Remove(item)));
            }            
        }

        private void ClientObserver_ImprumutUpdate(Imprumut imprumut)
        {
            if (CartiImprumutate.ToList().Any(_ => _.Imprumut.Abonat.CodAbonat == imprumut.Abonat.CodAbonat))
            {
                Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() => CartiImprumutate.Add(new ExemplarItem(imprumut))));                
            }
        }

        private void AbonatLoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CartiImprumutate.Clear();
                var abonat = App.Server.GetAbonatByCnp(CnpBox.Text);
                foreach(var imprumut in App.Server.GetImprumuturiByAbonat(abonat))
                {
                    CartiImprumutate.Add(new ExemplarItem(imprumut));
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReturButton_Click(object sender, RoutedEventArgs e)
        {
            var imprumut = ((sender as Button).DataContext as ExemplarItem).Imprumut;
            try
            {
                App.Server.EfectueazaRetur(Bibliotecar, imprumut);                                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Server.Logout(Bibliotecar);
        }
    }
}
