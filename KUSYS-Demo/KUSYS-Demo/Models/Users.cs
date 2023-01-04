using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
