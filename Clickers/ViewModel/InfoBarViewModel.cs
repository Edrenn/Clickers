using Clickers.Views;
using Clickers.Views.AttackAlertView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Clickers.ViewModel
{
    public class InfoBarViewModel
    {
        private InfoBarUserControl view;
        public InfoBarUserControl View { get; set; }

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        public CancellationTokenSource TokenSource
        {
            get
            {
                return tokenSource;
            }
            set
            {
                tokenSource = value;
            }

        }

        private CancellationToken token;
        public CancellationToken Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
                RaisePropertyChanged("Token");
            }
        }

        private bool attackingSoon;
        public bool AttackingSoon
        {
            get
            {
                return attackingSoon;
            }
            set
            {
                attackingSoon = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public InfoBarViewModel(InfoBarUserControl view)
        {
            this.View = view;
            EventGenerator();
            Token = TokenSource.Token;
            Task ennemyAttack = new Task(() =>
            {
                PreparingEnemyAttack();
            }, Token);
            ennemyAttack.Start();
        }

        private void EventGenerator()
        {
            View.AttackAlertButton.Click += AttackAlertButton_Click;
        }

        private void AttackAlertButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AttackAlertCounterView altertCounter = new AttackAlertCounterView();
            altertCounter.Show();
        }

        private void PreparingEnemyAttack()
        {
            Random rd = new Random();
            Double timeBeforeNextAttack = rd.Next(3, 5);
            Thread.Sleep(TimeSpan.FromSeconds(5));
            AttackingSoon = true;

        }

        void StartCalc()
        {
            var calc = new InfoBarUserControl();
        }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
