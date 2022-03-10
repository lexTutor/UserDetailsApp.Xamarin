using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserDetailsApp.Contracts.Repositories
{
   public interface IGenericRepository<T>
   {
      Task DeleteAsync(int Id);
      Task<T> GetAsync(int? id);
      Task<T> CreateAsync(T data);
      Task<T> UpdateAsync(T data);
      Task<IEnumerable<T>> GetALLAsync();
      Task SeedData(IList<T> Seed);
   }
}