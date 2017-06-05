using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Clickers.Models;
using Clickers.Models.Items;
using Clickers.Views.ElementViews;

namespace Clickers.ViewModel.ItemViewModels
{
    public class PotionViewModel
    {
        private PotionView view;
        public PotionView View
        {
            get { return view; }
            set { view = value; }
        }

        private Potion potion;
        public Potion Potion
        {
            get { return potion; }
            set { potion = value; }
        }

        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        public PotionViewModel(Potion potion)
        {
            this.Potion = potion;
            this.View = new PotionView(potion);
            this.View.DataContext = this.Potion;
        }

        public PotionViewModel(Potion potion,Hero hero)
        {
            this.Hero = hero;
            this.Potion = potion;
            this.View = new PotionView(potion);
            this.View.DataContext = this.Potion;
            this.View.BuyPotionButton.Click += BuyPotionButton_Click;
        }

        #region Buy
        public void IniBuyView()
        {
            this.View.BuyPotionButton.Click += BuyPotionButton_Click;
        }

        private void BuyPotionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter >= potion.Price)
            {
                GameViewModel.Instance.GoldCounter -= this.potion.Price;
                GameViewModel.Instance.MainCastle.PotionStock.Add(potion.DuplicatePotion());
            }
            else
            {
                int missingGold = potion.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + missingGold + " d'or monseigneur");
            }
        }
        #endregion

        #region Equip
        public void InitEquipView()
        {
            this.View.BuyPotionButton.Content = "Équiper";
            this.View.BuyPotionButton.Click += EquipEquipmentButton_Click1;
        }

        private void EquipEquipmentButton_Click1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Hero.Potion = this.Potion;
        }
        #endregion
    }
}
