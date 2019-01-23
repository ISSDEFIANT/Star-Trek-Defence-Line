using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Utils
{
    public class Debouncer : Singleton<Debouncer>
    {
        private readonly Dictionary<object, TimedAction> _actions = new Dictionary<object, TimedAction>();
        private readonly object _lock = new object();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public void Debounce(object key, Action action, long time)
        {
            lock (_lock)
                _actions[key] = new TimedAction(action, time);
        }

        public void Cancel(object key)
        {
            lock (_lock)
                _actions[key] = null;
        }

        private void OnDestroy()
        {
            lock (_lock)
                foreach (var action in _actions)
                    action.Value.Run();
        }

        private void Awake()
        {
            _stopwatch.Start();
        }

        private void Update()
        {
            _stopwatch.Stop();
            var elapsed = _stopwatch.ElapsedMilliseconds;

            List<KeyValuePair<object, TimedAction>> actions = new List<KeyValuePair<object, TimedAction>>();
            lock (_lock)
            {
                foreach (var action in _actions)
                {
                    action.Value.Time -= elapsed;
                    if (action.Value.Time <= 0)
                        actions.Add(action);
                }

                foreach (var kv in actions)
                    _actions.Remove(kv.Key);
            }

            foreach (var action in actions)
                action.Value.Run();

            _stopwatch.Reset();
            _stopwatch.Start();
        }

        private class TimedAction
        {
            private readonly Action _action;
            public long Time;

            public TimedAction(Action action, long time)
            {
                _action = action;
                Time = time;
            }

            public void Run()
            {
                ThreadPool.QueueUserWorkItem(state =>
                {
                    try
                    {
                        _action.Invoke();
                    }
                    catch (Exception e)
                    {
                        ErrorWindow.ShowException(e);
                    }
                });
            }
        }
    }
}