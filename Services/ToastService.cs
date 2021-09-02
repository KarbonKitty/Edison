using System;
using System.Timers;

namespace Edison
{
    public class ToastService : IToastService, IDisposable
    {
        public event Action<string> OnShow;
        public event Action OnHide;
        private Timer Countdown;

        public void Dispose() => Countdown?.Dispose();

        public void ShowToast(string message)
        {
            OnShow?.Invoke(message);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();

            Countdown.Stop();
            Countdown.Start();
        }

        private void SetCountdown()
        {
            if (Countdown is null)
            {
                Countdown = new Timer(5000);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }
    }
}
