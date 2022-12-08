using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Character
{
    internal class Use1Expression
    {
        public bool Recognize(string text)
        {
            if(text == "use 1")
            {
                return true;
            }
            return false;
        }

    }
}
