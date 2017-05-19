using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clickers.ViewModel
{
    public class GoldProducersViewModel
    {
        #region Properties
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

        private string costString;
        public string CostString
        {
            get { return costString; }
            set
            {
                costString = value;
                RaisePropertyChanged("CostString");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        MySQLManager<RessourceProducer> mySQLManager = new MySQLManager<RessourceProducer>();

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public GoldProducerUserControl view;

        private static Dictionary<RessourceProducer, GoldProducersViewModel> _instances = new Dictionary<RessourceProducer, GoldProducersViewModel>();
        private static object _lock = new object();

        public static GoldProducersViewModel GetProducersViewModelMultition(RessourceProducer Key)
        {
            lock (_lock)
            {
                if (!_instances.ContainsKey(Key))
                {
                    _instances.Add(Key, new GoldProducersViewModel(Key));
                }
                if (_instances[Key].RessourceProducer.IsActive == true)
                {
                    _instances[Key].RefreshView();
                }
                return _instances[Key];
            }
        }

        private GoldProducersViewModel(RessourceProducer ressourceProducer)
        {
            view = new GoldProducerUserControl(this);
            RessourceProducer = ressourceProducer;
            EventsGenerator();
            view.DataContext = this;
            view.BuildingNameLabel.DataContext = RessourceProducer;
        }
        

        #region Events
        private void EventsGenerator()
        {
            view.AcheterButton.Click += AcheterButton_Click;
            view.Close.Click += Close_Click;
            view.UpgradeButton.Click += UpgradeButton_Click;
        }

        private void UpgradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter < RessourceProducer.Price)
            {
                int rest = RessourceProducer.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + rest + " Golds !");
            }
            else
            {
                TokenSource.Cancel();
                RessourceProducer.Level = RessourceProducer.Level  + 1;
                GameViewModel.Instance.GoldCounter -= RessourceProducer.Price;
                RessourceProducer.QuantityProduct += 10;
                RessourceProducer.Price *= 2;
                RefreshView();

                TokenSource = new CancellationTokenSource();
                Token = TokenSource.Token;
                Task usineUnTask = new Task(() =>
                {
                    GameViewModel.Instance.UsineProduction(RessourceProducer.ProductSpeed, RessourceProducer.QuantityProduct, TokenSource);
                },Token);
                usineUnTask.Start();

                if (RessourceProducer.Level == 5)
                {
                    view.UpgradeButton.Content = "Maxed";
                    view.UpgradeButton.IsEnabled = false;
                }
            }
        }

        private async void Close_Click(object sender, RoutedEventArgs e)
        {
            view.Visibility = Visibility.Collapsed;
            await LeUpdate();
        }

        private async Task LeUpdate()
        {
            await mySQLManager.Update(GetProducersViewModelMultition(RessourceProducer).RessourceProducer);
        }

        private void AcheterButton_Click(object sender, RoutedEventArgs e)
        {
            BuyBuilding(view);
        }

        #endregion
        private async void BuyBuilding(GoldProducerUserControl view)
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
                RessourceProducer.IsActive = true;
                RefreshView();

                Token = TokenSource.Token;
                Task usineUnTask = new Task(() =>
                {
                    GameViewModel.Instance.UsineProduction(RessourceProducer.ProductSpeed, RessourceProducer.QuantityProduct, TokenSource);

                }, Token);

                usineUnTask.Start();
                view.ProductTB.Visibility = Visibility.Visible;
                view.AcheterButton.Visibility = Visibility.Collapsed;
                view.UpgradeButton.Visibility = Visibility.Visible;
                view.LevelTB.Visibility = Visibility.Visible;
                view.MainGrid.Background = Brushes.Green;
            }
        }

        private void RefreshView()
        {
            int speedInSec = RessourceProducer.ProductSpeed / 1000;
            view.ProductTB.Text = RessourceProducer.QuantityProduct + " Golds / " + speedInSec + " secs";
            view.LevelTB.Text = "Niveau : " +  RessourceProducer.Level;
            this.CostString = "Prix : " + RessourceProducer.Price;
            view.PriceTB.Text = "Prix : " + RessourceProducer.Price;
            view.MainGrid.Background = Brushes.Green;
        }



        

    }
}
