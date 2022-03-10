using System;
using System.Collections.Generic;
using System.Text;
using Mapster;
using UserDetailsApp.Models.Entities;
using UserDetailsApp.Models.Models;

namespace UserDetailsApp.Implementations.Utilities
{
   public static class Mappings
   {
      public static void Configure()
      {
         TypeAdapterConfig<User, UserModel>
             .NewConfig();
      }
   }
}
