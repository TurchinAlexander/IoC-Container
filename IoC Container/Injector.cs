using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using IoC_Container.Interfaces;
using IoC_Container.Attributes;
using IoC_Container.Exceptions;

namespace IoC_Container
{
    public sealed class Injector : ISimpleIoC, IDisposable
    {
        private Dictionary<Type, Type> registeredTypes =
            new Dictionary<Type, Type>();

        private Dictionary<Type, object> singletons =
            new Dictionary<Type, object>();

        private bool isSingletonMode = false;
        private Stack<Type> trace = new Stack<Type>();

        /// <summary>
        /// Register types.
        /// </summary>
        /// <typeparam name="TKey">From type.</typeparam>
        /// <typeparam name="TConcrete">Call this implemetation.</typeparam>
        public void Register<TKey, TConcrete>() where TConcrete : TKey
        {
            this.registeredTypes[typeof(TKey)] = typeof(TConcrete);
        }

        /// <summary>
        /// Get instance.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <returns>The instance of the <typeparamref name="T"/>.</returns>
        public T Resolve<T>() where T : class
        {
            if (this.registeredTypes.Count == 0)
            {
                throw new InvalidOperationException($"There is nothing in {nameof(Injector)}");
            }

            T instance = (T)this.ResolveParameter(typeof(T));

            return instance;
        }

        /// <summary>
        /// Use singleton for instantiating.
        /// </summary>
        public void AsSingleton()
        {
            this.isSingletonMode = true;
        }

        /// <summary>
        /// Don't use singleton for instantiating.
        /// </summary>
        public void AsTransient()
        {
            this.isSingletonMode = false;
        }

        /// <summary>
        /// Check if specified type is registered.
        /// </summary>
        /// <typeparam name="Type">The type that you want to check.</typeparam>
        /// <returns><c>true</c> if <typeparamref name="Type"/> is contained. Otherwise, <c>false</c>.</returns>
        public bool IsRegistered<Type>()
        {
            return this.registeredTypes.ContainsKey(typeof(Type));
        }

        public void Dispose()
        {
            foreach (var singleton in singletons.Values)
            {
                (singleton as IDisposable)?.Dispose();
            }
        }

        private object ResolveParameter(Type type)
        {
            if (!this.registeredTypes.ContainsKey(type))
            {
                throw new InvalidOperationException($"Here is no implementation for {nameof(type)}");
            }

            if (type.GetType().IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            var instance = this.TryGetInstanceIfSingleton(type);
            if (instance != null)
            {
                return instance;
            }

            if (this.trace.Contains(type))
            {
                throw new CircularDependencyException(nameof(type));

            }

            this.trace.Push(type);

            Type typeToInstantiate = this.registeredTypes[type];

            var constructor = GetConstructorWithLongestParameterList(typeToInstantiate);
            var arguments = GetConstructorArguments(constructor);

            instance = Activator.CreateInstance(typeToInstantiate, arguments);

            this.Initialize(instance);
            this.SaveInstanceIfSingleton(type, instance);

            this.trace.Pop();

            return instance;
        }

        private void Initialize(object value)
        {
            foreach (var prop in value.GetType().GetProperties())
            {
                if (NeedInitialization(prop))
                {
                    prop.SetValue(value, ResolveParameter(prop.PropertyType));
                }
            }
        }

        private bool NeedInitialization(PropertyInfo property)
       => property.GetCustomAttributes(typeof(InitializeAttribute), false).Length > 0 ? true : false;

        private object TryGetInstanceIfSingleton(Type type)
        {
            return this.singletons.TryGetValue(type, out var singleton) ? singleton : null;
        }

        private void SaveInstanceIfSingleton(Type type, object instance)
        {
            if (this.isSingletonMode && !singletons.ContainsKey(type))
                singletons[type] = instance;
        }

        private static ConstructorInfo GetConstructorWithLongestParameterList(Type type) =>
           type.GetConstructors()
               .OrderByDescending(c => c.GetParameters().Length)
               .FirstOrDefault();


        private object[] GetConstructorArguments(ConstructorInfo constructor)
        {
            return constructor.GetParameters()
                .Select(param => ResolveParameter(param.ParameterType))
                .ToArray();
        }
    }
}

