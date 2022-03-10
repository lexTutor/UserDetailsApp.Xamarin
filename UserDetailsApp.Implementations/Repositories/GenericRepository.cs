using System.Collections.Generic;
using System.Threading.Tasks;
using UserDetailsApp.Contracts.Repositories;

namespace UserDetailsApp.Implementations.Repositories
{
   public class GenericRepository<T> : IGenericRepository<T> where T : new()
   {
      public GenericRepository()
      {
         DataContext.InitializeAsync<T>();
      }
      public async Task DeleteAsync(int Id) => await DataContext.Database.DeleteAsync<T>(Id);
      public async Task<T> GetAsync(int? id) => await DataContext.Database.GetAsync<T>(id);
      public async Task<T> CreateAsync(T data)
      {
         await DataContext.Database.InsertAsync(data);
         return data;
      }
      public async Task<T> UpdateAsync(T data)
      {
         await DataContext.Database.UpdateAsync(data);
         return data;
      }
      public async Task<IEnumerable<T>> GetALLAsync() => await DataContext.Database.Table<T>().ToListAsync();
      public async Task SeedData(IList<T> Seed) => await DataContext.Database.InsertAllAsync(Seed);
   }
}