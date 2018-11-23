using System;
using NUnit.Framework;

using IoC_Container.UnitTests.Classes;

namespace IoC_Container.UnitTests
{
    [TestFixture]
    public class InjectorTests
    {
        Injector injector = Injector.CreateInstance();

        [Test]
        public void Inject_ConcreteClass_ReturnIntance()
        {
            injector.Register<ConstructInject, ConstructInject>();
            injector.Register<ClassToInject, ClassToInject>();

            ConstructInject resolved = injector.Resolve<ConstructInject>();

            ConstructInject created = new ConstructInject(new ClassToInject());

            Assert.AreEqual(resolved, created);
        }

        [Test]
        public void Inject_Interface_ReturnInstance()
        {
            injector.Register<InterfaceInject, InterfaceInject>();
            injector.Register<IClassToInject, ClassToInject>();

            InterfaceInject resolved = injector.Resolve<InterfaceInject>();

            InterfaceInject created = new InterfaceInject(new ClassToInject());

            Assert.AreEqual(resolved, created);
        }

        [Test]
        public void Inject_PropertyWithAttribute_PropertyIsInitialized()
        {
            injector.Register<PropertyInject, PropertyInject>();
            injector.Register<IClassToInject, ClassToInject>();

            PropertyInject resolved = injector.Resolve<PropertyInject>();

            PropertyInject created = new PropertyInject();
            created.Obj = new ClassToInject();

            Assert.AreEqual(resolved, created);
        }

        [Test]
        public void Inject_ConstructorWithSeveralParamsAndOneProperty_ReturnInstance()
        {
            injector.Register<MultyConstructorAndPropertyInject, MultyConstructorAndPropertyInject>();
            injector.Register<IClassToInject, ClassToInject>();

            MultyConstructorAndPropertyInject resolved = injector.Resolve<MultyConstructorAndPropertyInject>();

            MultyConstructorAndPropertyInject created = new MultyConstructorAndPropertyInject(new ClassToInject(), new ClassToInject());
            created.Obj = new ClassToInject();

            Assert.AreEqual(resolved, created);
        }

        [Test]
        public void Inject_ClassWithSeveralConstructor_ReturnInstanceOfMaxParamsConstructor()
        {
            injector.Register<ClassWithMultyConstructor, ClassWithMultyConstructor>();
            injector.Register<ClassToInject, ClassToInject>();

            ClassWithMultyConstructor resolved = injector.Resolve<ClassWithMultyConstructor>();

            ClassWithMultyConstructor created = new ClassWithMultyConstructor(new ClassToInject(), new ClassToInject());

            Assert.AreEqual(resolved, created);
        }

        [Test]
        public void Inject_Nothing_ReturnException()
        {
            injector.Register<IClassToInject, ClassToInject>();
            Assert.Throws<InvalidOperationException>(() => injector.Resolve<ITestInterface>());
        }

        [Test]
        public void InjectNested()
        {
            injector.Register<OnNested, OnNested>();
            injector.Register<OnConstructorAndProperty, OnConstructorAndProperty>();
            injector.Register<IClassToInject, ClassToInject>();

            OnNested resolved = injector.Resolve<OnNested>();

            OnConstructorAndProperty param = new OnConstructorAndProperty(new ClassToInject());
            param.Obj = new ClassToInject();
            OnNested created = new OnNested(param);

            Assert.AreEqual(resolved, created);
        }

        [Test]
        public void SingeltonRegister()
        {
            injector.AsSingleton();
            injector.Register<IClassToInject, ClassToSingelton>();

            IClassToInject resolved = injector.Resolve<IClassToInject>();
            IClassToInject resolved1 = injector.Resolve<IClassToInject>();

            Assert.IsTrue(object.ReferenceEquals(resolved, resolved1));
        }

        [Test]
        public void RegisterValueType()
        {
            int b = 0;
            injector.Register<IClassToInject, ClassForValueType>();
            IClassToInject resolved = injector.Resolve<IClassToInject>();

            ClassForValueType created = new ClassForValueType(b);
            Assert.AreEqual(resolved, created);

        }


    }
}
