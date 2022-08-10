using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__refactoring
{
    public class Tragedy : BaseGenre
    {
        public Tragedy(Genre type)
        {
            this.type = type;
        }

        public override int CalculateAmount(int audience)
        {
            int thisAmount = 40000;

            if (audience > 30)
            {
                thisAmount += 1000 * (audience - 30);
            }

            return thisAmount;
        }

        public override int CalculateExtra(int audience)
        {
            // add volume credits
            int volumeCredits = Math.Max(audience - 30, 0);

            return volumeCredits;
        }
    }
}
