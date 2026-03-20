using System.ComponentModel.DataAnnotations;

namespace suprimmil.Dto.Access;

public class LoginDto
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email informado é inválido.")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; } = null!;
}