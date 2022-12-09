using MauiApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class AppService : IAppService
    {
        
        public async Task<MainResponse> AuthenticateUser(LoginModel loginModel)
        {
            var returnResponse = new MainResponse();
            using (var client = new HttpClient())
            {
                var url = $"{APIs.baseUrl}{APIs.AuthenticateUser}";
                var serializedObject = JsonConvert.SerializeObject(loginModel);
                

               var response= await client.PostAsync(url,new StringContent(serializedObject,Encoding.UTF8,"application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    returnResponse=JsonConvert.DeserializeObject<MainResponse>(contentStr);
                }
            }
            return returnResponse;
        }
        public async Task<(bool IsSuccess,string ErrorMessage)> RegisterUser(RegisterModel registerModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess=false;
            using (var client = new HttpClient())
            {
                var url = $"{APIs.baseUrl}{APIs.RegisterUser}";
                var serializedObject = JsonConvert.SerializeObject(registerModel);


                var response = await client.PostAsync(url, new StringContent(serializedObject, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    isSuccess = true;
                }
                else
                {
                    errorMessage =await response.Content.ReadAsStringAsync();
                }
            }
            return (isSuccess,errorMessage);
        }
        public async Task<bool> RefreshToken()
        {
            bool isTokenRefreshed= false;
            
            using (var client = new HttpClient())
            {
                var url = $"{APIs.baseUrl}{APIs.RefreshToken}";
                var serializedObject = JsonConvert.SerializeObject(new AuthenticateRequestandResponse
                {
                    RefreshToken = Setting.UserDetail.RefreshToken,
                    AccessToken = Setting.UserDetail.AccessToken
                });


                var response = await client.PostAsync(url, new StringContent(serializedObject, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string contentStr = await response.Content.ReadAsStringAsync();
                    var mainResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);
                    if (mainResponse.IsSuccess)
                    {
						var tokenDetails = JsonConvert.DeserializeObject<AuthenticateRequestandResponse>(mainResponse.Content.ToString());
						Setting.UserDetail.AccessToken = tokenDetails.AccessToken;
						Setting.UserDetail.RefreshToken = tokenDetails.RefreshToken;

						string userDetailsStr = JsonConvert.SerializeObject(Setting.UserDetail);

						await SecureStorage.SetAsync(nameof(Setting.UserDetail), userDetailsStr);

					  isTokenRefreshed= true;   
					}
                }
            }
            return isTokenRefreshed;

		}
        public async Task<List<StudentModel>> GetAllStudents()
        {
            var returnResponse = new List<StudentModel>();
            using (var client = new HttpClient())
            {
                var url = $"{APIs.baseUrl}{APIs.GetAllStundents}";

                client.DefaultRequestHeaders.Add("Authorization",$"Bearer {Setting.UserDetail?.AccessToken}");

                 var response = await client.GetAsync(url);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    bool isTokenRefreshed = await RefreshToken();
                    if (isTokenRefreshed) return await GetAllStudents();
                    
                    
                }
                else
                {
					if (response.IsSuccessStatusCode)
					{
						string contentStr = await response.Content.ReadAsStringAsync();
						var mainResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);

						if (mainResponse.IsSuccess)
						{
							returnResponse = JsonConvert.DeserializeObject<List<StudentModel>>(mainResponse.Content.ToString());
						}
					}
				}

               
            }
            return returnResponse;
        }
    }
}
