using System.ComponentModel.DataAnnotations;

namespace SchoolMaterials.Models
{
    public class MaterialRequest
    {
        public int Id { get; set; }
        public string? RequestNumber { get; set; }
        public DateTime Date { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        public string? Status { get; set; }
        
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}