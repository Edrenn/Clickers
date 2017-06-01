using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Views;

namespace Clickers.ViewModel
{
    public class ToCastleButtonViewModel
    {
        private ToCastleButtonUserControl view;
        public ToCastleButtonUserControl View
        {
            get { return view; }
            set { view = value; }
        }


        public ToCastleButtonViewModel(ToCastleButtonUserControl view)
        {
            this.View = view;
            this.View.ToCastleButton.Click += ToCastleButton_Click;
        }

        private void ToCastleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainCastleView());
        }
    }
}
