using Licenta.Commons.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Commons.OS
{
    public static class ProcessUtil
    {
        public static (string Out, string Err) Run(string path, params string[] args)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = path,
                Arguments = args.Select(_ => $"\"{_}\"").JoinToString(" "),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError=true,
                CreateNoWindow = true
            };

            var proc = new Process { StartInfo = startInfo };
            proc.Start();

            return (proc.StandardOutput.ReadToEnd(), proc.StandardError.ReadToEnd());
        }
    }
}
