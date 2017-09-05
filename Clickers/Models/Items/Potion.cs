using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Base;


namespace Clickers.Models.Items
{
    public class Potion : BaseEntity
    {
        private int potionId;

        public int PotionId
        {
            get { return potionId; }
            set { potionId = value; }
        }


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

            return newPotion;
        }

        public Potion() : base()
        {

        }
    }
}
