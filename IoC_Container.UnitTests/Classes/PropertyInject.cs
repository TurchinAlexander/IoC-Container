using IoC_Container.Attributes;

namespace IoC_Container.UnitTests.Classes
{
    public class PropertyInject
    {
        [Initialize]
        public ClassToInject Obj { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            PropertyInject baseClass = (PropertyInject)obj;
            return Obj.Equals(baseClass.Obj);
        }

    }
}
