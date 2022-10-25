using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal abstract class Decorator : Character
    {
        public Character _character;

        public Decorator(Character character)
        {
            _character = character;
        }
        public override void draw(Graphics g)
        {
            if (_character != null)
            {
                _character.draw(g);
            }
            
        }
        public override (int, int) coordinates { 
            get => _character.coordinates; 
            set => _character.coordinates = value; 
        }
        public override int size { 
            get => _character.size; 
            set => _character.size = value; }
    }
}
