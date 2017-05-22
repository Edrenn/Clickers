using Clickers.Views;
using Clickers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

using Clickers.DataBaseManager;
using Clickers.ViewModel.SoldierProducer;
using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Models.Reflection;
using Clickers.Models.Buildings;

namespace Clickers.ViewModel
{
    public class SoldiersBuildingsViewModel
    {
        MySQLManager<SoldiersProducer> mySoldiersProducerSQLManager = new MySQLManager<SoldiersProducer>();
        MySQLManager<Soldier> mySQLSoldierManager = new MySQLManager<Soldier>();
        SoldierProducerMySQLManager newSoldierProducerMySQLManager = new SoldierProducerMySQLManager();
        private SoldiersProducer producer1 = null;
        public SoldiersProducer Producer1
        {
            get { return producer1; }
            set { producer1 = value; }
        }

        private SoldiersProducer producer2 = null;
        public SoldiersProducer Producer2
        {
            get { return producer2; }
            set { producer2 = value; }
        }

        private SoldiersProducer producer3 = null;
        public SoldiersProducer Producer3
        {
            get { return producer3; }
            set { producer3 = value; }
        }

        private SoldiersBuildingsView view;
        public SoldiersBuildingsView View
        {
            get;
            set;
        }

        public SoldiersBuildingsViewModel(SoldiersBuildingsView view)
        {
            this.View = view;
            EventGenerator();
            Task<Soldier> soldier = this.RecupSoldiers(1);
            this.View.soldierView1.DataContext = soldier.Result;
            soldier = this.RecupSoldiers(2);
            this.View.soldierView2.DataContext = soldier.Result;
        }

        private async Task<SoldiersProducer> RecupProducer(int idToRecup)
        {
            SoldiersProducer producerToRdeturn = await mySoldiersProducerSQLManager.Get(idToRecup);
            return producerToRdeturn;
        }

        private async Task<Soldier> RecupSoldiers(int idToRecup)
        {
            Soldier soldier = await mySQLSoldierManager.Get(idToRecup);
            return soldier;
        }

        private void EventGenerator()
        {
            this.View.CastleButton.Click += CastleButton_Click;
            this.View.Caserne1Button.Click += Caserne1Button_Click;
            this.View.Caserne2Button.Click += Caserne2Button_Click;
            this.View.Caserne3Button.Click += Caserne3Button_Click;
        }

        private void CastleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainCastleView castleView = new MainCastleView();
            var test = RecupSoldiers(1);
            Switcher.Switch(castleView);
        }

        private void Caserne1Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.Caserne1Button.Visibility = System.Windows.Visibility.Collapsed;
            this.View.soldierView1.Visibility = System.Windows.Visibility.Visible;

            //if (Producer1 == null)
            //{
            //    Task<SoldiersProducer> newProducer = RecupProducer(1);
            //    Producer1 = newProducer.Result;
            //    Producer1 = newSoldierProducerMySQLManager.SetSoldiers(Producer1);
            //}
            //SoldierProducerViewModel popUp = SoldierProducerViewModel.GetProducersViewModelMultition(Producer1);
            //popUp.View.SoldierView.DataContext = Producer1.SoldierType;
            //popUp.View.Visibility = System.Windows.Visibility.Visible;
        }

        private void Caserne2Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.Caserne2Button.Visibility = System.Windows.Visibility.Collapsed;
            this.View.soldierView2.Visibility = System.Windows.Visibility.Visible;
            //if (Producer2 == null)
            //{
            //    Task<SoldiersProducer> newProducer = RecupProducer(2);
            //    Producer2 = newProducer.Result;
            //    Producer2 = newSoldierProducerMySQLManager.SetSoldiers(Producer2);
            //}
            //SoldierProducerViewModel popUp = SoldierProducerViewModel.GetProducersViewModelMultition(Producer2);
            //popUp.View.SoldierView.DataContext = Producer2.SoldierType;
            //popUp.View.Visibility = System.Windows.Visibility.Visible;
        }

        private void Caserne3Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Producer3 == null)
            {
                Task<SoldiersProducer> newProducer = RecupProducer(3);
                Producer3 = newProducer.Result;
                Producer3 = newSoldierProducerMySQLManager.SetSoldiers(Producer3);
            }
            SoldierProducerViewModel popUp = SoldierProducerViewModel.GetProducersViewModelMultition(Producer3);
            if (Producer3.IsActive == true)
            {
                popUp.View.Background = Brushes.Green;
                popUp.View.SoldierView.Visibility = System.Windows.Visibility.Visible;
            }
            popUp.View.SoldierView.DataContext = Producer3.SoldierType;
            popUp.View.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
