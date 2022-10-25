using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal class DamageDecorator : Decorator
    {
        public DamageDecorator(Character character) : base(character)
        {

        }

        public override void draw(Graphics g)
        {
            base.draw(g);
            addedColor(g);
        }
        public void addedColor(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            g.FillRectangle(brush, _character.coordinates.Item1, _character.coordinates.Item2, _character.size, _character.size);
        }
    }
}
