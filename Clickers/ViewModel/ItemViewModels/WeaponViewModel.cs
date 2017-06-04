using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public WeaponViewModel(Weapon weapon)
        {
            this.Weapon = weapon;
            this.View = new EquipmentView();
            this.View.DataContext = this.Weapon;
            this.View.AttaqueSP.Visibility = System.Windows.Visibility.Visible;
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
    }
}
