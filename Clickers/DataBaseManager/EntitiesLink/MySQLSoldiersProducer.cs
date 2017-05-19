using Clickers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class MySQLSoldiersProducer : MySQLManager<SoldiersProducer>
    {
        public void GetSoldierType(SoldiersProducer soldiersProducer)
        {
            bool isDetached = this.Entry(soldiersProducer).State == EntityState.Detached;
            if (isDetached)
                this.DbSetT.Attach(soldiersProducer);
            Entry(soldiersProducer).Reference(x => x.SoldierType).Load();
        }
    }
}
