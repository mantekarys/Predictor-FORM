﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Predictor_FORM.Character;

namespace Predictor_FORM.Map
{
    internal class DamagePowerUp : PowerUp
    {
        public new string name = "Damage power up";
        public new string description = "Increases damage";
        public new DateTime experationTime = DateTime.MinValue;
        public new int remainingTime = 200;
       public DamagePowerUp((int, int) coord)
        {
            this.coordinates = coord;
        }
        /* public override void pickedUp(Class character)
                {
                    character.ApplyPowerUp(this);
                }
        */
    }
}
