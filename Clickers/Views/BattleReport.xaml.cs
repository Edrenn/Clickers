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

namespace Clickers.Views
{
    /// <summary>
    /// Logique d'interaction pour BattleReport.xaml
    /// </summary>
    public partial class BattleReport : UserControl
    {
        private Castle mainCastle;
        public Castle MainCastle
        {
            get { return mainCastle; }
            set { mainCastle = value; }
        }

        private Castle enemyCastle;
        public Castle EnemyCastle
        {
            get { return enemyCastle; }
            set { enemyCastle = value; }
        }

        public BattleReport()
        {
            this.MainCastle = GameViewModel.Instance.MainCastle;
            this.EnemyCastle = GameViewModel.Instance.EnnemyCastle;
            this.DataContext = this;
            InitializeComponent();
        }
    }
}
