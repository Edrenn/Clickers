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

using Clickers.Models;

namespace Clickers.Views.TaverneView
{
    /// <summary>
    /// Logique d'interaction pour HeroView.xaml
    /// </summary>
    public partial class HeroView : UserControl
    {
        public Hero HeroContext;

        public HeroView()
        {
            InitializeComponent();
        }

        public HeroView(Hero hero)
        {
            InitializeComponent();
            this.HeroContext = hero;
            this.DataContext = this.HeroContext;

        }
    }
}
