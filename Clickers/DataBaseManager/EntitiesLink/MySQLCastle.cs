using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models;
using Clickers.Models.Buildings;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class MySQLCastle : MySQLManager<Castle>
    {
        public Castle GetProperties(Castle castle)
        {
            this.DbSetT.Attach(castle);

            this.Entry(castle).Reference(c => c.Army).Load();
            this.Entry(castle.Army).Reference(a => a.Hero).Load();

            this.Entry(castle).Collection(c => c.GoldProducers).Load();

            this.Entry(castle).Collection(c => c.SoldiersProducers).Load();
            foreach (SoldiersProducer SP in castle.SoldiersProducers)
            {
                this.Entry(SP).Reference(sp => sp.SoldierType).Load();
            }

            this.Entry(castle).Collection(c => c.Heroes).Load();
            foreach (Hero hero in castle.Heroes)
            {
                this.Entry(hero).Collection(h => h.Skills).Load();
                this.Entry(hero).Reference(h => h.Weapon).Load();
                this.Entry(hero).Reference(h => h.Shield).Load();
                this.Entry(hero).Reference(h => h.Potion).Load();
            }

            this.Entry(castle).Reference(c => c.Blacksmith).Load();
            this.Entry(castle.Blacksmith).Collection(b => b.ShieldList).Load();
            this.Entry(castle.Blacksmith).Collection(b => b.WeaponList).Load();


            this.Entry(castle).Reference(c => c.Healer).Load();
            this.Entry(castle.Healer).Collection(b => b.PotionList).Load();
            return castle;
        }
    }
}
