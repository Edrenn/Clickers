using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Items
{
    public class Potion : Item
    {
        private int healValue;
        public int HealValue
        {
            get { return healValue; }
            set { healValue = value; }
        }

        public Potion DuplicatePotion()
        {
            Potion newPotion = new Potion();
            newPotion.Description = this.Description;
            newPotion.HealValue = this.HealValue;
            newPotion.ImagePath = this.ImagePath;
            newPotion.Name = this.Name;
            newPotion.Price = this.Price;
            newPotion.Type = this.Type;

            return newPotion;
        }
    }
}
