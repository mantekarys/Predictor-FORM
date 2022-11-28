using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Predictor_FORM.Server
{
    internal class ProxyCreateMatch : MatchCreation
    {
        public override void Match(Create create)
        {
            string text = create.textBox1.Text;
            foreach (char c in text)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    create.errorProvider1.SetError(create.textBox1, "Match names cannot contain symbols");
                    return;
                }
            }

            CreateMatch createMatch= new CreateMatch();
            createMatch.Match(create);

        }
    }
}
