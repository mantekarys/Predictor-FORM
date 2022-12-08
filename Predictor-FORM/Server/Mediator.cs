using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Server
{
    public abstract class Mediator
    {
        public abstract string Send(string message,Colleague colleague);
    }
}
