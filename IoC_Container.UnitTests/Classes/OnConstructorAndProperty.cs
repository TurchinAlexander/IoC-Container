using IoC_Container.Attributes;

namespace IoC_Container.UnitTests.Classes
{
    class OnConstructorAndProperty
    {
        [Initialize]
        public ClassToInject Obj { get; set; }

        private ClassToInject obj;

        public OnConstructorAndProperty(ClassToInject obj)
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

            OnConstructorAndProperty newObj = (OnConstructorAndProperty)o;
            return obj.Equals(newObj.GetObj())
                && Obj.Equals(newObj.Obj);
        }

    }
}
