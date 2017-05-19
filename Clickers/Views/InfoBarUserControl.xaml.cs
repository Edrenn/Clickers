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
            this.DataContext = GameViewModel.Instance;
            this.controller = new InfoBarViewModel(this);
            //ArmyViewModel
        }

        public event EventHandler ProgressUpdate;

        public void testMethod(object input)
        {
            ////part 1
            //if (ProgressUpdate != null)
            //    ProgressUpdate(this, new EventArgs(status));
            ////part 2
        }
        public void TestMethod()
        {
            //part 1
            //ProgressUpdate.Raise(this, status); // #6 - status is of type YourStatus
                                                //part 2
        }

    }
}
