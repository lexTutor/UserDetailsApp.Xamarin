using SQLite;
using UserDetailsApp.Implementations.Utilities;

namespace UserDetailsApp.Implementations.Repositories
{
   public static class DataContext
   {
      public static SQLiteAsyncConnection Database;
      public static async  void InitializeAsync<T>() where T : new()
      {
         Database = new SQLiteAsyncConnection(DbConstants.DatabasePath);
         await Database.CreateTableAsync<T>(CreateFlags.AutoIncPK);
      }
   }
}