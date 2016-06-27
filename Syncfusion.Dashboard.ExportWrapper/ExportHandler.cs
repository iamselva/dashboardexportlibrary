using System;

namespace Syncfusion.Dashboard.ExportWrapper
{
    public class ExportHandler
    {
        public void ExportUsingPhantom(string token)
        {
            var p = new System.Diagnostics.Process();
            {
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                p.StartInfo.FileName = "phantomjs.exe";
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = token + "js.js  --disk-cache=[true]";
                p.Start();
                p.WaitForExit();
            }
        }           
    }
}
