using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Buildings;
using Clickers.Models.Items;
using Clickers.ViewModel.ItemViewModels;
using Clickers.Views.ElementViews;

namespace Clickers.ViewModel
{
    public class EquipmentListingViewModel
    {
        private EquipmentListing view;
        public EquipmentListing View
        {
            get { return view; }
            set { view = value; }
        }

        private Blacksmith blacksmith;
        public Blacksmith Blacksmith
        {
            get { return blacksmith; }
            set { blacksmith = value; }
        }


        public EquipmentListingViewModel(EquipmentListing view)
        {
            this.View = view;
        }

        #region Blacksmith

        public void initBlacksmithView()
        {
            this.View.ToShieldButton.Click += ToShieldEquipButton_Click;
            this.View.ToWeaponButton.Click += ToWeaponEquipButton_Click;
            this.View.ToPotionButton.Visibility = System.Windows.Visibility.Collapsed;
            foreach (Shield shield in this.Blacksmith.ShieldList)
            {
                ShieldViewModel newShieldViewModel = new ShieldViewModel(shield);
                this.View.BuyShieldSP.Children.Add(newShieldViewModel.View);
            }

            foreach (Weapon weapon in this.Blacksmith.WeaponList)
            {
                WeaponViewModel newWeaponViewModel = new WeaponViewModel(weapon);
                this.View.BuyWeaponSP.Children.Add(newWeaponViewModel.View);
            }
        }

        private void ToShieldBlacksmithButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.WeaponSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ShieldSV.Visibility = System.Windows.Visibility.Visible;
            this.View.ToShieldButton.Opacity = 1;
            this.View.ToWeaponButton.Opacity = 0.2;
        }

        private void ToWeaponBlacksmithButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.WeaponSV.Visibility = System.Windows.Visibility.Visible;
            this.View.ShieldSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ToShieldButton.Opacity = 0.2;
            this.View.ToWeaponButton.Opacity = 1;
        }
        #endregion

        #region Equip
        public void initEquipView()
        {
            this.View.ToShieldButton.Click += ToShieldEquipButton_Click;
            this.View.ToWeaponButton.Click += ToWeaponEquipButton_Click;
            this.View.ToPotionButton.Click += ToPotionEquipButton_Click;
            foreach (Shield shield in GameViewModel.Instance.MainCastle.ShieldStock)
            {
                ShieldViewModel newShieldViewModel = new ShieldViewModel(shield);
                this.View.BuyShieldSP.Children.Add(newShieldViewModel.View);
            }

            foreach (Weapon weapon in GameViewModel.Instance.MainCastle.WeaponStock)
            {
                WeaponViewModel newWeaponViewModel = new WeaponViewModel(weapon);
                this.View.BuyWeaponSP.Children.Add(newWeaponViewModel.View);
            }

            foreach (Potion potion in GameViewModel.Instance.MainCastle.PotionStock)
            {
                PotionViewModel newPotionViewModel = new PotionViewModel(potion);
                this.View.BuyPotionSP.Children.Add(newPotionViewModel.View);
            }
        }
        
        private void ToShieldEquipButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.WeaponSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ShieldSV.Visibility = System.Windows.Visibility.Visible;
            this.View.PotionSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ToShieldButton.Opacity = 1;
            this.View.ToWeaponButton.Opacity = 0.2;
            this.View.ToPotionButton.Opacity = 0.2;
        }

        private void ToWeaponEquipButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.WeaponSV.Visibility = System.Windows.Visibility.Visible;
            this.View.ShieldSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.PotionSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ToShieldButton.Opacity = 0.2;
            this.View.ToWeaponButton.Opacity = 1;
            this.View.ToPotionButton.Opacity = 0.2;
        }

        private void ToPotionEquipButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.WeaponSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ShieldSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.PotionSV.Visibility = System.Windows.Visibility.Visible;
            this.View.ToShieldButton.Opacity = 0.2;
            this.View.ToWeaponButton.Opacity = 0.2;
            this.View.ToPotionButton.Opacity = 1;
        }
        #endregion
    }
}
