using Clickers.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models
{
    public class RessourceProducer : BaseDBEntity
    {
        private string name;
        private int price;
        private string typeRessource;
        private int productSpeed;
        private int quantityProduct;
        private int level;
        private bool isActive;
        private string imagePath;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

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

        public int Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
                RaisePropertyChanged("Price");
            }
        }

        public string TypeRessource
        {
            get
            {
                return typeRessource;
            }

            set
            {
                typeRessource = value;
            }
        }

        public int ProductSpeed
        {
            get
            {
                return productSpeed;
            }

            set
            {
                productSpeed = value;
            }
        }

        public int QuantityProduct
        {
            get
            {
                return quantityProduct;
            }

            set
            {
                quantityProduct = value;
                RaisePropertyChanged("QuantityProduct");
            }
        }

        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
                RaisePropertyChanged("Level");
            }
        }
    }
}
