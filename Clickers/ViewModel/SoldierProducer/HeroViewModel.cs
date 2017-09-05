using Clickers.Models;
using Clickers.Views.TaverneView;
using Clickers.ViewModel.popUp;
using Clickers.Views.ElementViews;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Clickers.ViewModel.SoldierProducer
{
    public class HeroViewModel
    {
        private HeroView view;
        public HeroView View
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


        public HeroViewModel(Hero hero)
        {
            View = new HeroView();
            this.Hero = hero;
            this.View.DataContext = this.Hero;
            InventoryUC newIventoryUC = new InventoryUC();
            newIventoryUC.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            newIventoryUC.DataContext = this.Hero;
            this.View.HeroInfoSP.Children.Add(newIventoryUC);
            InitEquipView();
        }

        #region Equip
        public void InitEquipView()
        {
            this.View.InventoryHeroButton.Click += EquipButton_Click;
        }

        private void EquipButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowEquipmentView();
        }

        private void ShowEquipmentView()
        {
            InventorySetViewModel newController = new InventorySetViewModel(this.Hero);
            newController.View.Show();
        }
        #endregion

        #region Select
        public void InitSelectView()
        {
            View.SelectHeroButton.Click += SelectHeroButton_Click;
        }

        private void SelectHeroButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.MainCastle.Army.Hero = this.Hero;
        }
        #endregion
    }
}
