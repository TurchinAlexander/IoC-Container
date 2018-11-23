using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.UnitTests.Classes
{
    public class ClassWithMultyConstructor: IClassToInject
    {
        private ClassToInject obj;
        private ClassToInject obj1;

        public ClassWithMultyConstructor(ClassToInject obj)
        {
            this.obj = obj;
        }  

        public ClassWithMultyConstructor(ClassToInject obj, ClassToInject obj1)
        {
            this.obj = obj;
            this.obj1 = obj1;
        }

        public ClassToInject GetObj()
        {
            return obj;
        }

        public ClassToInject GetObj1()
        {
            return obj1;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            ClassWithMultyConstructor newObj = (ClassWithMultyConstructor)o;
            return obj.Equals(newObj.GetObj())
                && obj1.Equals(newObj.GetObj1());
        }
    }
}
