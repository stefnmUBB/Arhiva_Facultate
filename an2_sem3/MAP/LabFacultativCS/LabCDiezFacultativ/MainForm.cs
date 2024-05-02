using LabCDiezFacultativ.Domain;
using LabCDiezFacultativ.Repo;
using LabCDiezFacultativ.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabCDiezFacultativ
{
    public partial class MainForm : Form
    {
        NBAService NBAService = NBAService.DefaultInstance;

        class JucatorActiveNumeCell : DataGridViewTextBoxCell
        {
            MainForm Form = null;

            public JucatorActiveNumeCell() { }
            public JucatorActiveNumeCell(MainForm form) { Form = form; }
            protected override object GetValue(int rowIndex)
            {
                var col = ((DataGridView.DataSource as BindingSource).DataSource as IEnumerable<JucatorActiv>).ToList();

                JucatorActiv ja = col[rowIndex];
                if (ja == null) return "<None>";                

                Jucator j = Form.NBAService.JucatorServ.GetById(ja.IdJucator);
                return j == null ? "<Null>" : j.Nume;
            }

            public override object Clone()
            {
                var cloned = base.Clone();
                ((JucatorActiveNumeCell)cloned).Form = Form;
                return cloned;
            }
        }

        class JucatorActiveNumeColumn : DataGridViewTextBoxColumn
        {
            public JucatorActiveNumeColumn() { }
            public JucatorActiveNumeColumn(MainForm form)
            {
                HeaderText = "Nume";
                CellTemplate = new JucatorActiveNumeCell(form);
            }

            public override object Clone()
            {
                var cloned = base.Clone();
                ((JucatorActiveNumeColumn)cloned).CellTemplate = CellTemplate;
                return cloned;
            }
        }

        class MeciEchipaCell : DataGridViewTextBoxCell
        {
            MainForm Form = null;
            int Index = 0;

            public MeciEchipaCell() { }
            public MeciEchipaCell(MainForm form, int index) { Form = form; Index = index;  }
            protected override object GetValue(int rowIndex)
            {
                var col = ((DataGridView.DataSource as BindingSource).DataSource as IEnumerable<Meci>).ToList();

                Meci m = col[rowIndex];
                if (m == null) return "<None>";

                Echipa e = Form.NBAService.EchipaServ.GetById(m.IdEchipa[Index]);                
                return e == null ? "<Null>" : e.Nume;
            }

            public override object Clone()
            {
                var cloned = base.Clone();
                ((MeciEchipaCell)cloned).Form = Form;
                ((MeciEchipaCell)cloned).Index = Index;
                return cloned;
            }
        }

        class MeciEchipaColumn : DataGridViewTextBoxColumn
        {
            public MeciEchipaColumn() { }
            public MeciEchipaColumn(MainForm form, int index)
            {
                HeaderText = $"Echipa {index + 1}";
                CellTemplate = new MeciEchipaCell(form, index);
            }

            public override object Clone()
            {
                var cloned = base.Clone();
                ((MeciEchipaColumn)cloned).CellTemplate = CellTemplate;
                return cloned;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            T1EchipaBox.Items.AddRange(NBAService.Echipe.ToArray());
            T1EchipaBox.DisplayMember = "Nume";
            T1EchipaBox.SelectedIndex = 0;

            T2Echipa1Box.Items.AddRange(NBAService.Echipe.ToArray());
            T2Echipa1Box.DisplayMember = "Nume";            

            T2Echipa2Box.Items.AddRange(NBAService.Echipe.ToArray());
            T2Echipa2Box.DisplayMember = "Nume";            

            T2Echipa1Box.SelectedIndex = 0;
            T2Echipa2Box.SelectedIndex = 0;

            T1JucatoriView.AutoGenerateColumns = false;

            T1JucatoriView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName="Nume",
                HeaderText="Nume"
            });
            

            T1JucatoriView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Scoala",
                HeaderText = "Scoala"
            });

            T2Echipa1View.AutoGenerateColumns = false;
            T2Echipa1View.Columns.Add(new JucatorActiveNumeColumn(this));
            T2Echipa1View.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NrPuncteInscrise",
                HeaderText = "Nr Puncte Inscrise"
            });
            T2Echipa1View.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tip",
                HeaderText = "Tip"
            });

            T2Echipa2View.AutoGenerateColumns = false;
            T2Echipa2View.Columns.Add(new JucatorActiveNumeColumn(this));
            T2Echipa2View.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NrPuncteInscrise",
                HeaderText = "Nr Puncte Inscrise"
            });
            T2Echipa2View.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tip",
                HeaderText = "Tip"
            });

            T3MeciuriView.Columns.Add(new MeciEchipaColumn(this, 0));
            T3MeciuriView.Columns.Add(new MeciEchipaColumn(this, 1));

            T3MeciuriView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Data",
                HeaderText = "Data"
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {            

        }

        private void T1EchipaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Echipa echipa = T1EchipaBox.SelectedItem as Echipa;

            BindingSource bs = new BindingSource();            
            bs.DataSource = NBAService.GetJucatori(echipa);
            T1JucatoriView.AutoGenerateColumns = false;
            T1JucatoriView.DataSource = bs;
        }

        bool selecting = false;
        

        void T2UpdateTables()
        {
            Echipa echipa1 = T2Echipa1Box.SelectedItem as Echipa;
            Echipa echipa2 = T2Echipa2Box.SelectedItem as Echipa;

            if (echipa1 == null)
                MessageBox.Show("1");
            if (echipa2 == null)
                MessageBox.Show("2");

            if (echipa1 == null || echipa2 == null)
                return;            

            Meci meci = NBAService.GetMeci(echipa1, echipa2);

            BindingSource bs1 = new BindingSource();
            bs1.DataSource = NBAService.GetJucatoriActivi(echipa1, meci);
            T2Echipa1View.AutoGenerateColumns = false;
            T2Echipa1View.DataSource = bs1;

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = NBAService.GetJucatoriActivi(echipa2, meci);
            T2Echipa2View.AutoGenerateColumns = false;
            T2Echipa2View.DataSource = bs2;

            var scores = NBAService.GetScore(meci).Select(kv => new KeyValuePair<long, int>(kv.Key.Id, kv.Value))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
            ScoreLabel.Text = $"{scores[echipa1.Id]}-{scores[echipa2.Id]}";

            T3Timeline.StartDate = new DateTime(2023, 1, 1);
            T3Timeline.EndDate = new DateTime(2023, 1, 15);

        }

        private void T2Echipa1Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            var echipa = T2Echipa1Box.SelectedItem as Echipa;
            if (!selecting)
            {
                selecting = true;                
                T2Echipa2Box.Items.Clear();
                T2Echipa2Box.Items.AddRange(NBAService.Echipe
                    .Where(ec => ec.Nume != echipa.Nume)
                    .ToArray());
                T2Echipa2Box.SelectedIndex = 0;
                selecting = false;
            }

            T2UpdateTables();

            
        }

        private void T2Echipa2Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            var echipa = T2Echipa2Box.SelectedItem as Echipa;
            if (!selecting)
            {
                selecting = true;

                bool change = T2Echipa1Box.SelectedItem != null && (T2Echipa1Box.SelectedItem as Echipa).Nume == echipa.Nume;

                if (change)
                {
                    T2Echipa1Box.Items.Clear();
                    T2Echipa1Box.Items.AddRange(NBAService.Echipe
                        .Where(ec => ec.Nume != echipa.Nume)
                        .ToArray());
                    T2Echipa1Box.SelectedIndex = 0;
                }
                selecting = false;
            }            

            T2UpdateTables();
        }

        private void T3Timeline_SelectedIntervalChanged(object sender)
        {
            var meciuri = NBAService.GetMeciuri(T3Timeline.SelectedStartDate, T3Timeline.SelectedEndDate)
                .ToList();
            meciuri.Sort((m1, m2) => Math.Sign((m1.Data - m2.Data).Ticks));


            BindingSource bs = new BindingSource();
            bs.DataSource = meciuri;
            T3MeciuriView.AutoGenerateColumns = false;
            T3MeciuriView.DataSource = bs;            

        }
    }
}
