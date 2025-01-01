using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUI.State
{
    public class BaseState
    {
        public event Action? OnChange;

        protected void NotifyStateChanged() => OnChange?.Invoke();
    }
}
