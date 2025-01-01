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

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                NotifyStateChanged();
            }
        }
    }
}
