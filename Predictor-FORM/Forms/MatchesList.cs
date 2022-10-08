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
        public MatchesList()
        {
            InitializeComponent();

            using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Echo"))
            {
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                var mes = JsonConvert.SerializeObject(999);
                ws.Send(mes);
                Thread.Sleep(1000);
            }
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
        }
        //private void matchMessage(object sender, MessageEventArgs e)
        //{
        //    Console.WriteLine("Received from the server: " + e.Data);
        //    List<(int, string)> matchIds = new List<(int, string)>();
        //    matchIds = JsonConvert.DeserializeObject<List<(int, string)>>(e.Data);
        //    foreach (var item in matchIds)
        //    {
        //        listBox1.Items.Add(item.Item2);
        //    }
        //}
        private void whichMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            which = JsonConvert.DeserializeObject<int>(e.Data);
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
                using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Echo"))
                {
                    ws.OnMessage += whichMessage;
                    ws.Connect();
                    var mes = JsonConvert.SerializeObject((876, matchIds[index].Item1.ToString()));
                    ws.Send(mes);
                    Thread.Sleep(1000);
                }
                this.Hide();
                Join join = new Forms.Join(matchId, which);
                join.Show();
            }
            catch(Exception ex)
            {
                log.WriteMessage("Couldn't join any match");
            }
           
        }
    }
}
