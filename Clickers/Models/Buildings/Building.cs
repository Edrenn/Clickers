using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Base;

namespace Clickers.Models.Buildings
{
    public class Building : BaseDBEntity
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private int level;
        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
    }
}
