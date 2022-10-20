using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal abstract class Character
    {
        public int size;
        public int speed;
        public int health;
        public int damage;
        public (int,int) coordinates;
        Ability ability;

        public abstract void move();//event
        public void takeDamage(int damage)
        {
            this.health -= damage;
        }
    }
}
