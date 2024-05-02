using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBD.Lab2
{
    internal class Table
    {
        public string Name { get; }
        public List<string> Columns { get; }

        public Table(string name)
        {
            Name = name;
        }
    }
}
