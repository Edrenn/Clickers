using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Items;
using Clickers.Models.Base;
using Clickers.Models.Buildings;

namespace Clickers.Models
{
    public class Castle : BaseDBEntity
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

        private int life;
        public int Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
            }
        }

        private Army army;
        public Army Army
        {
            get
            {
                return army;
            }

            set
            {
                army = value;
            }
        }

        private List<RessourceProducer> goldProducers;
        public List<RessourceProducer> GoldProducers
        {
            get { return goldProducers; }
            set { goldProducers = value; }
        }

        private List<SoldiersProducer> soldiersProducers;
        public List<SoldiersProducer> SoldiersProducers
        {
            get { return soldiersProducers; }
            set { soldiersProducers = value; }
        }

        private List<Item> itemStock;
        public List<Item> ItemStock
        {
            get { return itemStock; }
            set { itemStock = value; }
        }


        private List<Hero> heroes;
        public List<Hero> Heroes
        {
            get { return heroes; }
            set { heroes = value; }
        }

        public Castle() { }

        public Castle(string Name)
        {
            this.Name = Name;
            this.Life = 100;
            this.Army = new Army();
            this.GoldProducers = new List<RessourceProducer>();
            this.SoldiersProducers = new List<SoldiersProducer>();
            this.ItemStock = new List<Item>();
        }
    }
}
