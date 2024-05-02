using System;
using System.Windows.Forms;

namespace AI.Lab8.Utils
{
    internal class Logger
    {
        public void Log(string message)
        {            
            Write?.Invoke(message);
        }

        public delegate void OnWrite(string message);
        public event OnWrite Write;
    }
}