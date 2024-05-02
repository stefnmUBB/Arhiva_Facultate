using AI.Lab8.Normalization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab8.Controls
{
    public partial class NormalizationMethodSelector : UserControl
    {
        public NormalizationMethodSelector()
        {
            InitializeComponent();
            var typesList = Assembly.GetExecutingAssembly().GetTypes().Where(t
               => t.IsClass && t.GetInterfaces().Contains(typeof(INormalizationMethod)))
               .Distinct().ToList();

            Combobox.Items.Clear();
            Combobox.Items.AddRange(typesList.ToArray());           
            Combobox.SelectedIndex = 0;
            Combobox.DisplayMember = "Name";
        }
        
        public Type Value
        {
            get=> Combobox.SelectedItem as Type;
            set => Combobox.SelectedItem = value;
        }        
    }
}
