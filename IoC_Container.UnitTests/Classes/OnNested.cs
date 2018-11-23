namespace IoC_Container.UnitTests.Classes
{
    class OnNested
    {

        private OnConstructorAndProperty obj;

        public OnNested(OnConstructorAndProperty obj)
        {
            this.obj = obj;
        }

        public OnConstructorAndProperty GetObj()
        {
            return obj;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            OnNested newObj = (OnNested)o;
            return obj.Equals(newObj.GetObj());
        }

    }
}
