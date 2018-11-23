using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.UnitTests.Classes
{
    class ConstructInject
    {

        private ClassToInject classToInject;

        public ConstructInject(ClassToInject classToInject)
        {
            this.classToInject = classToInject;
        }

        public ClassToInject GetClassToInject()
        {
            return classToInject;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            ConstructInject newObj = (ConstructInject)obj;
            return classToInject.Equals(newObj.GetClassToInject());
        }

    }
}
