using Clickers.Views;
using Clickers.Views.AttackAlertView;
using Clickers.ViewModel.Army;
using Clickers.Models;
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
        public InfoBarUserControl View
        {
            get { return view; }
            set { view = value; }
        }


        private CancellationTokenSource tokenSource;
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

        private static InfoBarViewModel _instance;
        public static InfoBarViewModel _Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InfoBarViewModel();
                }
                return _instance;
            }
        }


        private InfoBarViewModel()
        {
            this.AlertPopUp = new AttackAlertCounterView();
            this.View = new InfoBarUserControl();
            this.View.DataContext = GameViewModel.Instance;
            EventGenerator();
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
            this.TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            Random randomizer = new Random();
            int timeToCount = randomizer.Next(100, 240);
            Thread.Sleep(new TimeSpan(0, 0, 10));
            for (int i = 0; i <= 4; i++)
            {
                Thread.Sleep(600);
                if (i % 2 == 0)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.View.AttackAlertButton.Visibility = System.Windows.Visibility.Visible;
                    }));
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.View.AttackAlertButton.Visibility = System.Windows.Visibility.Collapsed;
                    }));
                }

            }
            for (int i = 10; i >= 0; i--)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    this.AlertPopUp.TimeCounterTB.Text = i.ToString();
                }));
                Thread.Sleep(new TimeSpan(0, 0, 1));
            }

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                this.View.AttackAlertButton.Visibility = System.Windows.Visibility.Collapsed;
                this.AlertPopUp.Visibility = System.Windows.Visibility.Collapsed;
            }));

            EnnemyAttack();
        }

        private void AttackAlertButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AlertPopUp.Show();
        }

        private void EnnemyAttack()
        {
            if (!this.TokenSource.IsCancellationRequested)
            {
                if (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count > 5)
                {
                    GameViewModel.Instance.EnnemyCastle.Army.GenerateEnnemyArmy();
                    Random rng = new Random();
                    rng.Next(0, 100);
                    if (rng.Next(0, 100) > 30)
                    {
                        GameViewModel.Instance.EnnemyCastle.Army.GenerateHero();
                    }
                    if (GameViewModel.Instance.MainCastle.Army.Hero != null && GameViewModel.Instance.EnnemyCastle.Army.Hero != null)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            HeroFightViewModel newDuel = new HeroFightViewModel(GameViewModel.Instance.EnnemyCastle, GameViewModel.Instance.MainCastle, true);
                        }));
                    }
                    else if (GameViewModel.Instance.MainCastle.Army.Hero != null)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Battle newBattle = new Battle(GameViewModel.Instance.EnnemyCastle.Army, GameViewModel.Instance.MainCastle.Army.BoostArmy(), GameViewModel.Instance.MainCastle, GameViewModel.Instance.EnnemyCastle, true);
                        }));
                    }
                    else if (GameViewModel.Instance.EnnemyCastle.Army.Hero != null)
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Battle newBattle = new Battle(GameViewModel.Instance.EnnemyCastle.Army.BoostArmy(), GameViewModel.Instance.MainCastle.Army, GameViewModel.Instance.MainCastle, GameViewModel.Instance.EnnemyCastle, true);
                        }));
                    }
                    else
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            Battle newBattle = new Battle(GameViewModel.Instance.EnnemyCastle.Army, GameViewModel.Instance.MainCastle.Army, GameViewModel.Instance.MainCastle, GameViewModel.Instance.EnnemyCastle, true);
                        }));
                    }
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        GameViewModel.Instance.MainCastle.Life -= 25;
                        BattleReport newBattleReport = new BattleReport();
                        newBattleReport.WinOrLoseLabel.Content = "Défaite";
                        System.Windows.Controls.TextBlock newTB = new System.Windows.Controls.TextBlock() { Text = "Votre armée n'était pas prête" };
                        newTB.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        newTB.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                        newBattleReport.MainGrid.Children.Add(newTB);

                        Switcher.Switch(newBattleReport);
                    }));
                }

            }
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
