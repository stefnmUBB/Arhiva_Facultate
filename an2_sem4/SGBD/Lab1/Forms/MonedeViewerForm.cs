using SGBD.DepozitColectie.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGBD.DepozitColectie.Forms
{
    public partial class MonedeViewerForm : Form
    {
        public MonedeViewerForm()
        {
            InitializeComponent();
            MakeDoubleBuffered(TipMonedeView);

            CladireSelector_DropDown(null, null);
        }

        SqlConnection Connection = new SqlConnection(Constants.ConnectionString);
        SqlDataAdapter DataAdapter = new SqlDataAdapter();
        DataSet DataSetTipMonede = new DataSet();
        DataSet DataSetExemplarMonede = new DataSet();
        DataSet DataSetCladiri= new DataSet();
        DataSet DataSetDepozite = new DataSet();

        BindingSource SourceExemplarMonede = new BindingSource();


        private void Connect()
        {
            LoadTipMonede();
        }

        void LoadTipMonede()
        {
            DataAdapter.SelectCommand = new SqlCommand(
                "SELECT TM.IdTipMoneda, NN.Valoare, NN.Valuta, TM.Orientare, TM.An FROM TipMonede TM " +
                "INNER JOIN NotaNumismatica NN ON TM.IdNotaNumismatica = TM.IdNotaNumismatica " +
                "INNER JOIN MaterialMoneda MM ON TM.IdMaterialMoneda = MM.IdMaterialMoneda",
                Connection);
            DataSetTipMonede.Clear();
            DataAdapter.Fill(DataSetTipMonede);
            TipMonedeView.DataSource = SourceExemplarMonede.DataSource = DataSetTipMonede.Tables[0];
            TipMonedeView.Rows[0].Selected = true;
        }

        void LoadExemplarMonede()
        {
            DataAdapter.SelectCommand = new SqlCommand(
                "SELECT EM.IdExemplarMoneda, EM.StareConservare, SD.IdDepozit, SD.Cladire, SD.Camera, SD.Raion, SD.Raft " +
                "FROM ExemplarMonede EM " +
                "INNER JOIN SpatiuDepozitare SD ON EM.IdDepozit = SD.IdDepozit " +
                "WHERE EM.IdTipMoneda = @id",
                Connection);
            DataAdapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = IdTipMoneda;


            DataSetExemplarMonede.Clear();
            DataAdapter.Fill(DataSetExemplarMonede);
            ExemplareMonedeView.DataSource = DataSetExemplarMonede.Tables[0];
            ExemplareMonedeView.Columns["IdDepozit"].Visible = false;

        }

        private void MonedeViewerForm_Load(object sender, EventArgs e)
        {
            Connect();
        }

        private void MakeDoubleBuffered(Control control)
        {
            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic 
                | System.Reflection.BindingFlags.Instance)
                .SetValue(control, true, null);
        }

        int IdTipMoneda;

        private void TipMonedeView_SelectionChanged(object sender, EventArgs e)
        {
            if (TipMonedeView.SelectedRows.Count == 0) return;
            int id = (int)TipMonedeView.SelectedRows[0].Cells[0].Value;
            SelectTipMoneda(id);
        }

        void SelectTipMoneda(int id)
        {
            IdTipMoneda = id;
            SelectedTipMonedaLabel.Text = $"#{IdTipMoneda}";
            LoadExemplarMonede();
        }        

        int IdExemplarMoneda = -1;
        void SelectExemplarMoneda(int id)
        {
            IdExemplarMoneda = id;
            if(id==-1)
            {
                UpdateButton.Enabled = false;
                DeleteButton.Enabled = false;
                return;
            }
            UpdateButton.Enabled = true;
            DeleteButton.Enabled = true;            
        }

        private void ExamplareMonedeView_SelectionChanged(object sender, EventArgs e)
        {
            if(ExemplareMonedeView.SelectedRows.Count==0)
            {
                SelectExemplarMoneda(-1);
                return;
            }           

            int id = (int)ExemplareMonedeView.SelectedRows[0].Cells[0].Value;
            int stare = (int)ExemplareMonedeView.SelectedRows[0].Cells[1].Value;
            string cladire = (string)ExemplareMonedeView.SelectedRows[0].Cells[3].Value;
            int camera = (int)ExemplareMonedeView.SelectedRows[0].Cells[4].Value;
            int raion = (int)ExemplareMonedeView.SelectedRows[0].Cells[5].Value;
            int raft = (int)ExemplareMonedeView.SelectedRows[0].Cells[6].Value;


            SelectExemplarMoneda(id);

            StareConversareSelector.Value = stare;            
            CladireSelector.SelectedValue = cladire;

            CameraBox.Value = camera;
            RaionBox.Value = raion;
            RaftBox.Value = raft;
        }

        private void CladireSelector_DropDown(object sender, EventArgs e)
        {
            DataAdapter.SelectCommand = new SqlCommand(
                "SELECT DISTINCT Cladire FROM SpatiuDepozitare",                
                Connection);

            DataSetCladiri.Clear();
            DataAdapter.Fill(DataSetCladiri);            


            CladireSelector.DataSource = DataSetCladiri.Tables[0];
            CladireSelector.DisplayMember = "Cladire";
            CladireSelector.ValueMember = "Cladire";
        }        

        int SelectDepozit(string cladire, int camera, int raion, int raft)
        {
            DataAdapter.SelectCommand = new SqlCommand(
                "SELECT IdDepozit FROM SpatiuDepozitare " +
                "WHERE Cladire = @cladire AND Camera = @camera " +
                "AND Raion = @raion AND Raft = @raft",
                Connection);
            DataAdapter.SelectCommand.Parameters.Add("@cladire", SqlDbType.VarChar).Value = cladire;
            DataAdapter.SelectCommand.Parameters.Add("@camera", SqlDbType.Int).Value = camera;
            DataAdapter.SelectCommand.Parameters.Add("@raion", SqlDbType.Int).Value = raion;
            DataAdapter.SelectCommand.Parameters.Add("@raft", SqlDbType.Int).Value = raft;

            DataSetDepozite.Clear();
            DataAdapter.Fill(DataSetDepozite);

            if (DataSetDepozite.Tables[0].Rows.Count == 0)
                return -1;
            return (int)DataSetDepozite.Tables[0].Rows[0].ItemArray[0];
        }

        int InsertDepozit(string cladire, int camera, int raion, int raft)
        {            
            try
            {
                Connection.Open();
                DataAdapter.InsertCommand = new SqlCommand(
                    "INSERT INTO SpatiuDepozitare (Cladire, Camera, Raion, Raft) " +
                    "VALUES (@cladire, @camera, @raion, @raft)",
                    Connection);

                DataAdapter.InsertCommand.Parameters.Add("@cladire", SqlDbType.VarChar).Value = cladire;
                DataAdapter.InsertCommand.Parameters.Add("@camera", SqlDbType.VarChar).Value = camera;
                DataAdapter.InsertCommand.Parameters.Add("@raion", SqlDbType.VarChar).Value = raion;
                DataAdapter.InsertCommand.Parameters.Add("@raft", SqlDbType.VarChar).Value = raft;
                DataAdapter.InsertCommand.ExecuteNonQuery();

                DataSetDepozite.Clear();
                DataAdapter.Fill(DataSetDepozite);                                
                return (int)DataSetDepozite.Tables[0].Rows[0].ItemArray[0];
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error with depozit:\n{e.Message}", "Error");
                return -1;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
        }

        int GetIdDepozit()
        {
            var cladire = CladireSelector.SelectedValue as string;

            if (cladire == null)
                cladire = CladireSelector.Text;

            int camera = (int)CameraBox.Value;
            int raion = (int)RaionBox.Value;
            int raft = (int)RaftBox.Value;
            int sel_id = SelectDepozit(cladire, camera, raion, raft);
            if (sel_id >= 0)
                return sel_id;

            return InsertDepozit(cladire, camera, raion, raft);            
        }                

        void AddExemplarMoneda()
        {
            int idDepozit = GetIdDepozit();
            int idTipMoneda = IdTipMoneda;
            int stareConservare = StareConversareSelector.Value;

            if (idDepozit < 0)
                return;

            try
            {
                Connection.Open();
                DataAdapter.InsertCommand = new SqlCommand(
                    "INSERT INTO ExemplarMonede (IdTipMoneda, StareConservare, IdDepozit) " +
                    "VALUES (@idtm, @sc, @iddep)",
                    Connection);

                DataAdapter.InsertCommand.Parameters.Add("@idtm", SqlDbType.VarChar).Value = idTipMoneda;
                DataAdapter.InsertCommand.Parameters.Add("@sc", SqlDbType.VarChar).Value = stareConservare;
                DataAdapter.InsertCommand.Parameters.Add("@iddep", SqlDbType.VarChar).Value = idDepozit;
                DataAdapter.InsertCommand.ExecuteNonQuery();


                //SourceExemplarMonede.ResetBindings(false);                
                //ExemplareMonedeView.DataSource = DataSetExemplarMonede.Tables[0];                

                MessageBox.Show("Successfully added exemplar moneda");

                LoadExemplarMonede();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");               
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
        }

        void DeleteExemplarMoneda()
        {
            try
            {
                Connection.Open();
                DataAdapter.DeleteCommand = new SqlCommand(
                    "DELETE FROM ExemplarMonede WHERE IdExemplarMoneda = @idem",
                    Connection);

                DataAdapter.DeleteCommand.Parameters.Add("@idem", SqlDbType.VarChar).Value = IdExemplarMoneda;
                DataAdapter.DeleteCommand.ExecuteNonQuery();

                MessageBox.Show("Successfully remved exemplar moneda");

                LoadExemplarMonede();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
        }

        void UpdateExemplarMoneda()
        {
            int idDepozit = GetIdDepozit();            
            int stareConservare = StareConversareSelector.Value;

            if (idDepozit < 0)
                return;

            try
            {
                Connection.Open();
                DataAdapter.UpdateCommand = new SqlCommand(
                    "UPDATE ExemplarMonede SET StareConservare = @sc, IdDepozit = @iddep " +
                    "WHERE IdExemplarMoneda = @idem",
                    Connection);

                DataAdapter.UpdateCommand.Parameters.Add("@idem", SqlDbType.VarChar).Value = IdExemplarMoneda;
                DataAdapter.UpdateCommand.Parameters.Add("@sc", SqlDbType.VarChar).Value = stareConservare;
                DataAdapter.UpdateCommand.Parameters.Add("@iddep", SqlDbType.VarChar).Value = idDepozit;
                DataAdapter.UpdateCommand.ExecuteNonQuery();


                //SourceExemplarMonede.ResetBindings(false);                
                //ExemplareMonedeView.DataSource = DataSetExemplarMonede.Tables[0];                

                MessageBox.Show("Successfully updated exemplar moneda");

                LoadExemplarMonede();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            finally
            {
                if (Connection.State == ConnectionState.Open) Connection.Close();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddExemplarMoneda();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteExemplarMoneda();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            UpdateExemplarMoneda();
        }
    }
}
