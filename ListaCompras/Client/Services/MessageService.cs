using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace ListaCompras.Client.Services
{
    public class MessageService
    {
        public enum MessageType
        {
            INFO = 0,
            SUCCESS = 1,
            WARNING = 2,
            DANGER = 3
        }
        public class MessageArgs : EventArgs
        {
            public string Message { get; set; }
            public int Duration { get; set; }
            public MessageType MessageType { get; set; }
        }
        public event EventHandler OnShowMessage;
        public event EventHandler OnHideMessage;
        private Timer timer;

        public void ShowMessage(string message, MessageType? type = 0, int? duration = 10000)
        {
            var args = new MessageArgs()
            {
                Message = message,
                Duration = duration.Value,
                MessageType = type.Value
            };

            OnShowMessage?.Invoke(this, args);

            timer?.Stop();
            timer = new Timer(args.Duration);
            timer.Elapsed += HideMessage;
            timer.AutoReset = false;
            timer.Start();
        }

        public void HideMessage(object sender, EventArgs e)
        {
            OnHideMessage?.Invoke(sender, e);
        }
    }
}
