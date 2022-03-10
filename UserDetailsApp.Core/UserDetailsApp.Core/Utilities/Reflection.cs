using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UserDetailsApp.Core.ViewModels;

namespace UserDetailsApp.Core.Utilities
{
   public class Reflection
   {
      public static IEnumerable<Type> GetViewModels() => Assembly.GetExecutingAssembly().GetTypes()
                          .Where(type => !string.IsNullOrEmpty(type.Namespace) && !type.IsInterface && !type.IsAbstract)
                          .Where(type => type.BaseType != null && typeof(BaseViewModel).IsAssignableFrom(type));

      //public static IEnumerable<Type> GetPages() => Assembly.GetExecutingAssembly().GetTypes()
      //                    .Where(type => !string.IsNullOrEmpty(type.Namespace) && !type.IsInterface && !type.IsAbstract)
      //                    .Where(type => type.BaseType != null && typeof(Page).IsAssignableFrom(type));

      public static Type GetTypeFromPage(Type page)
      {
         string viewModelName = page.Name + "Model";
         return GetViewModels().FirstOrDefault(type => type.Name == viewModelName);
      }
   }
}
