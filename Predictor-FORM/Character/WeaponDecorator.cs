using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal class WeaponDecorator : Decorator
    {
        public WeaponDecorator(Character character) : base(character)
        {

        }

        public override void draw(Graphics g)
        {
            base.draw(g);
            addedWeaponImage(g);
        }
        public void addedWeaponImage(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 0, 200));
            g.FillRectangle(brush, _character.coordinates.Item1 + 2, _character.coordinates.Item2 -11 , 11, 11);
        }
    }
}
