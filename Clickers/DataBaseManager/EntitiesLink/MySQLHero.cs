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
        public Hero GetSkills(Hero hero)
        {
            this.DbSetT.Attach(hero);
            this.Entry(hero).Collection(x => x.Skills).Load();
            return hero;
        }
    }
}
