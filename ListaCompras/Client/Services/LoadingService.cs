using System;

namespace ListaCompras.Client.Services
{
    public class LoadingService
    {
        public class LoadingEventArgs : EventArgs
        {
            public bool Show { get; set; }
        }
        public event EventHandler OnLoadingEvent;

        public void StartLoading()
        {
            var args = new LoadingEventArgs() { Show = true };
            OnLoadingEvent?.Invoke(this, args);
        }

        public void StopLoading()
        {
            var args = new LoadingEventArgs() { Show = false };
            OnLoadingEvent?.Invoke(this, args);
        }
    }
}
