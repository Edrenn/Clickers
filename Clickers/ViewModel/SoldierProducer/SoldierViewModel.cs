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

        #region Fields
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
        #endregion

        #region Constructor
        public SoldierViewModel(SoldierView view, Soldier soldier)
        {
            this.view = view;
            this.Soldier = soldier;
            this.View.DataContext = this.Soldier;
            EventGenerator();
        }
        #endregion

        #region Events
        private void EventGenerator()
        {
            this.View.BuyButton.Click += SoldierViewBuyButtonClick;
            this.view.Slider.ValueChanged += Slider_ValueChanged;
            this.view.SoldierNumberTBx.TextChanged += SoldierNumberTBx_TextChanged;
        }


        /// <summary>
        /// This event will change the slider Value when we change manually the number in the textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoldierNumberTBx_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int outSoldierNumber = 1;
            if (!Int32.TryParse(this.view.SoldierNumberTBx.Text, out outSoldierNumber))
            {
                outSoldierNumber = 1;
            }
            this.view.Slider.Value = outSoldierNumber;
            this.View.TotalPriceTB.Text = (outSoldierNumber * this.Soldier.Price).ToString();
        }

        /// <summary>
        /// This event will change the value of the number in the textBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            this.view.SoldierNumberTBx.Text = this.view.Slider.Value.ToString();
            this.View.TotalPriceTB.Text = (this.view.Slider.Value * this.Soldier.Price).ToString();
        }

        /// <summary>
        /// This event will buy N Soldier 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion
    }
}
