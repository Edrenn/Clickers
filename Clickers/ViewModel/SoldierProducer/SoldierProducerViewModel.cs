using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Models;
using Clickers.Models.Buildings;
using Clickers.Views;
using Clickers.Views.SoldierView;

namespace Clickers.ViewModel.SoldierProducer
{
    public class SoldierProducerViewModel
    {
        private SoldierProducerView view;
        public SoldierProducerView View
        {
            get { return view; }
            set { view = value; }
        }

        private SoldierView soldierView;
        public SoldierView SoldierView
        {
            get { return soldierView; }
            set { soldierView = value; }
        }

        private SoldiersProducer soldierProducer;
        public SoldiersProducer SoldierProducer
        {
            get { return soldierProducer; }
            set { soldierProducer = value; }
        }

        public SoldierProducerViewModel(SoldiersProducer soldiersProducer,SoldierView soldierView)
        {
            this.View = new SoldierProducerView();
            this.SoldierView = soldierView;
            SoldierProducer = soldiersProducer;
            this.View.DataContext = SoldierProducer;
            this.View.SoldierProducerBuyButton.Click += SoldierProducerBuyButtonClick;
        }

        private void SoldierProducerBuyButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter >= SoldierProducer.Price)
            {
                SoldierViewModel controller = new SoldierViewModel(this.SoldierView, this.SoldierProducer.SoldierType);
                this.SoldierView.Visibility = System.Windows.Visibility.Visible;
                GameViewModel.Instance.GoldCounter -= SoldierProducer.Price;
                this.View.Visibility = System.Windows.Visibility.Collapsed;
                this.SoldierProducer.IsActive = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Il vous manque " + (SoldierProducer.Price - GameViewModel.Instance.GoldCounter) + " d'Or");
            }
        }
    }
}
