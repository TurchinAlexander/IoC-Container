using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.UnitTests.Classes
{
    class InterfaceInject
    {

        private IClassToInject obj;

        public InterfaceInject(IClassToInject obj)
        {
            this.obj = obj;
        }

        public IClassToInject GetObj()
        {
            return obj;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            InterfaceInject newObj = (InterfaceInject)o;
            return obj.Equals(newObj.GetObj());
        }

    }
}
