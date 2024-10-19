﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models.Entities
{
    /* UserAccount(Kullanıcı hesabı) sınıfı(nesnesi):
     * Fields(Props): 
     *          Eşsiz(unique) kimlik, 
     *          Kullanıcı kimlikteki adı-soyadı(zorunlu), 
     *          Kullanıcı epostası(zorunlu),
     *          Kullanıcı adı-username (zorunlu),
     *          Kullanıcı şifresi (zorunlu)
    */
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class UserAccount
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim Zorunlu!")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 karakter girilebilir.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email Zorunlu!")]
        [MaxLength(100, ErrorMessage = "Maksimum 100 karakter girilebilir.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Kullanıcı Adı Zorunlu!")]
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter girilebilir.")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Şifre Zorunlu!")]
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter girilebilir.")]
        public string Password { get; set; } = "";
    }
}
