using Clickers.ViewModel.Army;
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

namespace Clickers.Views.ArmyView
{
    /// <summary>
    /// Logique d'interaction pour ArmyView.xaml
    /// </summary>
    public partial class ArmyView : UserControl
    {
        public ArmyViewModel controller;

        public ArmyView()
        {
            InitializeComponent();
            this.controller = new ArmyViewModel(this);
        }
    }
}
