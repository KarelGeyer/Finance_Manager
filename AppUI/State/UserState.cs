using AppUI.Helpers.Enums;

namespace AppUI.State
{
    public class UserState : BaseState
    {
        private int _userId = 0;
        private bool _isAuthenticated = false;
        private string? _token = null;
        private string? _username = null;
        private EResult _authResultState = EResult.None;
        private string? _authErrorMessage = null;

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                NotifyStateChanged();
            }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            private set
            {
                _isAuthenticated = value;
                NotifyStateChanged();
            }
        }

        public string? Token
        {
            get { return _token; }
            private set
            {
                _token = value;
                NotifyStateChanged();
            }
        }

        public string? Username
        {
            get { return _username; }
            private set
            {
                _username = value;
                NotifyStateChanged();
            }
        }

        public EResult AuthResultState
        {
            get { return _authResultState; }
            set
            {
                _authResultState = value;
                NotifyStateChanged();
            }
        }

        public string? AuthErrorMessage
        {
            get { return _authErrorMessage; }
            set
            {
                _authErrorMessage = value;
                NotifyStateChanged();
            }
        }

        /// <summary>
        /// Sets the user as authenticated with the provided token and username
        /// </summary>
        public void SetAuthenticated(string token, string username, int userId = 0)
        {
            Token = token;
            Username = username;
            UserId = userId;
            IsAuthenticated = true;
            AuthResultState = EResult.Success;
            AuthErrorMessage = null;
        }

        /// <summary>
        /// Logs out the user and clears authentication state
        /// </summary>
        public void Logout()
        {
            Token = null;
            Username = null;
            UserId = 0;
            IsAuthenticated = false;
            AuthResultState = EResult.None;
            AuthErrorMessage = null;
        }

        /// <summary>
        /// Sets authentication failure state with error message
        /// </summary>
        public void SetAuthenticationFailed(string errorMessage)
        {
            AuthResultState = EResult.Error;
            AuthErrorMessage = errorMessage;
            IsAuthenticated = false;
        }

        /// <summary>
        /// Clears any authentication error messages
        /// </summary>
        public void ClearAuthError()
        {
            AuthResultState = EResult.None;
            AuthErrorMessage = null;
        }
    }
}
