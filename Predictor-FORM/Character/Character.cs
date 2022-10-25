using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal abstract class Character
    {
        public virtual int size {
            get;set;
        }
        public int speed;
        public int health;
        public int damage;
        public virtual (int,int) coordinates {
            get;set;
        }
        public Ability ability;

        public void takeDamage(int damage)
        {
            this.health -= damage;
        }
        public virtual void draw(Graphics g)
        {

        }
    }
}
