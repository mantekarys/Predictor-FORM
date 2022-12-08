using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Predictor_FORM.Server
{
    public class P1Colleague : Colleague
    {
        public P1Colleague(Mediator mediator): base(mediator)
        {
        }
        public string Send(string message)
        {
            return mediator.Send(message, this);
        }
        public string Notify(string message)
        {
            string text = "Player2 to Player1: "+ message;
            return text;
        }
    }
}
