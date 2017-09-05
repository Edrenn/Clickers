using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Items;

namespace Clickers.Models.Buildings
{
    public class HealerHouse : Building
    {

        private int healerHouseId;
        public int HealerHouseId
        {
            get { return healerHouseId; }
            set { healerHouseId = value; }
        }

        private List<Potion> potionList;
        public List<Potion> PotionList
        {
            get { return potionList; }
            set { potionList = value; }
        }

        private int priceToHeal;
        public int PriceToHeal
        {
            get { return priceToHeal; }
            set { priceToHeal = value; }
        }

        public HealerHouse()
        {
            this.PotionList = new List<Potion>();
        }
    }
}
