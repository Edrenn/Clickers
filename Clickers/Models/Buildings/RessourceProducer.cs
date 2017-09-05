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
        private int ressourceProducerId;
        public int RessourceProducerId
        {
            get { return ressourceProducerId; }
            set { ressourceProducerId = value; }
        }


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
                OnPropertyChanged("QuantityProduct");
            }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Upgrade()
        {
            this.Level += 1;
            this.QuantityProduct *= 2;
            this.Price *= 2;
        }
    }
}
