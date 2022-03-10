using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using UserDetailsApp.Contracts.Repositories;
using UserDetailsApp.Contracts.Services;
using UserDetailsApp.Models.Entities;
using UserDetailsApp.Models.Models;

namespace UserDetailsApp.Implementations.Services
{
   public class UserService : IUserService
   {
      private readonly IGenericRepository<User> _userRepository;
      public UserService(IGenericRepository<User> userRepository)
      {
         _userRepository = userRepository;
      }
      public async Task<UserModel> CreateAsync(UserModel data)
      {
         User user = data.Adapt<User>();
         User result = await _userRepository.CreateAsync(user);
         return result.Adapt<UserModel>();
      }
      public Task DeleteAsync(int Id) => _userRepository.DeleteAsync(Id);
      public async Task<IEnumerable<UserModel>> GetAllAsync()
      {
         List<User> result = (await _userRepository.GetALLAsync()).ToList();
         if(result.Count == 0)
         {
            await _userRepository.SeedData(new List<User>
            {
              new User
              {
                  Email = "abcd@squrriel.com",
                  FirstName = "Chibuikem",
                  LastName ="Akpaka",
                  Id = 1
              },
              new User
              {
                  Email = "abcd@squrriel.com",
                  FirstName = "Chibuikem",
                  LastName ="Rowland",
                  Id = 2
              }
            });
         }
         result = (await _userRepository.GetALLAsync()).ToList();
         return result.Adapt<List<UserModel>>();
      }
      public async Task<UserModel> GetAsync(int? id)
      {
         User user = await _userRepository.GetAsync(id);
         return user.Adapt<UserModel>();
      }
      public async Task<UserModel> UpdateAsync(UserModel data)
      {
         User user = data.Adapt<User>();
         User result = await _userRepository.UpdateAsync(user);
         return result.Adapt<UserModel>();
      }
   }
}