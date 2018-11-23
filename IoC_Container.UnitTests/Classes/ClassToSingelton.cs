using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.UnitTests.Classes
{
    public class ClassToSingelton: IClassToInject
    {
        private int _count=0;
        public ClassToSingelton(int count)
        {
            _count = count;
        }

        public int CountOf()
        {
            return ++_count;
        }
    }
}
