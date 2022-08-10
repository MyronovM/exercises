using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__refactoring
{
    public class Comedy : BaseGenre
    {
        public Comedy(Genre type)
        {
            this.type = type;
        }

        public override int CalculateAmount(int audience)
        {
            int thisAmount = 30000;
            if (audience > 20)
            {
                thisAmount += 10000 + 500 * (audience - 20);
            }

            thisAmount += 300 * audience;
            return thisAmount;
        }

        public override int CalculateExtra(int audience)
        {
            // add volume credits
            int volumeCredits = Math.Max(audience - 30, 0);
            // add extra credit for every ten comedy attendees
            volumeCredits += (int)Math.Floor((double)audience / 5.0d);

            return volumeCredits;
        }
    }
}
