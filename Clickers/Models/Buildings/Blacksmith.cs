using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Items;

namespace Clickers.Models.Buildings
{
    public class Blacksmith : Building
    {
        private int blacksmithId;
        public int BlacksmithId
        {
            get { return blacksmithId; }
            set { blacksmithId = value; }
        }


        private int repairPrice;
        public int RepairPrice
        {
            get { return repairPrice; }
            set { repairPrice = value; }
        }

        private List<Weapon> weaponList;
        public List<Weapon> WeaponList
        {
            get { return weaponList; }
            set { weaponList = value; }
        }

        private List<Shield> shieldList;
        public List<Shield> ShieldList
        {
            get { return shieldList; }
            set { shieldList = value; }
        }

        public Blacksmith()
        {
            this.WeaponList = new List<Weapon>();
            this.ShieldList = new List<Shield>();
        }
    }
}
