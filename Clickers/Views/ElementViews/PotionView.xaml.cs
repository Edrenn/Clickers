using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Clickers.Models.Items;

namespace Clickers.Views.ElementViews
{
    /// <summary>
    /// Logique d'interaction pour PotionView.xaml
    /// </summary>
    public partial class PotionView : UserControl
    {
        private Potion potion;
        public Potion Potion
        {
            get { return potion; }
            set { potion = value; }
        }
        
        public PotionView(Potion potion)
        {
            InitializeComponent();
            this.Potion = potion;
        }
    }
}
