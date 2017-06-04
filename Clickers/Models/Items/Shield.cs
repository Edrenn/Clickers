using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clickers.Models.Base;

namespace Clickers.Models.Items
{
    public class Shield : BaseDBEntity
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private int price;
        public int Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private int armorValue;
        public int ArmorValue
        {
            get { return armorValue; }
            set { armorValue = value; }
        }

        public Shield DuplicateShield()
        {
            Shield newShield = new Shield();
            newShield.Description = this.Description;
            newShield.ArmorValue = this.ArmorValue;
            newShield.ImagePath = this.ImagePath;
            newShield.Name = this.Name;
            newShield.Price = this.Price;

            return newShield;
        }

        public Shield()
        {

        }
    }
}
