using Clickers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class MySQLHero : MySQLManager<Hero>
    {
        public MySQLHero() : base()
        {

        }

        public Hero GetProperties(Hero hero)
        {
            this.DbSetT.Attach(hero);
            this.Entry(hero).Reference(h => h.Weapon).Load();
            this.Entry(hero).Reference(h => h.Shield).Load();
            this.Entry(hero).Reference(h => h.Potion).Load();
            this.Entry(hero).Collection(x => x.Skills).Load();
            return hero;
        }

        public Hero GetSkills(Hero hero)
        {
            this.DbSetT.Attach(hero);
            this.Entry(hero).Collection(x => x.Skills).Load();
            return hero;
        }
    }
}
