using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Items;
using Clickers.Models.Buildings;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class MySQLHealerHouse : MySQLManager<HealerHouse>
    {
        public MySQLHealerHouse() : base()
        {

        }

        public HealerHouse GetHealerHouse(HealerHouse healerHouse)
        {
            this.DbSetT.Attach(healerHouse);
            this.Entry(healerHouse).Collection<Potion>(x => x.PotionList).Load();
            return healerHouse;
        }
    }
}
