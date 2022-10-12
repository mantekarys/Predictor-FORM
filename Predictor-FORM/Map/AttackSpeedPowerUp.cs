﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Predictor_FORM.Character;

namespace Predictor_FORM.Map
{
    internal class AttackSpeedPowerUp : PowerUp
    {
        public new string name = "Attack Speed Power up";
        public new string description = "Increases attack rate";
        public new int experationTime = 200;
        public new int remainingTime = 200;
/*        public AttackSpeedPowerUp((int, int) coord)
        {
            this.coordinates = coord;
        }
        public override void pickedUp(Class character)
        {
            character.ApplyPowerUp(this);
        }
*/    }
}
