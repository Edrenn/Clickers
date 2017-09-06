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

        private AttackAlertCounterView alertPopUp;
        public AttackAlertCounterView AlertPopUp
        {
            get { return alertPopUp; }
            set { alertPopUp = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public InfoBarViewModel(InfoBarUserControl view)
        {
            this.AlertPopUp = new AttackAlertCounterView();
            this.View = view;
            EventGenerator();
            Token = TokenSource.Token;
            Task ennemyAttack = new Task(() =>
            {
                NextAttack();
            }, Token);
            ennemyAttack.Start();
        }

        private void EventGenerator()
        {
            View.AttackAlertButton.Click += AttackAlertButton_Click;
        }

        public void NextAttack()
        {
            Random randomizer = new Random();
            int timeToCount = randomizer.Next(120, 240);
            Thread.Sleep(new TimeSpan(0, 0, 10));
            for (int i = 0; i <= 4; i++)
            {
                if (i % 2 == 0)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => {
                        this.View.AttackAlertButton.Visibility = System.Windows.Visibility.Visible;
                    }));
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => {
                        this.View.AttackAlertButton.Visibility = System.Windows.Visibility.Collapsed;
                    }));
                }

                Thread.Sleep(600);
            }


        }

        private void AttackAlertButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AlertPopUp.Show();

            Task countdown = new Task(() =>
            {
                for (int i = 60; i < 0; i--)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() => {
                        this.AlertPopUp.TimeCounterTB.Text = i + " secondes";
                    }));
                }
            });
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
