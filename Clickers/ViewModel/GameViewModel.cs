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
using Clickers.Models.Items;
using Clickers.Models.Buildings;
using Clickers.Views;
using Clickers.ViewModel.GoldProducer;

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
                if (goldCounter == this.MainCastle.GoldProducers[2].Price)
                {
                    this.MainCastle.GoldProducers[2].IsVisible = true;
                }
                if (goldCounter == this.MainCastle.GoldProducers[3].Price)
                {
                    this.MainCastle.GoldProducers[3].IsVisible = true;
                }
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

        private List<GoldProducerViewModel> allGoldProducerVM;
        public List<GoldProducerViewModel> AllGoldProducerVM
        {
            get { return allGoldProducerVM; }
            set { allGoldProducerVM = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private GameViewModel()
        {
            MySQLCastle MySQLCastle = new MySQLCastle();
            this.MainCastle = MySQLCastle.Get(1).Result;
            MySQLCastle.GetProperties(this.MainCastle);
            
            this.AllGoldProducerVM = new List<GoldProducerViewModel>();
            foreach (RessourceProducer RP in MainCastle.GoldProducers)
            {
                GoldProducerViewModel newGPVM = new GoldProducerViewModel(RP);
                this.AllGoldProducerVM.Add(newGPVM);
            }

            this.EnnemyCastle = MySQLCastle.Get(2).Result;
            MySQLCastle.GetArmy(this.EnnemyCastle);
            this.goldCounter = this.MainCastle.Golds;
        }

        public void UsineProduction(int delay, int quantityProduct,CancellationTokenSource CTS)
        {
            int delayInMilliseconds = delay * 1000;
            if (CTS.IsCancellationRequested == false)
            {
                Thread.Sleep(delayInMilliseconds);
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
                MainCastle.SoldiersProducers.Add(sp);
                SPMySQLM.SetSoldiers(sp);
            }
        }

        private void GetAllGoldProducer()
        {
            MySQLManager<RessourceProducer> mySQLRPManager = new MySQLManager<RessourceProducer>();
            List<RessourceProducer> allGoldProducers = mySQLRPManager.GetAll();
            int goldNumber = 0;
            foreach (RessourceProducer RessourceProducer in allGoldProducers)
            {
                this.MainCastle.GoldProducers.Add(RessourceProducer);
                goldNumber++;
            }
        }

        public async void Save()
        {
            MySQLManager<Castle> newManager = new MySQLManager<Castle>();
            this.mainCastle.Golds = this.GoldCounter;
            await newManager.Update(this.MainCastle);
            await newManager.Update(this.EnnemyCastle);

            MySQLManager<Army> newArmyManager = new MySQLManager<Army>();
            MainCastle.Army.chevCounter = 0;
            MainCastle.Army.archCounter = 0;
            MainCastle.Army.cavCounter = 0;
            foreach (Soldier soldier in this.MainCastle.Army.AllSoldiers)
            {
                switch (soldier.Type)
                {
                    case Enums.SoldierType.Chevalier:
                        MainCastle.Army.chevCounter++;
                        break;
                    case Enums.SoldierType.Archer:
                        MainCastle.Army.archCounter++;
                        break;
                    case Enums.SoldierType.Cavalier:
                        MainCastle.Army.cavCounter++;
                        break;
                    default:
                        break;
                }
            }
            if (this.MainCastle.Army.Hero != null)
            {
               await newArmyManager.Database.ExecuteSqlCommandAsync("Update armies set Hero_HeroId = '" + this.MainCastle.Army.Hero.HeroId + "' where ArmyId =" + this.MainCastle.Army.ArmyId);
            }

            MySQLManager<RessourceProducer> RPManager = new MySQLManager<RessourceProducer>();
            foreach (RessourceProducer RP in this.MainCastle.GoldProducers)
            {
                await RPManager.Update(RP);
            }

            MySQLManager<SoldiersProducer> SPManager = new MySQLManager<SoldiersProducer>();
            foreach (SoldiersProducer sp in this.MainCastle.SoldiersProducers)
            {
                await SPManager.Update(sp);
            }

            MySQLManager<Hero> HeroManager = new MySQLManager<Hero>();
            foreach (Hero hero in this.MainCastle.Heroes)
            {
                this.UpdateHero(hero);
            }

            MySQLManager<Weapon> WeaponManager = new MySQLManager<Weapon>();
            foreach (Weapon weapon in this.MainCastle.WeaponStock)
            {
                await WeaponManager.Database.ExecuteSqlCommandAsync("Update weapons set Castle_CastleId = '" + this.MainCastle.CastleId + "' where WeaponId =" + weapon.WeaponId);
            }
            foreach (Shield shield in this.MainCastle.ShieldStock)
            {
                await WeaponManager.Database.ExecuteSqlCommandAsync("Update shields set Castle_CastleId = '" + this.MainCastle.CastleId + "' where ShieldId =" + shield.ShieldId);
            }
            foreach (Potion potion in this.MainCastle.PotionStock)
            {
                await WeaponManager.Database.ExecuteSqlCommandAsync("Update potions set Castle_CastleId = '" + this.MainCastle.CastleId + "' where PotionId =" + potion.PotionId);
            }


            await newManager.SaveChangesAsync();
        }

        private async void UpdateHero(Hero hero)
        {
            MySQLManager<Hero> HeroManager = new MySQLManager<Hero>();
            string table = "Update heroes set ";
            string heroId = "' where HeroId = " + hero.HeroId;

            if (hero.Potion != null)
            {
                string setPotion = table + "Potion_PotionId = '" + hero.Potion.PotionId + heroId;
                await HeroManager.Database.ExecuteSqlCommandAsync(setPotion);
            }
            
            if (hero.Weapon != null)
            {
                string setWeapon = table + "Weapon_WeaponId = '" + hero.Weapon.WeaponId + heroId;
                await HeroManager.Database.ExecuteSqlCommandAsync(setWeapon);
            }

            if (hero.Shield != null)
            {
                string setShield = table + "Shield_ShieldId = '" + hero.Shield.ShieldId + heroId;
                await HeroManager.Database.ExecuteSqlCommandAsync(setShield);
            }
        }
    }
}
