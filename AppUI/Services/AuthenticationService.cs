using Common.Models.User;
using Common.Response;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AppUI.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001/api/auth"; // UserService base URL

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Attempts to log in the user with username and password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>BaseResponse containing JWT token if successful</returns>
        public async Task<BaseResponse<string>> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new Login
                {
                    Username = username,
                    Password = password
                };

                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/Login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = JsonSerializer.Deserialize<BaseResponse<string>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return loginResponse ?? new BaseResponse<string>();
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
                    ResponseMessage = $"Authentication failed: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Verifies if the provided token is valid
        /// </summary>
        /// <param name="token">JWT token to verify</param>
        /// <returns>BaseResponse indicating if token is valid</returns>
        public async Task<BaseResponse<bool>> VerifyTokenAsync(string token)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/VerifyToken?token={Uri.EscapeDataString(token)}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var verifyResponse = JsonSerializer.Deserialize<BaseResponse<bool>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return verifyResponse ?? new BaseResponse<bool>();
                }
                else
                {
                    return new BaseResponse<bool>
                    {
                        Status = Common.Enums.EHttpStatus.UNAUTHORIZED,
                        ResponseMessage = "Token verification failed"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Status = Common.Enums.EHttpStatus.INTERNAL_SERVER_ERROR,
                    ResponseMessage = $"Token verification failed: {ex.Message}"
                };
            }
        }
    }
}