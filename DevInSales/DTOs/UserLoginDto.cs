using System.ComponentModel.DataAnnotations;

namespace DevInSales.DTOs
{
    /// <summary>
    /// DTO que representa as informações de login do usuário
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// O id do usuário
        /// </summary>
        [Display(Name = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
