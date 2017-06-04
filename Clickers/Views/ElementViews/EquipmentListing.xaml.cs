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

using Clickers.ViewModel;

namespace Clickers.Views.ElementViews
{
    /// <summary>
    /// Logique d'interaction pour EquipmentListing.xaml
    /// </summary>
    public partial class EquipmentListing : UserControl
    {
        private EquipmentListingViewModel controller;
        public EquipmentListingViewModel Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        public EquipmentListing()
        {
            InitializeComponent();
            this.Controller = new EquipmentListingViewModel(this);
        }
    }
}
