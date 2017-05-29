using Clickers.Models;
using Clickers.Views.SoldierView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.ViewModel.SoldierProducer
{
    public class SoldierViewModel
    {
        private SoldierView view;
        public SoldierView View
        {
            get { return view; }
            set { view = value; }
        }

        private Soldier soldier;
        public Soldier Soldier
        {
            get { return soldier; }
            set { soldier = value; }
        }


        public SoldierViewModel(SoldierView view, Soldier soldier)
        {
            this.view = view;
            this.Soldier = soldier;
            this.View.DataContext = this.Soldier;
            this.View.BuyButton.Click += SoldierViewBuyButtonClick;
        }

        private void SoldierViewBuyButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter >= this.Soldier.Price)
            {
                Soldier newSoldier = new Soldier();
                newSoldier.InitializeSoldier(this.Soldier);
                GameViewModel.Instance.MainCastle.Army.AllSoldiers.Add(newSoldier);
                GameViewModel.Instance.GoldCounter -= this.Soldier.Price;
            }
            else
            {
                System.Windows.MessageBox.Show("Il vous manque " + (this.Soldier.Price - GameViewModel.Instance.GoldCounter) + " d'Or");
            }
        }
    }
}
