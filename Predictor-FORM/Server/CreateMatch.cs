using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Predictor_FORM.Server
{
    internal class CreateMatch : MatchCreation
    {
        public override void Match(Create create)
        {
            string text = create.textBox1.Text;
            var mes = JsonConvert.SerializeObject((752, text));
            create.ws.OnMessage += create.Ws_OnMessage;
            create.ws.Send(mes);
            Thread.Sleep(1000);

            create.join.ws.OnMessage -= create.Ws_OnMessage;
            create.Hide();
            create.join.Show();
        }
    }
}
