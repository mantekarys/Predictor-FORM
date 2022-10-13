using Newtonsoft.Json;
using Predictor_FORM.Character;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using static System.Net.Mime.MediaTypeNames;

namespace Predictor_FORM.Forms
{
    public partial class Join : Form
    {
        int matchId;
        Map.Map map;
        Forms.GameWindow gw;
        int which;
        int ready = 1;
        public WebSocket ws;
        public Join(int matchId, int which, WebSocket wsOld)
        {
            InitializeComponent();
            this.matchId = matchId;
            this.which = which;
            listBox1.Items.Add("Rogue");
            listBox1.Items.Add("Tank");
            listBox1.Items.Add("Gunner");
            ws = wsOld;
        }

        private void Join_Load(object sender, EventArgs e)
        {
        }

        private void Join_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ws.OnMessage += Ws_OnMessage;
            var mes = JsonConvert.SerializeObject((545, matchId, listBox1.SelectedItem, this.ready, this.which));
            ws.Send(mes);
            this.ready *= -1;
            Thread.Sleep(1000);
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            int count = e.Data.Count(x => x == ':');
            List<Character.Player> players;
            (players, this.map) = JsonConvert.DeserializeObject<(List<Character.Player>, Map.Map)>(e.Data);
            gw = new Forms.GameWindow(this.map, players, which, matchId, ws);
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (gw != null)
            {
                this.Hide();
                gw.ws.OnMessage -= Ws_OnMessage;
                gw.Show();
            }

        }
    }
}
