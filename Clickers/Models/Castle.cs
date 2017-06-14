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

        private int golds;
        public int Golds
        {
            get { return golds; }
            set { golds = value; }
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

        //private Dictionary<String,RessourceProducer> goldProducers;
        //public Dictionary<String,RessourceProducer> GoldProducers
        //{
        //    get { return goldProducers; }
        //    set { goldProducers = value; }
        //}

        private List<RessourceProducer> goldProducers;
        public List< RessourceProducer> GoldProducers
        {
            get { return goldProducers; }
            set { goldProducers = value; }
        }

        private Dictionary<String, SoldiersProducer> soldiersProducers;
        public Dictionary<String,SoldiersProducer> SoldiersProducers
        {
            get { return soldiersProducers; }
            set { soldiersProducers = value; }
        }

        private List<Shield> shieldStock;
        public List<Shield> ShieldStock
        {
            get { return shieldStock; }
            set { shieldStock = value; }
        }

        private List<Weapon> weaponStock;
        public List<Weapon> WeaponStock
        {
            get { return weaponStock; }
            set { weaponStock = value; }
        }

        private List<Potion> potionStock;
        public List<Potion> PotionStock
        {
            get { return potionStock; }
            set { potionStock = value; }
        }
        
        private List<Hero> heroes;
        public List<Hero> Heroes
        {
            get { return heroes; }
            set { heroes = value; }
        }

        private Blacksmith blacksmith;
        public Blacksmith Blacksmith
        {
            get { return blacksmith; }
            set { blacksmith = value; }
        }

        private HealerHouse healer;

        public HealerHouse Healer
        {
            get { return healer; }
            set { healer = value; }
        }


        public Castle() { }

        public Castle(string Name)
        {
            this.Name = Name;
            this.Life = 100;
            this.Army = new Army();
            this.GoldProducers = new List<RessourceProducer>();
            this.SoldiersProducers = new Dictionary<String, SoldiersProducer>();
            this.ShieldStock = new List<Shield>();
            this.WeaponStock = new List<Weapon>();
            this.PotionStock = new List<Potion>();
        }
    }
}
