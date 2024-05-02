using FestivalSellpoint.Domain;
using FestivalSellpoint.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FestivalSellpoint.UI.Forms
{
    public partial class SpectacoleViewForm : AppForm
    {
        Angajat Angajat;

        public SpectacoleViewForm(IAppService appService) : base(appService)
        {
            Login();
            InitializeComponent();

            SpectacoleBinding = new BindingList<Spectacol>(Spectacole);
            SpectacoleView.DataSource = SpectacoleBinding;

            FilteredSpectacoleBinding = new BindingList<FilteredSpectacol>(FilteredSpectacole);
            FilteredSpectacoleView.DataSource = FilteredSpectacoleBinding;

            ShowSpectacole(AppService.GetAllSpectacole());
            DatePicker.Value = DateTime.Now;
        }

        void Login()
        {
            var loginForm = new AngajatLoginForm(AppService);
            if(loginForm.ShowDialog()==DialogResult.OK)
            {
                Angajat = loginForm.ConnectedAngajat;
            }
        }        

        private void DatePicker_ValueChanged(object sender, System.EventArgs e)
        {
            ShowFilteredSpectacole(AppService.FilterSpectacole(DatePicker.Value));
        }

        List<Spectacol> Spectacole = new List<Spectacol>();
        BindingList<Spectacol> SpectacoleBinding;

        List<FilteredSpectacol> FilteredSpectacole = new List<FilteredSpectacol>();
        BindingList<FilteredSpectacol> FilteredSpectacoleBinding;

        private void SpectacoleView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = SpectacoleView.Rows[e.RowIndex];            
            int nrLocuriDisponibile = Convert.ToInt32(row.Cells["nrLocuriDisponibile"].Value);
            if (nrLocuriDisponibile == 0)
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.ForeColor = Color.White;
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        private void SpectacoleView_SelectionChanged(object sender, EventArgs e)
        {
            if (SpectacoleView.SelectedCells.Count == 1) 
            {
                SpectacoleView.Rows[SpectacoleView.SelectedCells[0].RowIndex].Selected = true;                
            }
            else if(SpectacoleView.SelectedRows.Count==1)
            {
                
            }
        }

        Spectacol SelectedSpectacol
        {
            get
            {
                if (FilteredSpectacoleView.SelectedRows.Count == 0) 
                    return null;
                return (FilteredSpectacoleView.SelectedRows[0].DataBoundItem as FilteredSpectacol).Spectacol;
            }
        }

        private void ReserveButton_Click(object sender, EventArgs e)
        {
            var name = NumeCumparatorBox.Text;
            var seats = (int)NrLocuriDoriteBox.Value;
            var spectacol = SelectedSpectacol;

            if (spectacol == null)
            {
                MessageBox.Show("Please select a Spectacol");
                return;
            }

            if (name=="")
            {
                MessageBox.Show("Name cannot be empty");
                return;
            }

            if(seats==0)
            {
                MessageBox.Show("Invalid seats count");
                return;
            }            

            try
            {
                AppService.ReserveBilet(spectacol, name, seats);
                MessageBox.Show("Success");
                ShowSpectacole(AppService.GetAllSpectacole());
                ShowFilteredSpectacole(AppService.FilterSpectacole(DatePicker.Value));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);               
            }
        }

        void ShowSpectacole(IEnumerable<Spectacol> spectacole)
        {
            Spectacole.Clear();
            Spectacole.AddRange(spectacole);
            SpectacoleBinding.ResetBindings();
        }

        void ShowFilteredSpectacole(IEnumerable<Spectacol> spectacole)
        {
            FilteredSpectacole.Clear();
            FilteredSpectacole.AddRange(spectacole.Select(s => new FilteredSpectacol(s)));
            FilteredSpectacoleBinding.ResetBindings();
        }

        private void FilteredSpectacoleView_SelectionChanged(object sender, EventArgs e)
        {
            if (FilteredSpectacoleView.SelectedCells.Count == 1)
            {
                FilteredSpectacoleView.Rows[FilteredSpectacoleView.SelectedCells[0].RowIndex].Selected = true;
            }
            else if (FilteredSpectacoleView.SelectedRows.Count == 1) 
            {

            }
        }

        private void FilteredSpectacoleView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = FilteredSpectacoleView.Rows[e.RowIndex];
            int nrLocuriDisponibile = Convert.ToInt32(row.Cells["nrLocuriDisponibile"].Value);
            if (nrLocuriDisponibile == 0)
            {
                e.CellStyle.BackColor = Color.Red;
                e.CellStyle.ForeColor = Color.White;
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        class FilteredSpectacol
        {
            public FilteredSpectacol(Spectacol spectacol)
            {
                Spectacol = spectacol;
            }

            [Browsable(false)]
            public Spectacol Spectacol { get; }

            public string Artist => Spectacol.Artist;

            public string Locatie => Spectacol.Locatie;
            public string Ora => Spectacol.Data.ToString("HH:mm");

            public int NrLocuriDisponibile => Spectacol.NrLocuriDisponibile;
            public int NrLocuriVandute => Spectacol.NrLocuriVandute;

        }        
    }
}
