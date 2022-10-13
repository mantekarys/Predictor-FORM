using Newtonsoft.Json;
using Predictor_FORM.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        Join join;
        WebSocket ws;
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
            var mes = JsonConvert.SerializeObject((752, textBox1.Text));
            ws.OnMessage += Ws_OnMessage;
            ws.Send(mes);
            Thread.Sleep(1000);

            //ws.OnMessage -= Ws_OnMessage;
            join.ws.OnMessage -= Ws_OnMessage;
            this.Hide();
            join.Show();

        }
        private void Ws_OnMessage(object sender, MessageEventArgs e)
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
