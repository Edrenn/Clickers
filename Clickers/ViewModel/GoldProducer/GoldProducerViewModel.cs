using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Windows;

using Clickers.Views.GoldView;
using Clickers.Models.Buildings;

namespace Clickers.ViewModel.GoldProducer
{
    public class GoldProducerViewModel
    {
        private GoldProducerUC view;
        public GoldProducerUC View
        {
            get { return view; }
            set { view = value; }
        }

        private RessourceProducer ressourceProducer;
        public RessourceProducer RessourceProducer
        {
            get { return ressourceProducer; }
            set
            {
                ressourceProducer = value;
                RaisePropertyChanged("RessourceProducer");
            }
        }

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

        private Task usineTask;
        public Task UsineTask
        {
            get { return usineTask; }
            set { usineTask = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public GoldProducerViewModel(RessourceProducer ressourceProducer)
        {
            this.RessourceProducer = ressourceProducer;
            this.View = new GoldProducerUC();
            this.View.DataContext = this.RessourceProducer;
            EventsGenerator();
        }

        private void EventsGenerator()
        {
            this.View.BuyButton.Click += BuyButton_Click;
            this.View.UpgradeButton.Click += UpgradeButton_Click;
        }

        private void UpgradeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter < RessourceProducer.Price)
            {
                int rest = RessourceProducer.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + rest + " Golds !");
            }
            else
            {
                GameViewModel.Instance.GoldCounter -= RessourceProducer.Price;
                TokenSource.Cancel();

                Task UpgradeTask = new Task(() => { Application.Current.Dispatcher.Invoke(new Action(() => { this.Upgrade(); })); });
                UpgradeTask.Start();
            }


        }

        private void Upgrade()
        {
            System.Windows.Window toto = new System.Windows.Window();
            toto = new System.Windows.Window();
            toto.Width = 250;
            toto.Height = 250;
            System.Windows.Controls.TextBlock test = new System.Windows.Controls.TextBlock() { Text = "Amélioration en cours" };
            test.HorizontalAlignment = HorizontalAlignment.Center;
            test.VerticalAlignment = VerticalAlignment.Center;
            toto.Content = test;
            toto.HorizontalAlignment = HorizontalAlignment.Center;
            toto.VerticalAlignment = VerticalAlignment.Center;
            toto.Show();

            if (this.UsineTask.IsCompleted == false)
            {
                while (this.UsineTask.IsCompleted == false)
                {
                }

            }
            if (toto.ShowActivated)
            {
                toto.Close();
            }

            this.RessourceProducer.Upgrade();
            System.Windows.MessageBox.Show(this.RessourceProducer.Name + " est passé au niveau " + this.RessourceProducer.Level);
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;
            this.UsineTask = new Task(() =>
            {
                GameViewModel.Instance.UsineProduction(RessourceProducer.ProductSpeed, RessourceProducer.QuantityProduct, TokenSource);

            }, Token);
            this.UsineTask.Start();

            if (RessourceProducer.Level == 5)
            {
                view.UpgradeButton.Content = "Maxed";
                view.UpgradeButton.IsEnabled = false;
            }
        }

        private void BuyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter < RessourceProducer.Price)
            {
                int rest = RessourceProducer.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + rest + " Golds !");
            }
            else
            {
                GameViewModel.Instance.GoldCounter -= RessourceProducer.Price;
                RessourceProducer.Price *= 2;
                this.RessourceProducer.IsActive = true;
                SetActiveView();


                Token = TokenSource.Token;
                this.UsineTask = new Task(() =>
                {
                    GameViewModel.Instance.UsineProduction(RessourceProducer.ProductSpeed, RessourceProducer.QuantityProduct, TokenSource);

                }, Token);

                this.UsineTask.Start();
            }
        }

        public void SetActiveView()
        {
            this.View.UnLockedProducerGrid.Visibility = System.Windows.Visibility.Visible;
            this.View.LockedProducerGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.BuyButton.Visibility = System.Windows.Visibility.Collapsed;
            this.View.UpgradeButton.Visibility = System.Windows.Visibility.Visible;
            this.View.ProductivityGrid.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
