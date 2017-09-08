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
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace Clickers.Views.AttackAlertView
{
    /// <summary>
    /// Logique d'interaction pour AttackAlertCounterView.xaml
    /// </summary>
    public partial class AttackAlertCounterView : Window
    {
        private AttackAlertViewModel controller;
        public AttackAlertViewModel Controller { get; set; }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        public AttackAlertCounterView()
        {
            InitializeComponent();
            this.ClosePopUpButton.Click += (s, e) =>
            {
                this.Visibility = Visibility.Collapsed;
            };
        }
    }
}
