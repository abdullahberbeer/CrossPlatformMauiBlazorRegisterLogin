using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adı giriniz..")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen şifre giriniz..")]
        public string Password { get; set; }
    }
}
