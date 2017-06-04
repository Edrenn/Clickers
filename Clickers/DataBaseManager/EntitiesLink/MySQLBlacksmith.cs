using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Buildings;
using Clickers.Models.Items;

namespace Clickers.DataBaseManager.EntitiesLink
{
    class MySQLBlacksmith : MySQLManager<Blacksmith>
    {
        public MySQLBlacksmith() : base()
        {

        }

        public Blacksmith SetItems(Blacksmith blacksmith)
        {
            this.DbSetT.Attach(blacksmith);
            this.Entry(blacksmith).Collection(x => x.ShieldList).Load();
            this.Entry(blacksmith).Collection(x => x.WeaponList).Load();
            return blacksmith;
        }
    }
}
