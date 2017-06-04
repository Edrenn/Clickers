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
            this.View.ToShieldBuyButton.Click += ToShieldBuyButton_Click;
            this.View.ToWeaponBuyButton.Click += ToWeaponBuyButton_Click;
        }

        #region Buy

        public void initBuyView()
        {
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

        private void ToWeaponBuyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.BuyWeaponSV.Visibility = System.Windows.Visibility.Visible;
            this.View.BuyShieldSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ToShieldBuyButton.Visibility = System.Windows.Visibility.Visible;
            this.View.ToWeaponBuyButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ToShieldBuyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.BuyWeaponSV.Visibility = System.Windows.Visibility.Collapsed;
            this.View.BuyShieldSV.Visibility = System.Windows.Visibility.Visible;
            this.View.ToShieldBuyButton.Visibility = System.Windows.Visibility.Collapsed;
            this.View.ToWeaponBuyButton.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion

        public void initRepairView()
        {
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
        }

       
    }
}
