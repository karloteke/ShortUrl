using System.ComponentModel.DataAnnotations;

public class UserCreateDTO
{   
    [Required(ErrorMessage = "El campo de usuario es obligatorio.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "El campo de contraseña es obligatorio.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "El campo de correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
    public string? Email { get; set; }
}
