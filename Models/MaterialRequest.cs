
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolMaterials.Models;

// Represents a material request submitted by a school teacher.
public class MaterialRequest
{
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string RequestNumber { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 10000)]
    public int Quantity { get; set; }

    [Required]
    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    [StringLength(500)]
    public string? AdminComment { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
}

public enum RequestStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}
