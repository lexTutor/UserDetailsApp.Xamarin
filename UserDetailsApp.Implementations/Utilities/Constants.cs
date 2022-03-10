using System;
using System.IO;

namespace UserDetailsApp.Implementations.Utilities
{
   public static class DbConstants
   {
      public const string DatabaseFilename = "UserDetailsApp.db3";

      public static string DatabasePath
      {
         get
         {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DatabaseFilename);
         }
      }
   }
}