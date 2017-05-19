using Clickers.DataBaseManager;
using Clickers.Models.Base;
using Clickers.DataBaseManager.EntitiesLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models
{
    public class Army : BaseDBEntity
    {
        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }

        List<Soldier> allSoldiers;
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

        public Army()
        {
            allSoldiers = new List<Soldier>();
        }

        public void GenerateHero()
        {
            Random random = new Random();
            MySQLManager<Hero> MyHeroSQLManager = new MySQLManager<Hero>();
            Hero newHero = null;
            MySQLHero heroSQL = new MySQLHero();
            int testTypeHero = random.Next(0, 40);
            if (testTypeHero <= 10)
            {
                Task<Hero> TaskHero = MyHeroSQLManager.Get(1);
                newHero = TaskHero.Result;
                newHero = heroSQL.GetSkills(newHero);
            }
            else if (testTypeHero >= 20 && testTypeHero < 30)
            {
                Task<Hero> TaskHero = MyHeroSQLManager.Get(2);
                newHero = TaskHero.Result;
                newHero = heroSQL.GetSkills(newHero);
            }
            else if (testTypeHero > 40)
            {

            }
            else
            {
                Task<Hero> TaskHero = MyHeroSQLManager.Get(3);
                newHero = TaskHero.Result;
                newHero = heroSQL.GetSkills(newHero);
            }
            GameViewModel.Instance.EnnemyCastle.Army.Hero = newHero;
        }

        public void GenerateEnnemyArmy()
        {
            GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Clear();
            MySQLManager<Soldier> MySoldierSQLManager = new MySQLManager<Soldier>();
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
                Soldier newSoldier;
                int testType = random.Next(0, 30);
                if (testType <= 10)
                {
                    Task<Soldier> TaskSoldier = MySoldierSQLManager.Get(1);
                    newSoldier = TaskSoldier.Result;
                }
                else if (testType > 20)
                {
                    Task<Soldier> TaskSoldier = MySoldierSQLManager.Get(2);
                    newSoldier = TaskSoldier.Result;
                }
                else
                {
                    Task<Soldier> TaskSoldier = MySoldierSQLManager.Get(3);
                    newSoldier = TaskSoldier.Result;
                }
                GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Add(newSoldier);
            }
        }
    }
}
