using Newtonsoft.Json;
using Predictor_FORM.Forms;
using Predictor_FORM.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;

namespace Predictor_FORM
{
    public partial class Create : Form
    {
        public Join join;
        public WebSocket ws;
        public Create(WebSocket wsOld)
        {
            InitializeComponent();
            ws = wsOld;
        }

        private void Create_Load(object sender, EventArgs e)
        {
        }

        private void CreateMatch_Click(object sender, EventArgs e)
        {
            long total = GC.GetTotalMemory(true);
            var sw = Stopwatch.StartNew();
            ProxyCreateMatch proxyCreateMatch= new ProxyCreateMatch();
            proxyCreateMatch.Match(this);

            //CreateMatch createMatch = new CreateMatch();
            //createMatch.Match(this);
            sw.Stop();
            Console.WriteLine($"Match functions executed {sw.Elapsed}");
            Console.WriteLine($"Diffrence in memory after Match function was executed {GC.GetTotalMemory(true) - total}");
        }

        public void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            int matchId, which;
            (matchId, which) = JsonConvert.DeserializeObject<(int,int)>(e.Data);
            join = new Forms.Join(matchId, which, ws);
        }

        private void Create_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
