using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal class TypeDecorator : Decorator
    {
        public TypeDecorator(Character character) : base(character)
        {

        }

        public override void draw(Graphics g)
        {
            base.draw(g);
            addedBodySign(g);
        }
        public void addedBodySign(Graphics g)
        {
            Pen pen = new Pen(Color.FromArgb(0, 0, 0),2);
            g.DrawRectangle(pen, _character.coordinates.Item1, _character.coordinates.Item2, _character.size, _character.size);
        }
    }
}
