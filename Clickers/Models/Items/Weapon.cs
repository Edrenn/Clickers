using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Items
{
    public class Weapon : Item
    {
        private int damageValue;
        public int DamageValue
        {
            get { return damageValue; }
            set { damageValue = value; }
        }

    }
}
