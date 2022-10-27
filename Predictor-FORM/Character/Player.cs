using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal class Player
    {
        public string userName;
        public int score = 0;
        public Class playerClass;
        public Character decoratable;
        public Player()
        {
        }
        public Player(Class c, string userName = "Bill")
        {
            playerClass = c;
            this.userName = userName;
            decoratable = c;
        }
    }
}
