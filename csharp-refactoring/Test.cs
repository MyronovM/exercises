using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__refactoring
{
    public class Test 
    {
        List<BaseGenre> list = new List<BaseGenre>();

        public Test()
        {
            Tragedy tragedy = new Tragedy(Genre.Tragedy);
            Comedy comedy = new Comedy(Genre.Comedy);
            list.Add(tragedy);
            list.Add(comedy);
        }

        // getter of type of Genre
        public BaseGenre GetGenre(Genre type)
        {
            return list.Where(x => x.Type == type).FirstOrDefault();
        }
    }
}
