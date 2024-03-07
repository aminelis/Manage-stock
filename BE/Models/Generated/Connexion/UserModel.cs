using System.ComponentModel.DataAnnotations;

namespace db_Api.Models.Generated.Connexion
{
    public class UserModel
    {
        [Key]
    public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

                [Required]
        public string Role { get; set; }
    }
}
