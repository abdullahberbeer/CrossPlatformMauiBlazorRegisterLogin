using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Lütfen isim giriniz..")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lütfen soyisim giriniz..")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Lütfen cinsiyet belirtin..")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Lütfen geçerli bir eposta adresi giriniz..")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen şifre giriniz..")]
        public string Password { get; set; }
       
        public string UserAvatar { get; set; } 
    }
}
