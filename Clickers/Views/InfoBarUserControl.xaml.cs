using Clickers.ViewModel;
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
    /// Logique d'interaction pour InfoBarUserControl.xaml
    /// </summary>
    public partial class InfoBarUserControl
    {
        private InfoBarViewModel controller;
        public InfoBarViewModel Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        public InfoBarUserControl()
        {
            InitializeComponent();
            base.DataContext = GameViewModel.Instance;
            this.controller = new InfoBarViewModel(this);
        }

    }
}
