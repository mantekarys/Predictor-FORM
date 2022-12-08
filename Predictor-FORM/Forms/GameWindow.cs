using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CustomFocuser = System.Windows.Input;
using Logger = Predictor_FORM.Server.Logger;
using Predictor_FORM.Server;

namespace Predictor_FORM.Forms
{
    public partial class GameWindow : Form
    {
        Map.Map map;
        List<Character.Player> players;
        
        List<Projectile> projectiles = new List<Projectile>();
        List<PickUp> pickables;
        List<MapObject> mapObjects = new List<MapObject>();
        List<Trap> traps = new List<Trap>();
        List<Obstacle> obstacles = new List<Obstacle>();
        List<string> messages = new List<string>();
        List<string> shownMessages = new List<string>();
        HashSet<Keys> keys = new HashSet<Keys>();
        MouseEventArgs mouseClick;
        public WebSocket ws;
        bool dead = false;
        List<Player> previous;
        List<int> damaged = new List<int>();
        readonly Use1Expression Use1 = new Use1Expression();
        readonly Use2Expression Use2 = new Use2Expression();
        readonly Use3Expression Use3 = new Use3Expression();
        List<Npc> npcs = new List<Npc>();

        ConcreteMediator m = new ConcreteMediator();
        P1Colleague c1;
        P2Colleague c2;

        MouseEventArgs mousePos;
        int which;
        Form1 form1;
        int matchId = 0;
        bool first = true;
        string message;

        Logger log = Logger.getInstance();

        Keys ConsoleTriggerKey = Keys.None;

        internal GameWindow(Map.Map map, List<Character.Player> p, int which, int matchId, WebSocket wsOld)
        {
            InitializeComponent();
            this.map = map;
            players = p;
            this.which = which;
            this.matchId = matchId;
            this.pickables = new List<PickUp>() { new DamagePowerUp((350, 350)) };

            if(which == 0)
            {
                c1 = new P1Colleague(m);
                m.Colleague1 = c1;
            }
            else
            {
                c2 = new P2Colleague(m);
                m.Colleague2 = c2;
            }

            ws = wsOld;
            ws.OnMessage += Ws_OnMessage;
            ws.Connect();

            Timer newTimer = new Timer();
            newTimer.Elapsed += new ElapsedEventHandler(Send);
            newTimer.Interval = 20;
            newTimer.Start();
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            previous = new List<Player>(players);
            (players, this.map, pickables, this.projectiles, this.traps, this.obstacles, this.npcs, this.messages) = JsonConvert.DeserializeObject<(List<Character.Player>, Map.Map, List<PickUp>, List < Projectile >, List<Trap>, List<Obstacle>, List<Npc>, List<string>)>(e.Data);
            if (which == 0)
            {
                c2 = new P2Colleague(m);
                m.Colleague2 = c2;
            }
            else
            {
                c1 = new P1Colleague(m);
                m.Colleague1 = c1;
            }

            if (messages != null)
            {
                if (messages.Count > shownMessages.Count)
                {
                    EmptyMessageBox();
                    shownMessages.Clear();
                    foreach (var mes in messages)
                    {
                        MessageBox.Text += mes.ToString() + "\r\n";
                        shownMessages.Add(mes);
                    }
                }
            }

            if (damaged.Count == 0)
            {
                foreach (var item in players)
                {
                    damaged.Add(0);
                }
            }
            for (int i = 0; i < players.Count; i++)
            {
                players[i].decoratable = new WeaponDecorator(players[i].playerClass);
                players[i].decoratable = new TypeDecorator(players[i].decoratable);
                if (players[i].playerClass.health < previous[i].playerClass.health)
                {
                    damaged[i] = 10;
                }
                if (damaged[i] > 0)
                {
                    players[i].decoratable = new DamageDecorator(players[i].decoratable);
                    damaged[i]--;
                }
            }

            if (this.mapObjects.Count > this.traps.Count + this.obstacles.Count || first)
            {
                this.mapObjects.Clear();
                this.mapObjects.AddRange(this.obstacles);
                this.mapObjects.AddRange(this.traps);
                first = false;
            }
            this.Invalidate();
        }
        private void Send(object sender, EventArgs e)
        {
            if (!dead)
            {
                var relativePoint = Cursor.Position;
                (int, int) mouse = (relativePoint.X, relativePoint.Y);
                var mes = JsonConvert.SerializeObject((keys.ToList(), mouse, which, matchId, message));
                ws.Send(mes);
                mouseClick = null;
                message = null;
                if (!ConsoleTriggerKey.Equals(Keys.None))
                {
                    keys.Remove(ConsoleTriggerKey);
                    ConsoleTriggerKey = Keys.None;
                }
            }
        }
        private void Map_Load(object sender, EventArgs e)
        {
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
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
            SolidBrush brushR = new SolidBrush(Color.FromArgb(255, 0, 255));
            SolidBrush brushProj = new SolidBrush(Color.FromArgb(255, 165, 0));
            SolidBrush brushNpc = new SolidBrush(Color.Green);
            SolidBrush brushNpcActivated = new SolidBrush(Color.LightGreen);
            int num = 0;
            foreach (var p in players)
            {
                Character.Character c = p.playerClass;
                if (p.decoratable != null)
                {
                    c = p.decoratable;
             
                }
                
                if (p.playerClass.health > 0)
                {
                    c.draw(g);

                }
                else
                {
                    if (num == which) { dead = true; }
                }
                num++;
            }
            foreach (var c in projectiles.ToList())
            {
                if (c == null)
                {
                    continue;
                }
                g.FillRectangle(brushProj, c.coordinates.Item1, c.coordinates.Item2, c.size, c.size);
            }
            brushR = new SolidBrush(Color.FromArgb(88, 233, 243));
            foreach (var picks in pickables)
            {
                g.FillRectangle(brushR, picks.coordinates.Item1, picks.coordinates.Item2, 4, 4);
            }


            foreach (var n in npcs)
            {
                if (n.ability.activated)
                {
                    g.FillRectangle(brushNpcActivated, n.coordinates.Item1, n.coordinates.Item2, n.size, n.size);
                }
                else
                {
                    g.FillRectangle(brushNpc, n.coordinates.Item1, n.coordinates.Item2, n.size, n.size);
                }
            }
        }
        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if((Keys)e.KeyData == Keys.Oemtilde)
            {
                GameConsole.Enabled = true;
                GameConsole.Focus();
            }

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

        private void NewInput(object sender, EventArgs e)
        {

            if (Use1.Recognize(GameConsole.Text))
            {
                keys.Add(Keys.D1);
                ConsoleTriggerKey = Keys.D1;
                EmptyGameConsole(); 
            }else if (Use2.Recognize(GameConsole.Text))
            {
                keys.Add(Keys.D2);
                ConsoleTriggerKey=Keys.D2;
                EmptyGameConsole();
            }else if (Use3.Recognize(GameConsole.Text))
            {
                keys.Add(Keys.D3);
                ConsoleTriggerKey = Keys.D3;
                EmptyGameConsole();
            }
        }
        private void EmptyGameConsole()
        {
            GameConsole.Enabled = false;
            GameConsole.Text = "";
        }

        private void EmptyMessageBox()
        {
            MessageBox.Text = "";
        }

        private void GameConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keys)e.KeyData == Keys.Enter && GameConsole.Enabled)
            {
                if (which == 0)
                {
                    message = c1.Send(GameConsole.Text);
                }
                else
                {
                    message = c2.Send(GameConsole.Text);
                }
                //message = GameConsole.Text;
                EmptyGameConsole();
            }
        }
    }
}
