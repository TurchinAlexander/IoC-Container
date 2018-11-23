using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.Interfaces
{
    public interface ISimpleIoC
    {
        T Resolve<T>() where T : class;
        void AsSingleton();
        void AsTransient();
        bool IsRegistered<Type>();
    }
}
