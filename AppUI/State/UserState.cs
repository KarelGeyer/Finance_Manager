using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUI.State
{
    public class UserState : BaseState
    {
        private int _userId = 1;
        private bool _isAuthenticated = false;
        private string _token = string.Empty;
        private string _username = string.Empty;

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
            set
            {
                _isAuthenticated = value;
                NotifyStateChanged();
            }
        }

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                NotifyStateChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyStateChanged();
            }
        }

        public void SetAuthenticatedUser(string token, string username, int userId)
        {
            Token = token;
            Username = username;
            UserId = userId;
            IsAuthenticated = true;
        }

        public void ClearAuthentication()
        {
            Token = string.Empty;
            Username = string.Empty;
            UserId = 0;
            IsAuthenticated = false;
        }
    }
}
