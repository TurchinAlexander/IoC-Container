
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.UnitTests.Classes
{
    class OnConstructorAndMultyProperty
    {

   
        public ClassToInject Obj { get; set; }

  
        public ClassToInject Obj1 { get; set; }

        private ClassToInject obj;

        public OnConstructorAndMultyProperty(ClassToInject obj)
        {
            this.obj = obj;
        }

        public ClassToInject GetObj()
        {
            return obj;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            OnConstructorAndMultyProperty newObj = (OnConstructorAndMultyProperty)o;
            return obj.Equals(newObj.GetObj())
                && Obj.Equals(newObj.Obj)
                && Obj1.Equals(newObj.Obj1);
        }

    }
}
