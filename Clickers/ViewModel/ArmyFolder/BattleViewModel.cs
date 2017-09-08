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

        private Castle defendingCastle;
        public Castle DefendingCastle
        {
            get
            {
                return defendingCastle;
            }

            set
            {
                defendingCastle = value;
            }
        }

        private Castle attackingCastke;
        public Castle AttackingCastle
        {
            get { return attackingCastke; }
            set { attackingCastke = value; }
        }

        private bool isEnnemyAttack;

        public BattleViewModel(Castle attackingCastle, Castle defendingCastle, bool isEnnemyAttack)
        {
            this.AttackSoldiers = attackingCastle.Army.AllSoldiers;
            this.DefenseSoldiers = defendingCastle.Army.AllSoldiers;
            this.AttackDeaths = new List<Soldier>();
            this.DefenseDeaths = new List<Soldier>();
            this.DefendingCastle = defendingCastle;
            this.AttackingCastle = attackingCastle;
            this.rng = new Random();
            this.isEnnemyAttack = isEnnemyAttack;

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                this.View = new BattleReport();

                Randomizer(AttackSoldiers);
                Randomizer(DefenseSoldiers);
                Fight();

                if (isEnnemyAttack)
                {
                    if (AttackWin == true)
                    {
                        view.WinOrLoseLabel.Content = "DÉFAITE...";
                        this.DefendingCastle.Life -= 25;
                    }
                    else
                    {
                        view.WinOrLoseLabel.Content = "VICTOIRE";
                    }
                }
                else
                {
                    if (AttackWin == true)
                    {
                        view.WinOrLoseLabel.Content = "VICTOIRE";
                        this.DefendingCastle.Life -= 25;
                    }
                    else
                    {
                        view.WinOrLoseLabel.Content = "DÉFAITE...";
                    }
                }

                if (isEnnemyAttack)
                {
                    view.AllyUnitsloseTB.Text = "Unités alliées détruites : " + DefenseDeaths.Count;
                    view.AllyUnitsRestTB.Text = "Unités restantes : " + (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count - DefenseDeaths.Count);
                    view.EnnemyUnitsloseTB.Text = "Unités ennemies détruites : " + AttackDeaths.Count;
                    view.EnnemyUnitsRestTB.Text = "Unités ennemies restantes : " + (GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Count - AttackDeaths.Count);
                }
                else
                {
                    view.AllyUnitsloseTB.Text = "Unités alliées détruites : " + AttackDeaths.Count;
                    view.AllyUnitsRestTB.Text = "Unités restantes : " + (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count - AttackDeaths.Count);
                    view.EnnemyUnitsloseTB.Text = "Unités ennemies détruites : " + DefenseDeaths.Count;
                    view.EnnemyUnitsRestTB.Text = "Unités ennemies restantes : " + (GameViewModel.Instance.EnnemyCastle.Army.AllSoldiers.Count - DefenseDeaths.Count);
                }
                Switcher.Switch(view);
                WashingBodies();
            }));
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
            if (this.isEnnemyAttack)
            {
                foreach (Soldier soldier in this.DefenseDeaths)
                {
                    if (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Contains(soldier))
                    {
                        GameViewModel.Instance.MainCastle.Army.AllSoldiers.Remove(soldier);
                    }
                }
            }
            else
            {
                foreach (Soldier soldier in this.AttackDeaths)
                {
                    if (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Contains(soldier))
                    {
                        GameViewModel.Instance.MainCastle.Army.AllSoldiers.Remove(soldier);
                    }
                }
            }
            
            if (GameViewModel.Instance.EnnemyCastle.Army.Hero.Life <= 0)
            {
                GameViewModel.Instance.EnnemyCastle.Army.Hero = null;
            }
        }

        private void Fight()
        {
            int defendingSoldierCounter = 0;
            foreach (Soldier soldier in AttackSoldiers)
            {
                while (soldier.Health > 0)
                {
                    DamageTest(soldier, DefenseSoldiers[defendingSoldierCounter]);

                    // If the attacking soldier kill his opponent
                    if (DefenseSoldiers[defendingSoldierCounter].Health <= 0)
                    {
                        DefenseDeaths.Add(DefenseSoldiers[defendingSoldierCounter]);
                        defendingSoldierCounter++;
                        if (defendingSoldierCounter == DefenseSoldiers.Count)
                        {
                            AttackWin = true;
                            break;
                        }
                    }

                    DamageTest(DefenseSoldiers[defendingSoldierCounter], soldier);
                    // If the defending soldier kill his opponent
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
