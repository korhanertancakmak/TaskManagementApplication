using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models
{
    /* Login(Kullanıcı giriş bilgileri) sınıfı(nesnesi):
     * Fields(Props): 
     *          Kullanıcı adı-username ya da epostası(zorunlu),
     *          Kullanıcı şifresi (zorunlu)
    */
    public class Login
    {

        [Required(ErrorMessage = "Username ya da Email Zorunlu!")]
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter girilebilir.")]
        [DisplayName("Username/Email")]
        public string UserNameOrEmail { get; set; } = "";

        [Required(ErrorMessage = "Şifre Zorunlu!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Maksimum 20 ya da minimum 5 karakter girilebilir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
    }
}
