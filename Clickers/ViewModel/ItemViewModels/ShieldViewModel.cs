using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models;
using Clickers.Models.Items;
using Clickers.Views.ElementViews;


namespace Clickers.ViewModel.ItemViewModels
{
    public class ShieldViewModel
    {
        private EquipmentView view;
        public EquipmentView View
        {
            get { return view; }
            set { view = value; }
        }

        private Shield shield;
        public Shield Shield
        {
            get { return shield; }
            set { shield = value; }
        }

        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }


        public ShieldViewModel(Shield shield)
        {
            this.Shield = shield;
            this.View = new EquipmentView();
            this.View.DataContext = this.Shield;
            this.View.DefenseSP.Visibility = System.Windows.Visibility.Visible;
        }

        public ShieldViewModel(Shield shield,Hero hero)
        {
            this.Hero = hero;
            this.Shield = shield;
            this.View = new EquipmentView();
            this.View.DataContext = this.Shield;
            this.View.DefenseSP.Visibility = System.Windows.Visibility.Visible;
        }

        #region Equip
        public void InitEquipView()
        {
            this.View.BuyEquipmentButton.Content = "Équiper";
            this.View.BuyEquipmentButton.Click += EquipEquipmentButton_Click1;
        }

        private void EquipEquipmentButton_Click1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Hero.Shield = this.Shield;
        }
        #endregion

        #region Buy
        public void InitBuyView()
        {
            this.View.BuyEquipmentButton.Click += BuyEquipmentButton_Click;
        }

        private void BuyEquipmentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter >= Shield.Price)
            {
                GameViewModel.Instance.GoldCounter -= this.Shield.Price;
                GameViewModel.Instance.MainCastle.ShieldStock.Add(Shield);
            }
            else
            {
                int missingGold = Shield.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + missingGold + " d'or monseigneur");
            }
        }
        #endregion
    }
}
