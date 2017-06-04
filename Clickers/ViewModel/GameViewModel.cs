using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Clickers.DataBaseManager.EntitiesLink;
using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Models.Buildings;
using Clickers.Views;

namespace Clickers
{
    public class GameViewModel : INotifyPropertyChanged
    {
        #region Properties
        private int goldCounter = 0;
        public int GoldCounter
        {
            get
            {
                return goldCounter;
            }
            set
            {
                goldCounter = value;
                RaisePropertyChanged("GoldCounter");
            }
        }

        private static GameViewModel instance;
        public static GameViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameViewModel();
                }
                return instance;
            }
        }

        private string castleName;
        public string CastleName
        {
            get
            {
                return castleName;
            }

            set
            {
                castleName = value;
                RaisePropertyChanged("GoldCounter");
            }
        }

        private Castle mainCastle;
        public Castle MainCastle
        {
            get
            {
                return mainCastle;
            }

            set
            {
                mainCastle = value;
            }
        }

        private Castle ennemyCastle;
        public Castle EnnemyCastle
        {
            get
            {
                return ennemyCastle;
            }

            set
            {
                ennemyCastle = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private GameViewModel()
        {
            MainCastle = new Castle(castleName);
            MySQLManager<HealerHouse> MyHealerHouseSQLManager = new MySQLManager<HealerHouse>();
            MainCastle.Healer = MyHealerHouseSQLManager.Get(1).Result;
            MySQLHealerHouse mySQLHealerHouse = new MySQLHealerHouse();
            mySQLHealerHouse.GetHealerHouse(MainCastle.Healer);


            MySQLManager<Blacksmith> MyBlacksmithSQLManager = new MySQLManager<Blacksmith>();
            MainCastle.Blacksmith = MyBlacksmithSQLManager.Get(1).Result;
            MySQLBlacksmith toto = new MySQLBlacksmith();
            toto.SetItems(MainCastle.Blacksmith);

            getAllHero();
            GetAllSoldierProducer();
            ennemyCastle = new Castle("Méchant Chato");
        }

        public void UsineProduction(int delay, int quantityProduct,CancellationTokenSource CTS)
        {
            if (CTS.IsCancellationRequested == false)
            {
                Thread.Sleep(delay);
                GoldCounter = GoldCounter + quantityProduct;
                UsineProduction(delay, quantityProduct,CTS);
            }
        }

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void getAllHero()
        {
            MySQLManager<Hero> mySQLHeroManager = new MySQLManager<Hero>();
            MainCastle.Heroes = mySQLHeroManager.GetAll();
            MySQLHero mySQLHero = new MySQLHero();
            foreach (Hero hero in MainCastle.Heroes)
            {
                mySQLHero.GetSkills(hero);
            }
        }

        private void GetAllSoldierProducer()
        {
            MySQLManager<SoldiersProducer> mySQLSPManager = new MySQLManager<SoldiersProducer>();
            List<SoldiersProducer> allSoldierProducers = mySQLSPManager.GetAll();
            SoldierProducerMySQLManager SPMySQLM = new SoldierProducerMySQLManager();
            foreach (SoldiersProducer sp in allSoldierProducers)
            {
                MainCastle.SoldiersProducers.Add(sp.Name, sp);
                SPMySQLM.SetSoldiers(sp);
            }
        }


    }
}
