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

namespace Clickers.Views.OptionView
{
    /// <summary>
    /// Logique d'interaction pour OptionPage.xaml
    /// </summary>
    public partial class OptionUserControl : Window
    {
        public OptionUserControl()
        {
            InitializeComponent();
            OptionViewModel controller = new OptionViewModel(this);
        }
    }
}
