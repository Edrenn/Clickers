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
            this.view.Slider.ValueChanged += Slider_ValueChanged;
            this.view.SoldierNumberTBx.LostKeyboardFocus += SoldierNumberTBx_LostKeyboardFocus;
        }

        private void SoldierNumberTBx_LostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            int outSoldierNumber = 1;
            if (!Int32.TryParse(this.view.SoldierNumberTBx.Text,out outSoldierNumber))
            {
                outSoldierNumber = 1;
            }
            this.view.Slider.Value = outSoldierNumber;
        }

        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            this.view.SoldierNumberTBx.Text = this.view.Slider.Value.ToString();
        }

        private void SoldierViewBuyButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            int SoldiersNumber = 0;
            if (!Int32.TryParse(this.view.SoldierNumberTBx.Text, out SoldiersNumber))
            {
                SoldiersNumber = 1;
            }

            if (GameViewModel.Instance.GoldCounter >= (this.Soldier.Price * SoldiersNumber))
            {
                Soldier newSoldier = new Soldier();
                newSoldier.InitializeSoldier(this.Soldier);
                for (int i = 0; i < SoldiersNumber; i++)
                {
                    GameViewModel.Instance.MainCastle.Army.AllSoldiers.Add(newSoldier);
                }
                GameViewModel.Instance.GoldCounter -= (this.Soldier.Price * SoldiersNumber);
            }
            else
            {
                System.Windows.MessageBox.Show("Il vous manque " + ((this.Soldier.Price * SoldiersNumber) - GameViewModel.Instance.GoldCounter) + " d'Or");
            }
        }
    }
}
