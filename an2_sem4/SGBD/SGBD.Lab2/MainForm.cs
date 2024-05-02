using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGBD.Lab2
{
    public partial class MainForm : Form
    {
        SqlConnection Connection = new SqlConnection(Constants.ConnectionString);
        DbUtils DbUtils;

        Config Config;

        public string ParentTableName => Config.ParentTable;
        public string ChildTableName => Config.ChildTable;

        DataSet ParentDataSet = new DataSet();

        DataSet ChildDataSet = new DataSet();

        List<(string Name, string Type, int CharMaxLength)> ChildColumns;
        List<(string TableColumn, string ReferencedTable, string ReferencedColumn)> ForeignKeys;

        string ParentPrimaryKey;
        string ChildPrimaryKey;
        string ParentForeignKeyInChild;

        List<Control> DynamicInputs = new List<Control>();

        public MainForm(string configpath = @"Config\default.xml")
        {
            InitializeComponent();            

            DbUtils = new DbUtils(Connection);

            LoadConfig(configpath);
            LoadParentTable();            
        }

        void LoadConfig(string path)
        {
            Config = Config.FromXml(path);
            ForeignKeys = DbUtils.GetForeignKeys(ChildTableName).ToList();
            ChildColumns = DbUtils.GetColumns(ChildTableName).ToList();

            ParentPrimaryKey = DbUtils.GetPrimaryKeyColumn(ParentTableName);
            ChildPrimaryKey = DbUtils.GetPrimaryKeyColumn(ChildTableName);
            ParentForeignKeyInChild = ForeignKeys.Where(fk => fk.ReferencedTable == ParentTableName)
                .FirstOrDefault().TableColumn;
               
            Console.WriteLine(ChildPrimaryKey);
            ChildColumns.ForEach(x => Console.WriteLine(x));            
            ForeignKeys.ForEach(x => Console.WriteLine(x));


            PropsPanel.Controls.Clear();

            foreach(var col in ChildColumns)
            {
                if (col.Name == ChildPrimaryKey) continue;

                var fks = ForeignKeys.Where(f => f.TableColumn == col.Name).ToList();

                if(fks.Count==0)
                {
                    if (col.Type == "int")
                    {
                        var input = new NumericUpDown();
                        var tag = new InputControlTag { Field = col.Name };
                        tag.SetValue = (r, i) => (i as NumericUpDown).Value = Convert.ToInt32(r.Cells[col.Name].Value);
                        tag.GetValue = i => (int)((i as NumericUpDown).Value);
                        input.Tag = tag;
                        AddInputRow(col.Name, input);
                    }
                    else
                    {
                        var input = new TextBox();
                        var tag = new InputControlTag { Field = col.Name };
                        tag.SetValue = (r, i) => (i as TextBox).Text = r.Cells[col.Name].Value as string;                        
                        tag.GetValue = i => (i as TextBox).Text;

                        input.Tag = tag;
                        AddInputRow(col.Name, input);
                    }
                }
                else
                {
                    var fk = fks[0];

                    if (fk.TableColumn == ParentPrimaryKey)
                        continue;

                    var ds = new DataSet();
                    DbUtils.Select(ds, $"SELECT * FROM {fk.ReferencedTable}");

                    var items = new List<ComboBoxItemView>();
                    foreach (DataRow row in ds.Tables[0].Rows) 
                    {
                        items.Add(new ComboBoxItemView(row, fk.ReferencedColumn));
                    }

                    var combobox = new ComboBox();
                    combobox.DataSource = items;
                    var tag = new InputControlTag { Field = col.Name };
                    tag.SetValue = (r, i) =>
                    {                        
                        var id = Convert.ToInt32(r.Cells[col.Name].Value);
                        var cbx = i as ComboBox;
                        cbx.SelectedItem = (cbx.DataSource as List<ComboBoxItemView>)
                            .Where(x => x.Id == id).First();
                    };
                    tag.GetValue = i => ((i as ComboBox).SelectedItem as ComboBoxItemView).Id;                    
                    combobox.Tag = tag;

                    AddInputRow(col.Name, combobox);
                }                
            }
        }

        void LoadParentTable()
        {
            DbUtils.Select(ParentDataSet, $"SELECT * FROM {ParentTableName}");
            ParentsView.DataSource = ParentDataSet.Tables[0];
        }

        void LoadChildTable(int parentId)
        {
            DbUtils.Select(ChildDataSet, $"SELECT * FROM {ChildTableName} WHERE {ParentForeignKeyInChild}=@id",
                ("@id", parentId));
            ChildrenView.DataSource = ChildDataSet.Tables[0];
        }

        void AddInputRow(string caption, Control input)
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Top;            

            var label = new Label { Text = caption };
            label.Dock = DockStyle.Left;

            input.Dock = DockStyle.Fill;

            panel.Controls.Add(input);
            panel.Controls.Add(label);

            panel.Height = input.Height;
            label.Top = (panel.Height - label.Height) / 2;

            PropsPanel.Controls.Add(panel);

            DynamicInputs.Add(input);
        }

        int SelectedParentId =>
            Convert.ToInt32(ParentDataSet.Tables[0].Rows[ParentsView.SelectedRows[0].Index][ParentPrimaryKey]);

        int SelectedChildId =>
            Convert.ToInt32(ChildDataSet.Tables[0].Rows[ChildrenView.SelectedRows[0].Index][ChildPrimaryKey]);

        private void ParentsView_SelectionChanged(object sender, EventArgs e)
        {
            if(ParentsView.SelectedCells.Count==1)
            {
                ParentsView.Rows[ParentsView.SelectedCells[0].RowIndex].Selected = true;
            }
            else
            {
                if (!Loaded) return;                
                LoadChildTable(SelectedParentId);
            }
        }

        bool Loaded = false;
        private void MainForm_Load(object sender, EventArgs e)
        {
            Loaded = true;
        }

        private void ChildrenView_SelectionChanged(object sender, EventArgs e)
        {
            if (ChildrenView.SelectedCells.Count == 1)
            {
                ChildrenView.Rows[ChildrenView.SelectedCells[0].RowIndex].Selected = true;
            }
            else
            {
                if (!Loaded) return;

                var selected = ChildrenView.SelectedRows[0];

                SelectedChildLabel.Text = Convert.ToString(selected.Cells[ChildPrimaryKey].Value);

                foreach(var input in DynamicInputs)
                {
                    (input.Tag as InputControlTag)?.SetValue(selected, input);
                }
            }
        }

        private List<(string Field, object Value)> CollectInputs() => DynamicInputs
                .Select(i => ((i.Tag as InputControlTag).Field, (i.Tag as InputControlTag).GetValue(i)))
                .ToList();

        private void AddButton_Click(object sender, EventArgs e)
        {
            var props = CollectInputs().Append((ParentForeignKeyInChild, SelectedParentId)).ToList();
            try
            {
                DbUtils.Insert(ChildTableName, props);
                LoadChildTable(SelectedParentId);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var props = CollectInputs();
            try
            {
                DbUtils.Update(ChildTableName, props, ChildPrimaryKey, SelectedChildId);
                LoadChildTable(SelectedParentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtils.Delete(ChildTableName, ChildPrimaryKey, SelectedChildId);
                LoadChildTable(SelectedParentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void monedeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new MainForm(@"Config\monede.xml");
            form.Show();
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new MainForm(@"Config\console.xml");
            form.Show();
        }
    }
}
