using Predictor_FORM.Map;
using System;
using System.Collections.Generic;
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
            this.coordinates.Item1 = x;
            this.coordinates.Item2 = y;
        }

        public override void move()
        {
            throw new NotImplementedException();
        }
    }
}
