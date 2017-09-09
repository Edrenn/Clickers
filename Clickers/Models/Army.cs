using Clickers.DataBaseManager;
using Clickers.Models.Base;
using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models
{
    public class Army : BaseEntity
    {
        private int armyId;

        public int ArmyId
        {
            get { return armyId; }
            set { armyId = value; }
        }

        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        private List<Soldier> allSoldiers;
        [NotMapped]
        public List<Soldier> AllSoldiers
        {
            get
            {
                return allSoldiers;
            }

            set
            {
                allSoldiers = value;
            }
        }

        public int chevCounter { get; set; }
        public int cavCounter { get; set; }
        public int archCounter { get; set; }

        public Army()
        {
            allSoldiers = new List<Soldier>();
        }

        public void GenerateHero()
        {
            Random random = new Random();

            List<Hero> allHeroes = JsonManager.Instance.GetAllHerosFromJSon(AllPath.Instance.JsonFolder + AllPath.Instance.BaseHero);
            MySQLHero heroSQL = new MySQLHero();

            Hero hero1 = allHeroes[0];
            Hero hero2 = allHeroes[1];
            Hero hero3 = allHeroes[2];

            Hero newHero = new Hero();
            int testTypeHero = random.Next(0, 40);
            if (testTypeHero <= 10)
            {
                newHero.InitializeHero(hero1);
            }
            else if (testTypeHero >= 20 && testTypeHero < 30)
            {
                newHero.InitializeHero(hero2);
            }
            else if (testTypeHero > 40)
            {

            }
            else
            {
                newHero.InitializeHero(hero3);
            }
            GameViewModel.Instance.EnnemyCastle.Army.Hero = newHero;
        }

        public void GenerateEnnemyArmy()
        {
            GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Clear();
            MySQLManager<Soldier> MySoldierSQLManager = new MySQLManager<Soldier>();
            Soldier chevalier = MySoldierSQLManager.Get(1).Result;
            Soldier archer = MySoldierSQLManager.Get(2).Result;
            Soldier cavalier = MySoldierSQLManager.Get(3).Result;


            Random random = new Random();
            int soldierNumber;
            if (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count <= 5)
            {
                int newVaratiation = GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count - 1;
                soldierNumber = random.Next(2, GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count + newVaratiation);
            }
            else
                soldierNumber = random.Next(GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count - 5, GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count + 5);
            for (int counter = 0; counter < soldierNumber; counter++)
            {
                Soldier newSoldier = new Soldier(); ;

                int testType = random.Next(0, 30);
                if (testType <= 10)
                {
                    newSoldier.CopySoldier(chevalier);
                }
                else if (testType > 20)
                {
                    newSoldier.CopySoldier(archer);
                }
                else
                {
                    newSoldier.CopySoldier(cavalier);
                }
                GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Add(newSoldier);
            }
        }

        public Army BoostArmy()
        {
            foreach (Soldier soldier in this.AllSoldiers)
            {
                Soldier newSoldier;
                if (this.Hero != null && soldier.Name == this.Hero.Type)
                { 
                    soldier.AttackValue += 5;
                } 
            }
            return this;
        }
    }
}
