using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserDetailsApp.Models.Entities;
using UserDetailsApp.Models.Models;

namespace UserDetailsApp.Contracts.Services
{
   public interface IUserService
   {
      Task DeleteAsync(int Id);
      Task<UserModel> GetAsync(int? id);
      Task<IEnumerable<UserModel>> GetAllAsync();
      Task<UserModel> CreateAsync(UserModel data);
      Task<UserModel> UpdateAsync(UserModel data);
   }
}