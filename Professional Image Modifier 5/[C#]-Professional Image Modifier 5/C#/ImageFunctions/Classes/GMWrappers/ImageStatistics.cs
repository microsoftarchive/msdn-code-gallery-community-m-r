using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace ImageFunctions.Classes
{
    class ImageStatistics
    {

        Process p = new Process();

        private string CurrentImage;
        private ArrayList Stats = new ArrayList();

        public delegate void StatisticsCompleteHandler(ArrayList Identity);
        public event StatisticsCompleteHandler StatsComplete;
        public delegate void StatisticsErrorHandler(string Message);
        public event StatisticsErrorHandler StatsError;

        public ImageStatistics(string CurrentImage)
        {
            this.CurrentImage = CurrentImage;
        }

        // Run Gm.exe and read its results saving them into output.
        public void GetStatistics()
        {
            string path = Path.Combine(Application.StartupPath, "Binn\\gm"); //TODO: This should also have an automated search function
            path = Path.Combine(path, "gm.exe");

            string arguments = string.Format("identify -verbose " + "\"" + this.CurrentImage + "\"");
            var startInfo = new ProcessStartInfo
            {
                Arguments = arguments,
                FileName = path,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,

            };

            p.EnableRaisingEvents = true;
            p.StartInfo = startInfo;
            p.Start();

            p.ErrorDataReceived += p_ErrorDataReceived;

            //	string error = p.StandardError.ReadToEnd();
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();

            if (output.Length > 0) OutputDataReceived(output);
            //	if (error.Length > 0) StatsError(error);

            if (StatsComplete != null) StatsComplete(Stats);
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            StatsError(e.Data);
            p.Dispose();

        }

        // Process the results of GM.EXE
        private void OutputDataReceived(string output)
        {
            string[] lines = output.Split('\r');
            string result = "";

            for (int idx = 0; idx < lines.Count(); idx++)
            {
                string[] parts = lines[idx].Replace("\\", "").Split(':'); // May not produce desired results if part[1] contains a : But we will know that if there are more that 2 parts.

                Stats.Add(parts[0].ToString());
                if (idx == 0)
                {
                    // This contains the path to the file so needs special treatment *fudging*
                    result = lines[idx].Replace(parts[0].ToString(), "").Substring(2).Trim();
                }
                else
                {
                    if (parts.Count() > 1)
                    {
                        for (int idx2 = 1; idx2 < parts.Count(); idx2++) // Start loop on 1 as we already added part[0] to the array.
                        {
                            result += parts[idx2].ToString().Trim(); // Remove additional spaces
                        }
                    }
                    else
                    {
                        result = "";
                    }
                }
                Stats.Add(result);
                result = "";
            }
        }


    }
}
