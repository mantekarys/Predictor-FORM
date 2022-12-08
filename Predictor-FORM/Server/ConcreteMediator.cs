using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Server
{
    public  class ConcreteMediator: Mediator
    {
        P1Colleague colleague1;
        P2Colleague colleague2;
        public P1Colleague Colleague1
        {
            set { colleague1 = value; }
        }
        public P2Colleague Colleague2
        {
            set { colleague2 = value; }
        }
        public override string Send(string message, Colleague colleague)
        {
            if (colleague == colleague1)
            {
                return colleague2.Notify(message);
            }
            else
            {
                return colleague1.Notify(message);
            }
        }
    }
}
