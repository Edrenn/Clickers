using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Models;
using Clickers.Json;
using Clickers.Models.Buildings;
using Clickers.Models.Generators;
using Clickers.Models.Items;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.DataBaseManager
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    class MySQLFullDB : DbContext
    {
        public DbSet<Castle> DbSetCastle { get; set; }
        public DbSet<RessourceProducer> DbSetRessourceProducer { get; set; }
        public DbSet<SoldiersProducer> DbSetSoldiersProducer { get; set; }
        public DbSet<Soldier> DbSetSoldiers { get; set; }
        public DbSet<Hero> DbSetHeros { get; set; }
        public DbSet<HealerHouse> DbSetHealerHouse { get; set; }
        public DbSet<Blacksmith> DbSetBlacksmith { get; set; }
        public DbSet<Potion> DbSetPotion { get; set; }
        public DbSet<Shield> DbSetShield { get; set; }

        public MySQLFullDB()
            : base(JsonManager.Instance.ReadFile<ConnectionString>(@"D:\Workspaces\Clickers\Clickers\JsonConfig\", @"MysqlConfig.json").ToString())
        {        }

        public async void InitLocalMySQL()
        {
            if (this.Database.CreateIfNotExists())
            {
                List<RessourceProducer> allGoldProducer = JsonManager.Instance.GetAllGoldProducersFromJSon();
                foreach (RessourceProducer item in allGoldProducer)
                {
                    DbSetRessourceProducer.Add(item);
                }
                List<SoldiersProducer> allSoldierProducer = JsonManager.Instance.GetAllSoldierProducersFromJSon(AllPath.Instance.JsonFolder + AllPath.Instance.BaseSoldierProducer);
                foreach (SoldiersProducer item in allSoldierProducer)
                {
                    DbSetSoldiersProducer.Add(item);
                }
                List<Hero> allHeros = JsonManager.Instance.GetAllHerosFromJSon(AllPath.Instance.JsonFolder + AllPath.Instance.BaseHero);
                foreach (Hero hero in allHeros)
                {
                    DbSetHeros.Add(hero);
                }

                List<Shield> allShield = JsonManager.Instance.GetShieldsFromJSon();
                foreach (Shield shield in allShield)
                {
                    DbSetShield.Add(shield);
                }
                HealerHouse HealerHouse = JsonManager.Instance.GetHealerHouseFromJSon();
                DbSetHealerHouse.Add(HealerHouse);

                Blacksmith Blacksmith = JsonManager.Instance.GetBlacksmithFromJSon();
                DbSetBlacksmith.Add(Blacksmith);
                this.SaveChangesAsync();
            }
        }

        public async void InitCustomLocalMySQL(string customFolder)
        {
            if (this.Database.CreateIfNotExists())
            {
                List<RessourceProducer> allGoldProducer = JsonManager.Instance.GetAllGoldProducersFromJSon();
                foreach (RessourceProducer item in allGoldProducer)
                {
                    DbSetRessourceProducer.Add(item);
                }
                List<SoldiersProducer> allSoldierProducer = JsonManager.Instance.GetAllSoldierProducersFromJSon(AllPath.Instance.JsonCustomFolder + customFolder + AllPath.Instance.CustomSoldierProducer);
                foreach (SoldiersProducer item in allSoldierProducer)
                {
                    DbSetSoldiersProducer.Add(item);
                }
                List<Hero> allHeros = JsonManager.Instance.GetAllHerosFromJSon(AllPath.Instance.JsonCustomFolder + customFolder + AllPath.Instance.CustomHero);
                foreach (Hero hero in allHeros)
                {
                    DbSetHeros.Add(hero);
                }

                List<Shield> allShield = JsonManager.Instance.GetShieldsFromJSon();
                foreach (Shield shield in allShield)
                {
                    DbSetShield.Add(shield);
                }
                HealerHouse HealerHouse = JsonManager.Instance.GetHealerHouseFromJSon();
                DbSetHealerHouse.Add(HealerHouse);

                Blacksmith Blacksmith = JsonManager.Instance.GetBlacksmithFromJSon();
                DbSetBlacksmith.Add(Blacksmith);
                this.SaveChangesAsync();
            }
        }

        public async void DeleteDatabase()
        {
            if (!(this.Database.CreateIfNotExists()))
            {
                this.Database.Delete();
            }
        }
    }
}
