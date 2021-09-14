using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace cmd_service.Services
{
    public class ProcessService
    {
        public string commandExec(string cmd) {
            
            int lineCount = 0;
            List<string> doutput = new List<string>();
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    lineCount++;
                    doutput.Add(e.Data);
                }
            });
            process.Start();
            process.StandardInput.WriteLine(cmd);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.Close();
            var table = $@"<table>";
            foreach (var item in doutput)
            {
                table += "<tr>";
                var row = new List<char>();
                row.Add(' ');
                foreach (var ch in item.ToCharArray())
                {
                    if (!(row.Last() == ' ' && ch == ' '))
                        row.Add(ch);
                }
                var ans = new string(row.ToArray());

                foreach (var td in ans.Split(" "))
                {
                    table += "<td>" + td + "<td>";
                }
                table += "</tr>";
            } 
            table += "</table>";
            return table;
        }
        
    }
}