using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using Clickers.Models;
using Clickers.Views.popUp;
using Clickers.Views.TaverneView;
using Clickers.ViewModel.SoldierProducer;
using Clickers.Views.ElementViews;

namespace Clickers.ViewModel.popUp
{
    public class InventorySetViewModel
    {
        private InventorySetPopUp view;
        public InventorySetPopUp View
        {
            get { return view; }
            set { view = value; }
        }

        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        private HeroView heroView;
        public HeroView HeroView
        {
            get { return heroView; }
            set { heroView = value; }
        }

        public InventorySetViewModel(Hero hero)
        {
            this.Hero = hero;
            this.View = new InventorySetPopUp();
            HeroViewModel newHeroViewModel = new HeroViewModel(this.Hero);
            newHeroViewModel.View.InventoryHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            this.HeroView = newHeroViewModel.View;
            this.HeroView.Height = this.View.Grid.Height;
            this.HeroView.ButtonSP.Visibility = System.Windows.Visibility.Collapsed;
            this.View.HeroViewSP.Children.Add(HeroView);

            EquipmentListing EquipmentListing = new EquipmentListing();
            EquipmentListing.Controller.initEquipView(hero);
            this.view.allObjectSP.Children.Add(EquipmentListing);
        }
    }
}
