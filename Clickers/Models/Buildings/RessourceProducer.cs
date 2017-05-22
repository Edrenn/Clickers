using Clickers.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models.Buildings
{
    public class RessourceProducer : Building
    {
        private string typeRessource;
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

        private int productSpeed;
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

        private int quantityProduct;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
