using Clickers.ViewModel;
using Clickers.ViewModel.SoldierProducer;
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

namespace Clickers.Views
{
    /// <summary>
    /// Logique d'interaction pour ArmyProducersView.xaml
    /// </summary>
    public partial class SoldiersBuildingsView
    {
        SoldiersBuildingsViewModel controller;

        public SoldiersBuildingsView()
        {
            InitializeComponent();
            controller = new SoldiersBuildingsViewModel(this);

        }
    }
}
