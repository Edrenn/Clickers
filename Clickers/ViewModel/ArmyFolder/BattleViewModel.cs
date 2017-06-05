using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.ViewModel.ArmyFolder
{
    public class BattleViewModel
    {
        private Random rng;
        Battle newBattle;

        private BattleReport view;
        public BattleReport View
        {
            get { return view; }
            set { view = value; }
        }

        private List<Soldier> attackSoldiers;
        public List<Soldier> AttackSoldiers
        {
            get
            {
                return attackSoldiers;
            }

            set
            {
                attackSoldiers = value;
            }
        }

        private List<Soldier> defenseSoldiers;
        public List<Soldier> DefenseSoldiers
        {
            get
            {
                return defenseSoldiers;
            }

            set
            {
                defenseSoldiers = value;
            }
        }

        private List<Soldier> attackDeaths;
        public List<Soldier> AttackDeaths
        {
            get
            {
                return attackDeaths;
            }

            set
            {
                attackDeaths = value;
            }
        }

        private List<Soldier> defenseDeaths;
        public List<Soldier> DefenseDeaths
        {
            get
            {
                return defenseDeaths;
            }

            set
            {
                defenseDeaths = value;
            }
        }

        private bool attackWin;
        public bool AttackWin
        {
            get
            {
                return attackWin;
            }

            set
            {
                attackWin = value;
            }
        }

        private Castle attackedCastle;
        public Castle AttackedCastle
        {
            get
            {
                return attackedCastle;
            }

            set
            {
                attackedCastle = value;
            }
        }


        public BattleViewModel(Clickers.Models.Army attackingArmy, Clickers.Models.Army defenseArmy, Castle attackedCastle)
        {
            this.AttackSoldiers = new List<Soldier>();
            this.DefenseSoldiers = new List<Soldier>();
            this.AttackDeaths = new List<Soldier>();
            this.DefenseDeaths = new List<Soldier>();
            this.AttackedCastle = attackedCastle;
            this.rng = new Random();
            this.View = new BattleReport();

            if ((attackingArmy.Hero == null || attackingArmy.Hero.Life <= 0) && (defenseArmy.Hero != null || defenseArmy.Hero.Life > 0))
            {
                ArmyCreation(attackingArmy, AttackSoldiers);
                ArmyCreationWithHero(defenseArmy, DefenseSoldiers);
            }
            else if ((defenseArmy.Hero == null || defenseArmy.Hero.Life <= 0) && (attackingArmy.Hero != null || attackingArmy.Hero.Life > 0))
            {
                ArmyCreation(defenseArmy, DefenseSoldiers);
                ArmyCreationWithHero(attackingArmy, AttackSoldiers);
            }
            else
            {
                ArmyCreation(attackingArmy, AttackSoldiers);
                ArmyCreation(defenseArmy, DefenseSoldiers);
            }

            Randomizer(AttackSoldiers);
            Randomizer(DefenseSoldiers);
            Fight();


            if (AttackWin == true)
            {
                view.WinOrLoseLabel.Content = "VICTOIRE";
                this.AttackedCastle.Life -= 25;
            }
            else
                view.WinOrLoseLabel.Content = "DÉFAITE..";
            if (this.AttackedCastle.Life <= 0)
            {
                System.Windows.MessageBox.Show("Fin du Game");
            }
            else
            {
                view.AllyUnitslose.Text = "Unités attaquantes perdue : " + AttackDeaths.Count;
                view.AllyUnitsRest.Text = "Unités attaquantes restantes : " + (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count - AttackDeaths.Count);
                view.EnnemyUnitslose.Text = "Unités défendantes perdue : " + DefenseDeaths.Count;
                view.EnnemyUnitsRest.Text = "Unités défendantes restantes : " + (GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Count - DefenseDeaths.Count);
                Switcher.Switch(view);
                EventGenerator();
            }
        }

        private void EventGenerator()
        {
            view.ToCastle.Click += ToCastle_Click;
        }

        private void ToCastle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WashingBodies();
            foreach (Hero hero in GameViewModel.Instance.MainCastle.Heroes)
            {

            }
            MainCastleView castleView = new MainCastleView();
            Switcher.Switch(castleView);
        }
        
        /// <summary>
        /// Refresh the Army
        /// </summary>
        private void WashingBodies()
        {
            foreach (Soldier soldier in AttackDeaths)
            {
                if (AttackSoldiers.Contains(soldier))
                {
                    AttackSoldiers.Remove(soldier);
                }
            }
            GameViewModel.Instance.MainCastle.Army.AllSoldiers.Clear();
            foreach (Soldier soldier in AttackSoldiers)
            {
                Soldier newSoldier = new Soldier();
                newSoldier.InitializeSoldier(soldier);
                GameViewModel.Instance.MainCastle.Army.AllSoldiers.Add(newSoldier);
            }
        }

        private void Fight()
        {
            int ennemySoldier = 0;
            foreach (Soldier soldier in AttackSoldiers)
            {
                while (soldier.Health > 0)
                {
                    DamageTest(soldier, DefenseSoldiers[ennemySoldier]);
                    if (DefenseSoldiers[ennemySoldier].Health <= 0)
                    {
                        DefenseDeaths.Add(DefenseSoldiers[ennemySoldier]);
                        ennemySoldier++;
                        if (ennemySoldier == DefenseSoldiers.Count)
                        {
                            AttackWin = true;
                            break;
                        }
                    }
                    DamageTest(DefenseSoldiers[ennemySoldier], soldier);
                    if (soldier.Health <= 0)
                    {
                        AttackDeaths.Add(soldier);
                    }
                }
                if (AttackWin == true)
                {
                    break;
                }
            }
        }

        private void DamageTest(Soldier attackingSoldier, Soldier defendingSoldier)
        {
            if (defendingSoldier.Type == attackingSoldier.StrongAgainst)
            {
                defendingSoldier.Health -= attackingSoldier.AttackValue * 2;
            }
            else
                defendingSoldier.Health -= attackingSoldier.AttackValue;
        }

        private void ArmyCreation(Clickers.Models.Army Army, List<Soldier> listToFill)
        {
            foreach (Soldier soldier in Army.AllSoldiers)
            {
                Soldier newSoldier = new Soldier();
                newSoldier.InitializeSoldier(soldier);
                listToFill.Add(newSoldier);
            }

        }

        private void ArmyCreationWithHero(Clickers.Models.Army Army, List<Soldier> listToFill)
        {
            foreach (Soldier soldier in Army.AllSoldiers)
            {
                Soldier newSoldier;
                if (Army.Hero != null && soldier.Name == Army.Hero.Type)
                {
                    newSoldier = new Soldier();
                    newSoldier.InitializeSoldier(soldier);
                    newSoldier.AttackValue += 5;
                }
                else
                {
                    newSoldier = new Soldier();
                    newSoldier.InitializeSoldier(soldier);
                }
                listToFill.Add(newSoldier);
            }

        }

        private void Randomizer(List<Soldier> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Soldier value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
