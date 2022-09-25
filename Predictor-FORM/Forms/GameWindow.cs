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

namespace Predictor_FORM.Forms
{
    public partial class GameWindow : Form
    {
        Map.Map map;
        internal GameWindow(Map.Map map)
        {
            InitializeComponent();
            this.map = map;
        }

        private void Map_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;

            g = e.Graphics;

            Pen myPen = new Pen(Color.Red);
            myPen.Width = 2;
            g.DrawRectangle(myPen, 5, 5, 500, 500);

        }
    }
}
