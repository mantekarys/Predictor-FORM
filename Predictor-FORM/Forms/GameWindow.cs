﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Predictor_FORM.Map;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


using System.Windows.Threading;
using System.Windows;
using Newtonsoft.Json;
using System.Threading;


using WebSocketSharp;
using Newtonsoft.Json;
using System.Timers;
using Timer = System.Timers.Timer;
using Predictor_FORM.Character;

namespace Predictor_FORM.Forms
{
    public partial class GameWindow : Form
    {
        Map.Map map;
        List<Character.Class> characters;
        List<Projectile> projectiles = new List<Projectile>();
        List<PickUp> pickables;
        List<MapObject> mapObjects = new List<MapObject>();
        List<Trap> traps = new List<Trap>();
        List<Obstacle> obstacles = new List<Obstacle>();
        HashSet<Keys> keys = new HashSet<Keys>();
        MouseEventArgs mouseClick;
        public WebSocket ws;

        MouseEventArgs mousePos;
        int which;
        Form1 form1;
        int matchId = 0;
        bool first = true;

        internal GameWindow(Map.Map map, List<Character.Class> c, int which, int matchId, WebSocket wsOld)
        {
            InitializeComponent();
            this.map = map;
            characters = c;
            this.which = which;
            this.matchId = matchId;
            this.pickables = new List<PickUp>() { new DamagePowerUp((350, 350)) };



            ws = wsOld;
            ws.OnMessage += Ws_OnMessage;
            ws.Connect();


            Timer newTimer = new Timer();
            newTimer.Elapsed += new ElapsedEventHandler(Send);
            newTimer.Interval = 10;
            newTimer.Start();
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
            (characters, this.map, pickables, this.projectiles, this.traps, this.obstacles) = JsonConvert.DeserializeObject<(List<Character.Class>, Map.Map, List<PickUp>, List < Projectile >, List<Trap>, List<Obstacle>)>(e.Data);
            
            if (this.mapObjects.Count > this.traps.Count + this.obstacles.Count || first)
            {
                this.mapObjects.Clear();
                this.mapObjects.AddRange(this.obstacles);
                this.mapObjects.AddRange(this.traps);
                first = false;
            }

            this.Invalidate();
            //gw = new Forms.GameWindow(this.map, characters);
        }
        private void Send(object sender, EventArgs e)
        {
            var relativePoint = Cursor.Position;//this.PointToClient(Cursor.Position);//Cursor.Positiont;
            (int, int) mouse = (relativePoint.X, relativePoint.Y);
            var mes = JsonConvert.SerializeObject((keys.ToList(), mouse, which, matchId));
            ws.Send(mes);
            mouseClick = null;
        }
        private void Map_Load(object sender, EventArgs e)
        {
            int z = 5;
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //this.Paint += new PaintEventHandler(PaintF);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            Pen myPen = new Pen(Color.Red);
            myPen.Width = 2;
            g.DrawRectangle(myPen, 5, 5, map.size, map.size);
            foreach(var mo in mapObjects)
            {
                SolidBrush brusht = new SolidBrush(Color.FromName(mo.color));
                g.FillRectangle(brusht, mo.coordinates.Item1, mo.coordinates.Item2, mo.size, mo.size);
            }
            SolidBrush brush = new SolidBrush(Color.GreenYellow);
            //g.FillRectangle(brush, 5, 5, map.size, map.size);

            SolidBrush brushR = new SolidBrush(Color.FromArgb(255, 0, 255));
            SolidBrush brushProj = new SolidBrush(Color.FromArgb(255, 165, 0));
            foreach (var c in characters)
            {
                //g.DrawRectangle(myPen, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);
                g.FillRectangle(brushR, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);
            }
            foreach (var c in projectiles.ToList())
            {
                if (c == null)
                {
                    continue;
                }
                //g.DrawRectangle(myPen, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);
                g.FillRectangle(brushProj, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);

            }
            brushR = new SolidBrush(Color.FromArgb(88, 233, 243));
            foreach (var picks in pickables)
            {
                //g.DrawRectangle(myPen, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);
                g.FillRectangle(brushR, picks.coordinates.Item1, picks.coordinates.Item2, 4, 4);
                

            }

        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //keys.Append(keyData);
        //    //keys.Add(keyData);
        //    keys.Add((Keys)keyData);
        //    return true;
        //}


        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            keys.Add((Keys)e.KeyData);
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            keys.Remove((Keys)e.KeyData);   
        }

        private void GameWindow_MouseDown(object sender, MouseEventArgs e)
        {
            keys.Add(Keys.LButton);
        }

        private void GameWindow_MouseUp(object sender, MouseEventArgs e)
        {
            keys.Remove(Keys.LButton);
        }
    }
}
