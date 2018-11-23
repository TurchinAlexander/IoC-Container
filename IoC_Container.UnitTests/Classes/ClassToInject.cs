using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.UnitTests.Classes
{
    public class ClassToInject : IClassToInject
    {
        public ClassToInject()
        {

        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            return true;
        }
    }
}
