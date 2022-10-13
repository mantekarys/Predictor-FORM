using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Map
{
    public abstract class  MapObject
    {
        public int size;
        public (int, int) coordinates;
        public string color;
    }
}
