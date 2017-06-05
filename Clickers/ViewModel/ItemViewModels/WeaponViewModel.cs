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
    public class WeaponViewModel
    {
        private EquipmentView view;
        public EquipmentView View
        {
            get { return view; }
            set { view = value; }
        }

        private Weapon weapon;
        public Weapon Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }

        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        public WeaponViewModel(Weapon weapon)
        {
            this.Weapon = weapon;
            this.View = new EquipmentView();
            this.View.DataContext = this.Weapon;
            this.View.AttaqueSP.Visibility = System.Windows.Visibility.Visible;
        }

        public WeaponViewModel(Weapon weapon,Hero hero)
        {
            this.Hero = hero;
            this.Weapon = weapon;
            this.View = new EquipmentView();
            this.View.DataContext = this.Weapon;
            this.View.AttaqueSP.Visibility = System.Windows.Visibility.Visible;
        }

        #region Equip
        public void InitEquipView()
        {
            this.View.BuyEquipmentButton.Content = "Équiper";
            this.View.BuyEquipmentButton.Click += EquipEquipmentButton_Click1;
        }

        private void EquipEquipmentButton_Click1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Hero.Weapon = this.Weapon;
        }
        #endregion

        #region Buy
        public void InitBuyView()
        {
            this.View.BuyEquipmentButton.Click += BuyEquipmentButton_Click;
        }

        private void BuyEquipmentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter >= Weapon.Price)
            {
                GameViewModel.Instance.GoldCounter -= this.Weapon.Price;
                GameViewModel.Instance.MainCastle.WeaponStock.Add(Weapon.DuplicateWeapon());
            }
            else
            {
                int missingGold = Weapon.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + missingGold + " d'or monseigneur");
            }
        }
        #endregion
    }
}
