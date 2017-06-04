using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        public ShieldViewModel(Shield shield)
        {
            this.Shield = shield;
            this.View = new EquipmentView();
            this.View.DataContext = this.Shield;
            this.View.DefenseSP.Visibility = System.Windows.Visibility.Visible;
            initBuyView();
        }

        public void initRepairView()
        {

        }

        public void initBuyView()
        {
            this.View.BuyEquipmentButton.Click += BuyEquipmentButton_Click;
        }

        private void BuyEquipmentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GameViewModel.Instance.GoldCounter >= Shield.Price)
            {
                GameViewModel.Instance.GoldCounter -= this.Shield.Price;
                GameViewModel.Instance.MainCastle.ShieldStock.Add(Shield.DuplicateShield());
            }
            else
            {
                int missingGold = Shield.Price - GameViewModel.Instance.GoldCounter;
                System.Windows.MessageBox.Show("Il vous manque " + missingGold + " d'or monseigneur");
            }
        }
    }
}
