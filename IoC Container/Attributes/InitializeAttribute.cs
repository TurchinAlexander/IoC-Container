using System;

namespace IoC_Container.Attributes
{
    /// <summary>
    /// Attribute that is used to mark a property for initializing. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InitializeAttribute : Attribute
    {
    }
}
