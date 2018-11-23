using IoC_Container.Attributes;

namespace IoC_Container.UnitTests.Classes
{
    class MultyConstructorAndPropertyInject
    {
        [Initialize]
        public ClassToInject Obj { get; set; }

        private ClassToInject obj;
        private ClassToInject obj1;

        public MultyConstructorAndPropertyInject(ClassToInject obj, ClassToInject obj1)
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

            MultyConstructorAndPropertyInject newObj = (MultyConstructorAndPropertyInject)o;
            return obj.Equals(newObj.GetObj())
                && obj1.Equals(newObj.GetObj1())
                && Obj.Equals(newObj.Obj);
        }

    }
}
