using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IAppService
    {
        public Task<MainResponse> AuthenticateUser(LoginModel loginModel);
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegisterModel registerModel);
        Task<bool> RefreshToken();
        Task<List<StudentModel>> GetAllStudents();
    }
}
