using System.ComponentModel.DataAnnotations;

namespace GerenciadorCondominios.ViewModels
{
  public class LoginViewModels
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }


  }
}