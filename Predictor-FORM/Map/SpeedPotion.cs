using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Predictor_FORM.Character;

namespace Predictor_FORM.Map
{
    internal class SpeedPotion : Item
    {
        public new string name = "Speed potion";
        public new string description = "Increases movement speed temporarly";
        public new DateTime experationTime = DateTime.MinValue;
        public new int remainingTime = 200;
        /*public override void Use()
        {

        }
        public override void pickedUp(Class character)
        {
            character.AddToInventory(this);
        }

        public SpeedPotion((int, int) position)
        {
            coordinates = position;
        }*/
    }
}
