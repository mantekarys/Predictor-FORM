using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Predictor_FORM.Character;

namespace Predictor_FORM.Map
{
    internal class DamagePotion : Item
    {
        public new string name = "Damage potion";
        public new string description = "Increases damage temporarly";
        public new DateTime experationTime = DateTime.MinValue;
        public new int remainingTime = 200;
/*        public override void Use()
        {

        }
        public override void pickedUp(Class character)
        {
            character.AddToInventory(this);
        }

        public DamagePotion((int, int) position)
        {
            coordinates = position;
        }
*/    }
}
