using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
//using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WebSocketSharp;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
using Predictor_FORM.Map;


using System.Threading;
using Predictor_FORM.Forms;

namespace Predictor_FORM
{
    public partial class Form1 : Form
    {
        Map.Map map;
        Forms.GameWindow gw;
        int which;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MapObject m = new MapObject();
            //using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Echo"))
            //{
            //    ws.OnMessage += Ws_OnMessage;

            //    ws.Connect();
            //    ws.Send("Hello from PCamp!");
            //}

        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            List<Character.Class> characters;
            (characters, this.map,which) = JsonConvert.DeserializeObject<(List<Character.Class>,Map.Map,int)>(e.Data);
            gw = new Forms.GameWindow(this.map, characters, which,0);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            Create create = new Create();
            create.Show();

        }

        private void Join_Click(object sender, EventArgs e)
        {
            this.Hide();
            MatchesList matchesList = new MatchesList();
            matchesList.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Echo"))
            {

                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                var mes = JsonConvert.SerializeObject(159);
                ws.Send(mes);
                Thread.Sleep(1000);
                gw.Show();
            }
        }
    }
}
