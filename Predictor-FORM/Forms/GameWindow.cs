using System;
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

namespace Predictor_FORM.Forms
{
    public partial class GameWindow : Form
    {
        Map.Map map;
        List<Character.Class> characters;
        List<MapObject> mapObjects = new List<MapObject>();
        List<Trap> traps = new List<Trap>();
        List<Obstacle> obstacles = new List<Obstacle>();
        HashSet<Keys> keys = new HashSet<Keys>();
        MouseEventArgs mouseClick;
        WebSocket ws;

        MouseEventArgs mousePos;
        int which;
        Form1 form1;
        int matchId = 0;
        bool first = true;

        internal GameWindow(Map.Map map, List<Character.Class> c, int which, int matchId)
        {
            InitializeComponent();
            this.map = map;
            characters = c;
            this.which = which;
            this.matchId = matchId;


            this.MouseClick += Form_MouseDown;
            this.MouseMove += Form_MouseMove;

            ws = new WebSocket("ws://127.0.0.1:7890/EchoAll");
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
            (characters, this.map, this.traps, this.obstacles) = JsonConvert.DeserializeObject<(List<Character.Class>, Map.Map, List<Trap>, List<Obstacle>)>(e.Data);
            
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
            var mouse = mouseClick;
            if (mouseClick == null)
            {
                mouse = mousePos;
            }
            var mes = JsonConvert.SerializeObject((keys,mouse, which, matchId));
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

            //This be a place for testing trap and obstacle drawing

            //Trap trap1 = new Trap(15, 15, "Red");
            //Trap trap2 = new Trap(20, 35, "Yellow");

            //traps.Add(trap1);
            //traps.Add(trap2);

            //Obstacle obstacle1 = new Obstacle(150, 150, "Red");
            //Obstacle obstacle2 = new Obstacle(200, 350, "Yellow");

            //obstacles.Add(obstacle1);
            //obstacles.Add(obstacle2);




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
            foreach (var c in characters)
            {
                //g.DrawRectangle(myPen, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);
                g.FillRectangle(brushR, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);

            }

        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    //keys.Append(keyData);
        //    //keys.Add(keyData);
        //    keys.Add((Keys)keyData);
        //    return true;
        //}

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClick = e;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e;
        }

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
    }
}
