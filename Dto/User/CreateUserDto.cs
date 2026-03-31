using System.ComponentModel.DataAnnotations;

namespace suprimmil.Dto.Access;

public class CreateUserDto
{
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email informado é inválido.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; } = string.Empty;

    public bool IsAdmin { get; set; } = false;
}