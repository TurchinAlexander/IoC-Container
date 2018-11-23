# IoC-Container
A very small IoC container in C#.NET.

It's simple, Here are the steps:
1. Define your types and implementations.
2. Call Resolve method of the container to get desired implementation.
3. Use it!

So let's get started!

**1. Register your types like as follows:**

      var injector = Injector.CreateInstance();
      container.Register<Animal, Dog>();

**2. Then resolve and get an instance of your type like this:**

       var animal = container.Resolve<Animal>();

**3. It's done! Enjoy it.**

       animal.Voice();

You can use Singleton in order to prevent instantiating classes more than once. To do so, add the **AsSingleton()** into your container, otherwise use **AsTransient()**:

    var container = Injector.CreateInstance();

    // return same instances;
    container.AsSingleton();

    //return unique instances;
    container.AsTransient();

By doing this, all of the objects just creating once.  
Also when you want to initialize the property, use **[Initialize]** attribute, like:

    public class JustClass
    {
      [Initialize]
      public anotherClass Property {get; set;}

      public JustClass() {}
    }
