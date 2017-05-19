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
        SoldierView view;
        Soldier soldier;

        public SoldierViewModel(SoldierView view, Soldier soldier)
        {
            this.view = view;
            view.BuyButton.Click += BuyButton_Click;
        }

        private void BuyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }
    }
}
