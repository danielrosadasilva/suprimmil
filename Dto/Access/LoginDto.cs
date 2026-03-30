using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace suprimmil.Dto.Access;

public class LoginDto
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email informado é inválido.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; } = string.Empty;
}