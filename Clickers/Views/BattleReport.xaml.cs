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

namespace Clickers.Views
{
    /// <summary>
    /// Logique d'interaction pour BattleReport.xaml
    /// </summary>
    public partial class BattleReport : UserControl
    {
        public BattleReport()
        {
            InitializeComponent();
            this.ToCastle.Click += ToCastle_Click;

        }

        private void ToCastle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainCastleView castleView = new MainCastleView();
            Task newAttack = new Task(new Action(() =>
            {
                if (GameViewModel.Instance.MainCastle.Life > 0 && GameViewModel.Instance.EnnemyCastle.Life > 0)
                {
                    InfoBarViewModel._Instance.NextAttack();
                }
            }));
            newAttack.Start();
            Switcher.Switch(castleView);
        }
    }
}
