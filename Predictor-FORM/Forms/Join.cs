﻿using Newtonsoft.Json;
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
        public Join(int matchId, int which)
        {
            InitializeComponent();
            this.matchId = matchId;
            this.which = which;
            listBox1.Items.Add("Rogue");
            listBox1.Items.Add("Tank");
            listBox1.Items.Add("Gunner");
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
            //using (WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Echo"))
            //{
            //    ws.OnMessage += Ws_OnMessage;
            //    ws.Connect();
            //    var mes = JsonConvert.SerializeObject((545, matchId, listBox1.SelectedItem, this.ready ,this.which));
            //    ws.Send(mes);
            //    this.ready *= -1;
            //    Thread.Sleep(1000);
            //}

            WebSocket ws = new WebSocket("ws://127.0.0.1:7890/Echo");
            ws.OnMessage += Ws_OnMessage;
            ws.Connect();
            var mes = JsonConvert.SerializeObject((545, matchId, listBox1.SelectedItem, this.ready, this.which));
            ws.Send(mes);
            this.ready *= -1;
            Thread.Sleep(1000);
            //if (gw != null)
            //{
            //    gw.Show();
            //}

        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            int count = e.Data.Count(x => x == ':');
            //if (count == 3)
            //{
            //    //string text = JsonConvert.DeserializeObject<string>(e.Data);
            //    List<Character.Class> characters;
            //    (characters, this.map, which) = JsonConvert.DeserializeObject<(List<Character.Class>, Map.Map, int)>(e.Data);
            //    gw = new Forms.GameWindow(this.map, characters, which);
            //}
            List<Character.Class> characters;
            (characters, this.map) = JsonConvert.DeserializeObject<(List<Character.Class>, Map.Map)>(e.Data);
            gw = new Forms.GameWindow(this.map, characters, which, matchId);
            this.Invalidate();


        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (gw != null)
            {
                this.Hide();
                gw.Show();
            }

        }
    }
}
