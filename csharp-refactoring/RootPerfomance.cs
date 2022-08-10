using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__refactoring
{
    [Serializable]
    public class RootPerfomance
    {
        public string customer { get; set; }
        public List<Performance> performances { get; set; }
    }
}
