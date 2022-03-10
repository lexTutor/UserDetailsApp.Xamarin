using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using UserDetailsApp.Contracts.Repositories;
using UserDetailsApp.Contracts.Services;
using UserDetailsApp.Core.Services;
using UserDetailsApp.Core.Utilities;
using UserDetailsApp.Implementations.Repositories;
using UserDetailsApp.Implementations.Services;

namespace UserDetailsApp.Core.DIContainer
{
   public class AppContainer
   {
      public static IContainer _container;

      public static void ConfigureServices()
      {
         var builder = new ContainerBuilder();

         //Services
         builder.RegisterType<UserService>().As<IUserService>();
         builder.RegisterType<DialogService>().As<IDialogService>();
         builder.RegisterType<ImageService>().As<IIMageService>();

         //Repository
         builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

         //dynamically register ViewModels
         IEnumerable<Type> types = Reflection.GetViewModels();
         foreach(Type type in types)
         {
            builder.RegisterType(type);
         }
         _container = builder.Build();
      }
      public static object Resolve(Type typeName)
      {
         return _container.Resolve(typeName);
      }

      public static T Resolve<T>()
      {
         return _container.Resolve<T>();
      }
   }
}
