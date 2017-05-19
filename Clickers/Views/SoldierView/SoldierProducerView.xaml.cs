using Clickers.ViewModel.SoldierProducer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Logique d'interaction pour SoldierProducerView.xaml
    /// </summary>
    public partial class SoldierProducerView : Window
    {
        SoldierProducerViewModel controller;
        public SoldierProducerViewModel Controller { get; set; }
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public SoldierProducerView(SoldierProducerViewModel controller)
        {
            InitializeComponent();
            this.Controller = controller;
        }
    }
}
