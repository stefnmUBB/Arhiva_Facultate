using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SGBD.ExamenPractic
{
    public partial class MainForm : Form
    {
        SqlConnection Connection = new SqlConnection(@"Data Source=DESKTOP-9HMTO67\SQLEXPRESS;Initial Catalog=S5;Integrated Security=True");
        SqlDataAdapter ParentAdapter = new SqlDataAdapter();
        SqlDataAdapter ChildAdapter = new SqlDataAdapter();
        DataSet DataSet = new DataSet();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ParentAdapter.SelectCommand = new SqlCommand("SELECT * FROM TipuriArticole", Connection);
            ParentAdapter.Fill(DataSet, "TipuriArticole");
            TipuriArticoleView.DataSource = DataSet.Tables["TipuriArticole"];
            TipuriArticoleView.Rows[0].Selected = true;
        }

        private void TipuriArticoleView_SelectionChanged(object sender, EventArgs e)
        {
            if (TipuriArticoleView.SelectedRows.Count == 0)
            {
                if (TipuriArticoleView.SelectedCells.Count > 0)
                    TipuriArticoleView.Rows[TipuriArticoleView.SelectedCells[0].RowIndex].Selected = true;                
                return;
            }
            int id = (int)TipuriArticoleView.SelectedRows[0].Cells["Tid"].Value;
            SelTipArticoleId = id;

            if (DataSet.Tables.Contains("Articole"))
                DataSet.Tables["Articole"].Clear();
            ChildAdapter.SelectCommand = new SqlCommand("SELECT * FROM Articole where Tid = @tid", Connection);
            ChildAdapter.SelectCommand.Parameters.AddWithValue("@tid", id);
            ChildAdapter.Fill(DataSet, "Articole");
            ArticoleView.DataSource = DataSet.Tables["Articole"];
            if (ArticoleView.Rows.Count > 0)
                ArticoleView.Rows[0].Selected = true;
        }


        int SelTipArticoleId;
        int SelArticolId;

        void RefreshArticole()
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
                if (DataSet.Tables.Contains("Briose"))
                    DataSet.Tables["Briose"].Clear();
                ChildAdapter.Fill(DataSet, "Briose");
                ArticoleView.DataSource = DataSet.Tables["Briose"];
                if (ArticoleView.Rows.Count > 0)
                    ArticoleView.Rows[0].Selected = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { Connection.Close(); }
        }

        private void ArticoleView_SelectionChanged(object sender, EventArgs e)
        {
            if (ArticoleView.SelectedRows.Count == 0)
            {
                if (ArticoleView.SelectedCells.Count > 0)
                    ArticoleView.Rows[ArticoleView.SelectedCells[0].RowIndex].Selected = true;
                return;
            }
            SelArticolId = (int)ArticoleView.SelectedRows[0].Cells["Aid"].Value;

            TitluBox.Text = ArticoleView.SelectedRows[0].Cells["Titlu"].Value.ToString();
            NrAutoriBox.Value = decimal.Parse(ArticoleView.SelectedRows[0].Cells["NrAutori"].Value.ToString());
            NrPaginiBox.Value = decimal.Parse(ArticoleView.SelectedRows[0].Cells["NrPagini"].Value.ToString());
            AnPublicareBox.Value = decimal.Parse(ArticoleView.SelectedRows[0].Cells["AnPublicare"].Value.ToString());            
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var titlu = TitluBox.Text;
            var nrAutori = NrAutoriBox.Value;            
            var nrPagini = NrPaginiBox.Value;
            var anPublicare = AnPublicareBox.Value;

            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
                ChildAdapter.InsertCommand = new SqlCommand("INSERT INTO Articole (Titlu, NrAutori, NrPagini, AnPublicare, Tid)" +
                    " VALUES (@titlu, @nrA, @nrP, @anPub, @tid)", Connection);
                ChildAdapter.InsertCommand.Parameters.AddWithValue("@titlu", titlu);
                ChildAdapter.InsertCommand.Parameters.AddWithValue("@nrA", nrAutori);
                ChildAdapter.InsertCommand.Parameters.AddWithValue("@nrP", nrPagini);
                ChildAdapter.InsertCommand.Parameters.AddWithValue("@anPub", anPublicare);
                ChildAdapter.InsertCommand.Parameters.AddWithValue("@tid", SelTipArticoleId);
                int rows = ChildAdapter.InsertCommand.ExecuteNonQuery();

                RefreshArticole();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                Connection.Close();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var titlu = TitluBox.Text;
            var nrAutori = NrAutoriBox.Value;
            var nrPagini = NrPaginiBox.Value;
            var anPublicare = AnPublicareBox.Value;

            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
                ChildAdapter.UpdateCommand = new SqlCommand("UPDATE Articole SET Titlu=@titlu, " +
                    "NrAutori = @nrA, NrPagini = @nrP, AnPublicare=@anPub WHERE Aid = @aid", Connection);
                ChildAdapter.UpdateCommand.Parameters.AddWithValue("@titlu", titlu);
                ChildAdapter.UpdateCommand.Parameters.AddWithValue("@nrA", nrAutori);
                ChildAdapter.UpdateCommand.Parameters.AddWithValue("@nrP", nrPagini);
                ChildAdapter.UpdateCommand.Parameters.AddWithValue("@anPub", anPublicare);
                ChildAdapter.UpdateCommand.Parameters.AddWithValue("@aid", SelArticolId);
                int rows = ChildAdapter.UpdateCommand.ExecuteNonQuery();

                RefreshArticole();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                Connection.Close();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {                
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
                ChildAdapter.DeleteCommand = new SqlCommand("DELETE FROM Articole WHERE Aid = @aid", Connection);
                ChildAdapter.DeleteCommand.Parameters.AddWithValue("@aid", SelArticolId);
                int rows = ChildAdapter.DeleteCommand.ExecuteNonQuery();
                RefreshArticole();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                Connection.Close();
            }
        }
    }
}
