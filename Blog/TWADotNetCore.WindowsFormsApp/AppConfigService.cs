using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHMTZDotNetCore.WindowsFormsApp
{
    internal class AppConfigService
    {
        public string GetDbConnection()
        {
            return ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }
    }
}
