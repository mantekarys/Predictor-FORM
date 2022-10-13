using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Newtonsoft.Json;

namespace Predictor_FORM.Character
{
    internal class Class : Character
    {
 
        public DateTime lastAttack;
        class Message
        {
            List<string> buttons;
        }

        public Class(int size, int speed, int health, int damage, int x,int y)
        {
            this.size = size;
            this.speed = speed;
            this.health = health;
            this.damage = damage;

            //send other parameters on start or when took an upgrade
        }

        public override void move()
        {
        }
    }
}
