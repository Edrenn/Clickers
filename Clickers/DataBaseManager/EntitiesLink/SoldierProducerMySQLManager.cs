using Clickers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class SoldierProducerMySQLManager : MySQLManager<SoldiersProducer>
    {

        public SoldierProducerMySQLManager() : base()
        {

        }

        public SoldiersProducer GetSoldiersProducer(SoldiersProducer soldiersProducer)
        {
            this.DbSetT.Attach(soldiersProducer);
            this.Entry(soldiersProducer).Reference(x => x.SoldierType).Load();
            return soldiersProducer;
        }
    }
}
