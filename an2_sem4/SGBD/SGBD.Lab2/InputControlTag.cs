using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGBD.Lab2
{
    internal class InputControlTag
    {
        public string Field;
        public Action<DataGridViewRow, Control> SetValue;
        public Func<Control, object> GetValue;
    }
}
