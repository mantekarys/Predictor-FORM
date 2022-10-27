using Predictor_FORM.Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal class Npc : Character
    {
        public Npc(int size, int speed, int health, int damage, int x, int y)
        {
            this.size = size;
            this.speed = speed;
            this.health = health;
            this.damage = damage;
            this.coordinates = (x, y);
        }


        public override void draw(Graphics g)
        {
            throw new NotImplementedException();
        }
    }

}
