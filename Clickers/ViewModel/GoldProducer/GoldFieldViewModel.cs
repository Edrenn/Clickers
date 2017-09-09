using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

using Clickers.DataBaseManager;
using Clickers.ViewModel.GoldProducer;
using Clickers.Models;
using Clickers.Models.Buildings;
using Clickers.Views;
using Clickers.Views.GoldView;

namespace Clickers.ViewModel
{
    public class GoldFieldViewModel : INotifyPropertyChanged
    {
        #region Fields
        GoldFieldView view;
        public event PropertyChangedEventHandler PropertyChanged;
        
        #endregion

        public GoldFieldViewModel(GoldFieldView view)
        {
            this.view = view;
            this.view.InfoBarUC.Content = InfoBarViewModel._Instance.View;
            EventGenerator();
            view.DataContext = GameViewModel.Instance;

            foreach (GoldProducerViewModel GPVM in GameViewModel.Instance.AllGoldProducerVM)
            {
                if (GPVM.View.Parent != null)
                {
                    ((StackPanel)GPVM.View.Parent).Children.Remove(GPVM.View);
                }
                if (GPVM.RessourceProducer.IsVisible)
                {
                    if (GPVM.RessourceProducer.IsActive == true)
                    {
                        GPVM.SetActiveView();
                        this.view.AllGoldProducersSP.Children.Add(GPVM.View);
                    }
                    else
                        this.view.AllGoldProducersSP.Children.Add(GPVM.View);
                }
                else
                {
                    GPVM.View.Visibility = System.Windows.Visibility.Collapsed;
                    this.view.AllGoldProducersSP.Children.Add(GPVM.View);
                }
            }

        }

        public GoldFieldViewModel()
        {
        }

        private void EventGenerator()
        {
            view.GoldButton.Click += GoldButton_Click1;
        }

        private void GoldButton_Click1(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.GoldCounter++;
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
