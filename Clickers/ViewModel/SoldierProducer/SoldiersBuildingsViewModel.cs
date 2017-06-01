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
using Clickers.Views.SoldierView;

namespace Clickers.ViewModel
{
    public class SoldiersBuildingsViewModel
    {
        #region Fields
        MySQLManager<SoldiersProducer> mySoldiersProducerSQLManager = new MySQLManager<SoldiersProducer>();
        MySQLManager<Soldier> mySQLSoldierManager = new MySQLManager<Soldier>();
        SoldierProducerMySQLManager newSoldierProducerMySQLManager = new SoldierProducerMySQLManager();

        private SoldiersBuildingsView view;
        public SoldiersBuildingsView View
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public SoldiersBuildingsViewModel(SoldiersBuildingsView view)
        {
            this.View = view;
            
            CaserneCheck(GameViewModel.Instance.MainCastle.SoldiersProducers["Caserne"],this.View.soldierView1);
            CaserneCheck(GameViewModel.Instance.MainCastle.SoldiersProducers["Archerie"], this.View.soldierView2);
            CaserneCheck(GameViewModel.Instance.MainCastle.SoldiersProducers["Ecurie"], this.View.soldierView3);
        }
        #endregion

        #region Private Methods
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

        /// <summary>
        /// This method will find if a SoldierProducer is already bought (IsActive = true) or not and will change the view if it's necessary
        /// </summary>
        /// <param name="soldierProducer">The SoldierProducer that we want to check</param>
        private void CaserneCheck(SoldiersProducer soldierProducer,SoldierView soldierView)
        {
            switch (soldierProducer.Name)
            {
                case "Caserne":
                    if (soldierProducer.IsActive == true)
                    {
                        this.View.CaserneGrid.Visibility = System.Windows.Visibility.Collapsed;
                        this.View.soldierView1.Controller = new SoldierViewModel(soldierView, soldierProducer.SoldierType);
                        this.View.soldierView1.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        // This dataContext allows us to see the price and the name of the SoldierProducer
                        SoldierProducerViewModel caserneView = new SoldierProducerViewModel(GameViewModel.Instance.MainCastle.SoldiersProducers[soldierProducer.Name], soldierView);
                        this.View.CaserneSP.Children.Add(caserneView.View);
                        this.View.CaserneGrid.DataContext = GameViewModel.Instance.MainCastle.SoldiersProducers["Caserne"];
                        this.View.Caserne1Button.Click += Caserne1Button_Click;
                    }
                    break;
                case "Archerie":
                    if (soldierProducer.IsActive == true)
                    {
                        this.View.ArcherieGrid.Visibility = System.Windows.Visibility.Collapsed;
                        this.View.soldierView1.Controller = new SoldierViewModel(soldierView, soldierProducer.SoldierType);
                        this.View.soldierView2.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        // This dataContext allows us to see the price and the name of the SoldierProducer
                        SoldierProducerViewModel archerieView = new SoldierProducerViewModel(GameViewModel.Instance.MainCastle.SoldiersProducers[soldierProducer.Name], soldierView);
                        this.View.ArcherieSP.Children.Add(archerieView.View);
                        this.View.ArcherieGrid.DataContext = GameViewModel.Instance.MainCastle.SoldiersProducers["Archerie"];
                        this.View.Caserne2Button.Click += Caserne2Button_Click;
                    }
                    break;
                case "Ecurie":
                    if (soldierProducer.IsActive == true)
                    {
                        this.View.EcurieGrid.Visibility = System.Windows.Visibility.Collapsed;
                        this.View.soldierView1.Controller = new SoldierViewModel(soldierView, soldierProducer.SoldierType);
                        this.View.soldierView3.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        // This dataContext allows us to see the price and the name of the SoldierProducer
                        SoldierProducerViewModel archerieView = new SoldierProducerViewModel(GameViewModel.Instance.MainCastle.SoldiersProducers[soldierProducer.Name], soldierView);
                        this.View.EcurieSP.Children.Add(archerieView.View);
                        this.View.EcurieGrid.DataContext = GameViewModel.Instance.MainCastle.SoldiersProducers["Ecurie"];
                        this.View.EcurieButton.Click += Caserne3Button_Click;
                    }
                    break;
            }

        }

        #region Events

        private void Caserne1Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.MainCastle.SoldiersProducers["Caserne"].IsActive = true;
            GameViewModel.Instance.GoldCounter -= GameViewModel.Instance.MainCastle.SoldiersProducers["Caserne"].Price;
            this.View.CaserneGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.soldierView1.Visibility = System.Windows.Visibility.Visible;
        }

        private void Caserne2Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.MainCastle.SoldiersProducers["Archerie"].IsActive = true;
            GameViewModel.Instance.GoldCounter -= GameViewModel.Instance.MainCastle.SoldiersProducers["Caserne"].Price;
            this.View.ArcherieGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.soldierView2.Visibility = System.Windows.Visibility.Visible;
        }

        private void Caserne3Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.MainCastle.SoldiersProducers["Ecurie"].IsActive = true;
            GameViewModel.Instance.GoldCounter -= GameViewModel.Instance.MainCastle.SoldiersProducers["Caserne"].Price;
            this.View.EcurieGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.soldierView3.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion
        #endregion
    }
}
