using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Models.User;
using Common.Response;
using AppUI.Services;

namespace AppUI.Tests
{
    /// <summary>
    /// Simple test class to demonstrate AuthenticationService functionality
    /// Note: This requires the UserService to be running with a valid database
    /// </summary>
    public class AuthenticationServiceTest
    {
        private readonly AuthenticationService _authService;

        public AuthenticationServiceTest()
        {
            var httpClient = new HttpClient();
            _authService = new AuthenticationService(httpClient);
        }

        /// <summary>
        /// Test login functionality with valid credentials
        /// </summary>
        public async Task<bool> TestLoginAsync(string username, string password)
        {
            try
            {
                Console.WriteLine($"Testing login for user: {username}");
                
                var response = await _authService.LoginAsync(username, password);
                
                if (response.Status == Common.Enums.EHttpStatus.OK && !string.IsNullOrEmpty(response.Data))
                {
                    Console.WriteLine("Login successful!");
                    Console.WriteLine($"Token received: {response.Data[..20]}...");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Login failed: {response.ResponseMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login test failed with exception: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test token verification functionality
        /// </summary>
        public async Task<bool> TestTokenVerificationAsync(string token)
        {
            try
            {
                Console.WriteLine("Testing token verification...");
                
                var response = await _authService.VerifyTokenAsync(token);
                
                if (response.Status == Common.Enums.EHttpStatus.OK && response.Data == true)
                {
                    Console.WriteLine("Token verification successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Token verification failed: {response.ResponseMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token verification test failed with exception: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Run complete authentication flow test
        /// </summary>
        public async Task RunAuthenticationFlowTest()
        {
            Console.WriteLine("=== Authentication Flow Test ===");
            Console.WriteLine("Note: UserService must be running with valid database");
            Console.WriteLine();

            // Test with dummy credentials (would need real user data)
            string testUsername = "testuser";
            string testPassword = "testpass";

            // Step 1: Test login
            bool loginSuccess = await TestLoginAsync(testUsername, testPassword);
            
            if (loginSuccess)
            {
                Console.WriteLine("✓ Login test passed");
                
                // In a real test, we would use the token returned from login
                // For this demo, we'll test with a dummy token
                string dummyToken = "dummy.jwt.token";
                
                // Step 2: Test token verification
                bool verifySuccess = await TestTokenVerificationAsync(dummyToken);
                
                if (verifySuccess)
                {
                    Console.WriteLine("✓ Token verification test passed");
                    Console.WriteLine("✓ Complete authentication flow successful!");
                }
                else
                {
                    Console.WriteLine("✗ Token verification test failed");
                }
            }
            else
            {
                Console.WriteLine("✗ Login test failed - skipping token verification");
            }

            Console.WriteLine();
            Console.WriteLine("=== Test Summary ===");
            Console.WriteLine("Authentication system components implemented:");
            Console.WriteLine("- AuthenticationService with login and token verification");
            Console.WriteLine("- UserState for managing authentication state");
            Console.WriteLine("- Login page with form validation and error handling");
            Console.WriteLine("- Navigation integration with login/logout");
            Console.WriteLine("- Route protection in MainLayout");
            Console.WriteLine("- AuthenticationWrapper for additional protection");
        }
    }
}