
using System.ComponentModel.DataAnnotations;

namespace SchoolMaterials.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Името е задължително.")]
    [StringLength(100)]
    [Display(Name = "Име и фамилия")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имейлът е задължителен.")]
    [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
    [Display(Name = "Имейл")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Паролата е задължителна.")]
    [MinLength(8, ErrorMessage = "Паролата трябва да е поне 8 символа.")]
    [DataType(DataType.Password)]
    [Display(Name = "Парола")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Потвърди паролата.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Паролите не съвпадат.")]
    [Display(Name = "Потвърди паролата")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
