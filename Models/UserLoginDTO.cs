using System.ComponentModel.DataAnnotations;

public class UserLoginDTO
{   
    [Required(ErrorMessage = "El campo de usuario es obligatorio.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "El campo de contraseña es obligatorio.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }
}
