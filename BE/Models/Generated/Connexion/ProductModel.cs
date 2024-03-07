using System.ComponentModel.DataAnnotations;

namespace db_Api.Models.Generated.Product
{
    public class ProduitModel
    {
        [Key]
    public int Id { get; set; }

        [Required]
        public string CodePdt { get; set; }

        public string? Produit { get; set; }
        public string? Prix { get; set; }
    }
}
