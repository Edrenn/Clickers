using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Json;
using Clickers.Models;
using Clickers.Models.Generators;
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
                List<SoldiersProducer> allSoldierProducer = JsonManager.Instance.GetAllSoldierProducersFromJSon();
                foreach (SoldiersProducer item in allSoldierProducer)
                {
                    DbSetSoldiersProducer.Add(item);
                }
                List<Hero> allHeros = JsonManager.Instance.GetAllHerosFromJSon();
                foreach (Hero hero in allHeros)
                {
                    DbSetHeros.Add(hero);
                }
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
