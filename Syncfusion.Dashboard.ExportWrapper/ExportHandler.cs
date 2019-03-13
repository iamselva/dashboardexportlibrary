using System;
using System.IO;
using System.Web.Configuration;

namespace Syncfusion.Dashboard.ExportWrapper
{
    public class ExportHandler
    {
        public void ExportUsingPhantom(string token)
        {
            using (var p = new System.Diagnostics.Process())
            {
                string fileName = "phantomjs.exe";
                string phantomPath = "";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileName))
                {
                    phantomPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                else if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "API\\" + fileName))
                {
                    phantomPath = AppDomain.CurrentDomain.BaseDirectory + "API\\";
                }
                else if (File.Exists(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\") + "DashboardServer.Web\\API\\" + fileName))
                {
                    phantomPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "..\\") + "DashboardServer.Web\\API\\";                    
                }
                else if (File.Exists(Path.GetFullPath(Environment.GetEnvironmentVariable("Home").ToString() + @"\site\wwwroot\API\phantomjs.exe")))
                {
                    phantomPath = Path.GetFullPath(Environment.GetEnvironmentVariable("Home").ToString() + @"\site\wwwroot\API\");
                }
                else if (HasSubAppName() && File.Exists(Path.GetFullPath(Environment.GetEnvironmentVariable("Home").ToString() + @"\site\wwwroot" + (HasSubAppName() ? WebConfigurationManager.AppSettings["SubAppName"] : string.Empty) + @"\API\" + fileName)))
                {
                    phantomPath = Path.GetFullPath(Environment.GetEnvironmentVariable("Home").ToString() + @"\site\wwwroot" + (HasSubAppName() ? WebConfigurationManager.AppSettings["SubAppName"] : string.Empty) + @"\API\");
                }
                Environment.CurrentDirectory = phantomPath + "Temp\\";
                p.StartInfo.FileName = phantomPath + fileName;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.Arguments = token + "js.js  --disk-cache=[true]";
                p.Start();
                p.WaitForExit(300000);
            }
        }

        public static bool HasSubAppName()
        {
            return string.IsNullOrEmpty(WebConfigurationManager.AppSettings["SubAppName"]) ? false : true;
        }
    }
}
