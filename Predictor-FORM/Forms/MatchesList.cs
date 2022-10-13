using Newtonsoft.Json;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Logger = Predictor_FORM.Server.Logger;

namespace Predictor_FORM.Forms
{
    public partial class MatchesList : Form
    {
        List<(int, string)> matchIds;
        int which;
        WebSocket ws;
        public MatchesList(WebSocket wsOld)
        {
            InitializeComponent();

            ws = wsOld;
            ws.OnMessage += Ws_OnMessage;

            var mes = JsonConvert.SerializeObject(999);
            ws.Send(mes);
            Thread.Sleep(1000);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            matchIds = JsonConvert.DeserializeObject<List<(int, string)>>(e.Data);
            foreach (var item in matchIds)
            {
                listBox1.Items.Add(item.Item2);
            }
            ws.OnMessage -= Ws_OnMessage;
        }

        private void whichMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            which = JsonConvert.DeserializeObject<int>(e.Data);
            ws.OnMessage -= whichMessage;
        }

        private void MatchesList_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logger log = Logger.getInstance();
            try
            {
                int index = listBox1.SelectedIndex;
                if (index < 0)
                {
                    index = 0;
                }
                int matchId = matchIds[index].Item1;
                ws.OnMessage += whichMessage;
                var mes = JsonConvert.SerializeObject((876, matchIds[index].Item1.ToString()));
                ws.Send(mes);
                Thread.Sleep(1000);
                this.Hide();
                Join join = new Forms.Join(matchId, which, ws);
                join.Show();
            }
            catch (Exception ex)
            {
                log.WriteMessage("Couldn't join any match");
            }
        }
    }
}
