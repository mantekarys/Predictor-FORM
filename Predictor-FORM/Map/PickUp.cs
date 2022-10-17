using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Predictor_FORM.Character;


namespace Predictor_FORM.Map
{
    public class PickUp
    {
        public  string name { get; set; }
        public  string description { get; set; }
        public  int experationTime { get; set; }
        public  int remainingTime { get; set; }
        public readonly int size = 6;
        public (int, int) coordinates;
        //public abstract void pickedUp(Class @class);
    }
}
