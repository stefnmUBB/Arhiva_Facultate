using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBD.Lab2
{
    internal class ComboBoxItemView
    {
        public int Id { get; set; }
        public string Display { get; set; }

        public ComboBoxItemView(DataRow row, string columnId)
        {
            Id = Convert.ToInt32(row[columnId]);
            Display = string.Join(", ", row.ItemArray);
        }

        public override string ToString()
        {
            return Display;
        }
    }
}
