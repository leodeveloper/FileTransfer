using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileTransferService.Service
{
    public class AppConfiguration
    {
        public static string LocalDirectory { get { return ConfigurationManager.AppSettings["localDirectory"]; } }
        public static string DestinationEmailAddress { get { return ConfigurationManager.AppSettings["destinationEmail"]; } }
        
    }
}
