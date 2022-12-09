using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    internal class APIs
    {
        public const string baseUrl = "https://localhost:7277";

        public const string AuthenticateUser = "/api/Users/AuthenticateUser";

        public const string RegisterUser = "/api/Users/RegisterUser";
        public const string RefreshToken = "/api/Users/RefreshToken";
        public const string GetAllStundents = "/api/Students/GetAllStudents";

    }
}
