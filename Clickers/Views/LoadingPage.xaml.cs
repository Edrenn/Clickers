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

using System.Threading;

namespace Clickers.Views
{
    /// <summary>
    /// Logique d'interaction pour LoadingPage.xaml
    /// </summary>
    public partial class LoadingPage : UserControl
    {
        public LoadingPage()
        {
            InitializeComponent();
            BeginLoadingAnimation();
        }

        private void BeginLoadingAnimation()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => { this.LoadingTB.Text = "Chargement"; }));
            Task loadingScreenAnimation = new Task(new Action(() =>
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { this.LoadingTB.Text = "Chargement"; }));
                Thread.Sleep(600);
                Application.Current.Dispatcher.Invoke(new Action(() => { this.LoadingTB.Text = "Chargement."; }));
                Thread.Sleep(600);
                Application.Current.Dispatcher.Invoke(new Action(() => { this.LoadingTB.Text = "Chargement.."; }));
                Thread.Sleep(600);
                Application.Current.Dispatcher.Invoke(new Action(() => { this.LoadingTB.Text = "Chargement..."; }));
                Thread.Sleep(600);
                BeginLoadingAnimation();
            }));
            loadingScreenAnimation.Start();
        }
    }
}
