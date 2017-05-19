using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Clickers.Views.AttackAlertView;

namespace Clickers.ViewModel
{
    public class AttackAlertViewModel
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        AttackAlertCounterView view;

        public AttackAlertViewModel(AttackAlertCounterView view)
        {
            _time = TimeSpan.FromSeconds(60);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                view.TimeCounterTB.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
    }
}
