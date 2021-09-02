using System;

namespace Edison
{
    public interface IToastService
    {
        event Action<string> OnShow;
        event Action OnHide;
        void ShowToast(string message);
    }
}
