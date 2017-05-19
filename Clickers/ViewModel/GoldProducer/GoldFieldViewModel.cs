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
using System.Windows.Media;

namespace Clickers.ViewModel
{
    public class GoldFieldViewModel : INotifyPropertyChanged
    {
        #region Fields
        GoldFieldView view;
        public event PropertyChangedEventHandler PropertyChanged;
        MySQLManager<RessourceProducer> mySQLManager = new MySQLManager<RessourceProducer>();
        RessourceProducer producer = null;
        RessourceProducer producer2 = null;
        RessourceProducer producer3 = null;
        RessourceProducer producer4 = null;
        #endregion
        #region Properties
        private int goldCounter = 0;
        public int GoldCounter
        {
            get
            {
                return goldCounter;
            }
            set
            {
                goldCounter = value;
                RaisePropertyChanged("GoldCounter");
            }
        }

        #endregion

        public GoldFieldViewModel(GoldFieldView view)
        {
            this.view = view;
            EventGenerator();
            view.DataContext = GameViewModel.Instance;
            
        }

        public GoldFieldViewModel()
        {
            this.view = new GoldFieldView();
            EventGenerator();
            view.DataContext = GameViewModel.Instance;

        }

        private void EventGenerator()
        {
            view.GoldButton.Click += GoldButton_Click1;
            view.UsineOneButton.Click += UsineOneButton_Click;
            view.UsineTwoButton.Click += UsineTwoButton_Click;
            view.UsineThreeButton.Click += UsineThreeButton_Click;
            view.UsineFourButton.Click += UsineFourButton_Click;
            view.MapButton.Click += MapButton_Click;
        }

        private void MapButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainCastleView());
        }

        private void GoldButton_Click1(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.GoldCounter++;
        }

        private void UsineFourButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (producer4 == null)
            {
                Task<RessourceProducer> newProducer = RecupProducer(4);
                producer4 = newProducer.Result;
            }
            GoldProducersViewModel popUp = GoldProducersViewModel.GetProducersViewModelMultition(producer4);
            popUp.view.Visibility = System.Windows.Visibility.Visible;
    }

        private void UsineThreeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (producer3 == null)
            {
                Task<RessourceProducer> newProducer = RecupProducer(3);
                producer3 = newProducer.Result;
            }
            GoldProducersViewModel popUp = GoldProducersViewModel.GetProducersViewModelMultition(producer3);
            popUp.view.Visibility = System.Windows.Visibility.Visible;
    }

        private void UsineTwoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (producer2 == null)
            {
                Task<RessourceProducer> newProducer = RecupProducer(2);
                producer2 = newProducer.Result;
            }
            GoldProducersViewModel popUp = GoldProducersViewModel.GetProducersViewModelMultition(producer2);
            popUp.view.Visibility = System.Windows.Visibility.Visible;
        }

        private void UsineOneButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (producer == null)
            {
                Task<RessourceProducer> newProducer = RecupProducer(1);
                producer = newProducer.Result;
            }
            GoldProducersViewModel popUp = GoldProducersViewModel.GetProducersViewModelMultition(producer);
            popUp.view.Visibility = System.Windows.Visibility.Visible;
        }

        private async Task<RessourceProducer> RecupProducer(int idToRecup)
        {
            RessourceProducer producerToReturn = await mySQLManager.Get(idToRecup);
            return producerToReturn;
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
