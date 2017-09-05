using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class MySQLArmy : MySQLManager<Army>
    {
        public Army GetHero(Army army)
        {
            this.DbSetT.Attach(army);
            this.Entry(army).Reference(x => x.Hero).Load();
            return army;
        }
    }   
}
