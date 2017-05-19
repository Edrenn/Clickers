using Clickers.DataBaseManager.EntitiesLink;
using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
            MySQLManager<Hero> mySQLHeroManager = new MySQLManager<Hero>();
            Task<List<Hero>> allHeros = mySQLHeroManager.GetAll();
            MainCastle.Heroes = allHeros.Result;
            MySQLHero mySQLHero = new MySQLHero();
            foreach (Hero hero in MainCastle.Heroes)
            {
                mySQLHero.GetSkills(hero);
            }
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


    }
}
