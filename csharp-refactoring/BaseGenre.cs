using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__refactoring
{
    public abstract class BaseGenre 
    {
        protected Genre type;
        public Genre Type => type;

        public virtual int CalculateAmount(int audience)
        {
            return 0;
        }

        public virtual int CalculateExtra(int audience)
        {
            return 0;
        }
    }
}
