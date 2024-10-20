using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models
{
    /* Registration(Kullanıcı kayıt bilgileri) sınıfı(nesnesi):
     * Fields(Props): 
     *          Kullanıcı kimlikteki adı-soyadı(zorunlu), 
     *          Kullanıcı epostası(zorunlu-regular expression ile format validation sağlanması),
     *          Kullanıcı adı-username (zorunlu),
     *          Kullanıcı şifresi (zorunlu)
     *          Kullanıcı şifresi doğrulama (zorunlu)
     *          
     * Kullanıcıdan alınan email adresinin uyması istenilen kriterler:
     *               1'den fazla harf, rakam, nokta/tire içermeli,
     *               '@' sembolu bulunmalı,
     *               Ardından gelen alan adı birden fazla karakter içermeli,
     *               '.' sembolü gelmeli,
     *               Ardından en az 2 karakter içeren bir uzantı ile bitmeli(.com .org gibi)
     *               IP adresi formatında (örneğin [192.168.1.1] yazım da kabul edilebilir.
    */
    public class Registration
    {
        [Required(ErrorMessage = "İsim Zorunlu!")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 karakter girilebilir.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email Zorunlu!")]
        [MaxLength(100, ErrorMessage = "Maksimum 100 karakter girilebilir.")]
        [RegularExpression(@"^([\w\.-]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([\w-]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Lütfen geçerli Email giriniz.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Kullanıcı Adı Zorunlu!")]
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter girilebilir.")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Şifre Zorunlu!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Maksimum 20 ya da minimum 5 karakter girilebilir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Şifre Zorunlu!")]
        [Compare("Password", ErrorMessage = "Lütfen şifrenizi onaylayınız.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = "";
    }
}
