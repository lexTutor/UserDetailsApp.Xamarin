using SQLite;

namespace UserDetailsApp.Models.Entities
{
   public class User
   {
      [PrimaryKey]
      public int Id { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Sex { get; set; }
      public string Address { get; set; }
      public string PhoneNumber { get; set; }
      public string PicturePath { get; set; }
      public string Email { get; set; }
   }
}