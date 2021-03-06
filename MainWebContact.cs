using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SoulReaver
{
    public partial class MainWebContact : ServiceBase
    {
        HttpClient client = new HttpClient();
        public MainWebContact()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WebServicesSettings();
        }

        protected override void OnStop()
        {
        }
        private void WebServicesSettings()
        {
            client.BaseAddress = new Uri("https://localhost:44394/MainMiniJIRA.asmx/");
        }
    }
}
