using System.Net.Http.Json;
using Common.Models.User;
using Common.Response;

namespace AppUI.Services
{
    public class AuthService(HttpClient http)
    {
        private const string AUTH_BASE_URL = "https://localhost:5001/api/auth"; // UserService endpoint

        public async Task<BaseResponse<string>> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new Login 
                { 
                    Username = username, 
                    Password = password 
                };

                var response = await http.PostAsJsonAsync($"{AUTH_BASE_URL}/Login", loginRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
                    return result ?? new BaseResponse<string> 
                    { 
                        Status = Common.Enums.EHttpStatus.INTERNAL_SERVER_ERROR,
                        ResponseMessage = "Failed to parse response"
                    };
                }
                else
                {
                    return new BaseResponse<string>
                    {
                        Status = Common.Enums.EHttpStatus.UNAUTHORIZED,
                        ResponseMessage = "Invalid credentials"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    Status = Common.Enums.EHttpStatus.INTERNAL_SERVER_ERROR,
                    ResponseMessage = ex.Message
                };
            }
        }

        public async Task<BaseResponse<bool>> VerifyTokenAsync(string token)
        {
            try
            {
                var response = await http.GetAsync($"{AUTH_BASE_URL}/VerifyToken?token={token}");
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<BaseResponse<bool>>();
                    return result ?? new BaseResponse<bool> 
                    { 
                        Status = Common.Enums.EHttpStatus.INTERNAL_SERVER_ERROR,
                        ResponseMessage = "Failed to parse response"
                    };
                }
                else
                {
                    return new BaseResponse<bool>
                    {
                        Status = Common.Enums.EHttpStatus.UNAUTHORIZED,
                        ResponseMessage = "Invalid token"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Status = Common.Enums.EHttpStatus.INTERNAL_SERVER_ERROR,
                    ResponseMessage = ex.Message
                };
            }
        }
    }
}