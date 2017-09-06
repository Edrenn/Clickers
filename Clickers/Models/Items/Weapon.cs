using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clickers.Models.Base;

namespace Clickers.Models.Items
{
    public class Weapon : BaseEntity
    {
        private int weaponId;

        public int WeaponId
        {
            get { return weaponId; }
            set { weaponId = value; }
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

        private int damageValue;
        public int DamageValue
        {
            get { return damageValue; }
            set { damageValue = value; }
        }

        public Weapon DuplicateWeapon()
        {
            Weapon newWeapon = new Weapon(); 
            newWeapon.Description = this.Description;
            newWeapon.DamageValue = this.DamageValue;
            newWeapon.ImagePath = this.ImagePath;
            newWeapon.Name = this.Name;
            newWeapon.Price = this.Price;

            return newWeapon;
        }
    }
}
