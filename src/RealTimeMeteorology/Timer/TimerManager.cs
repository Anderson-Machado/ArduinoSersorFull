using System;
using System.Threading;


namespace RealTimeMeteorology.Timer
{
    public class TimerManager
    {
        private System.Threading.Timer? _timer;
        private AutoResetEvent? _autoResetEvent;
        private Action? _action;
        public DateTime TimerStarted { get; set; }
        public bool IsTimerStarted { get; set; }

        public void PrepareTimer(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new System.Threading.Timer(Execute, _autoResetEvent, 10000, 20000);
            TimerStarted = DateTime.Now;
            IsTimerStarted = true;
        }

        public void Execute(object? stateInfo)
        {
            _action();
            if ((DateTime.Now - TimerStarted).TotalSeconds > 360)
            {
                IsTimerStarted = false;
                _timer.Dispose();
            }
        }
    }
}
