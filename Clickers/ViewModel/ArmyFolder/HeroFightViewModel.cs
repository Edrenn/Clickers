using Clickers.Models;
using Clickers.Models.Skills;
using Clickers.ViewModel.SoldierProducer;
using Clickers.Views.HeroFightViews;
using Clickers.Views.TaverneView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using System.ComponentModel;

namespace Clickers.ViewModel.Army
{
    public class HeroFightViewModel : INotifyPropertyChanged
    {
        private HeroDuelFightView view;
        public HeroDuelFightView View
        {
            get { return view; }
            set { view = value; }
        }

        private Hero attackingHero;
        public Hero AttackingHero
        {
            get { return attackingHero; }
            set { attackingHero = value; }
        }

        private Hero defendingHero;
        public Hero DefendingHero
        {
            get { return defendingHero; }
            set { defendingHero = value; }
        }

        private Clickers.Models.Army attackingArmy;

        private Clickers.Models.Army defendingArmy;

        private Castle attackedCastle;

        private Dictionary<Hero, string> actions;
        public Dictionary<Hero, string> Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        private int turn;
        public int Turn
        {
            get { return turn; }
            set
            {
                turn = value;
                RaisePropertyChanged("Turn");
            }
        }

        public HeroFightViewModel(Clickers.Models.Army attackingArmy, Clickers.Models.Army defendingArmy, Castle attackedCastle)
        {
            this.View = new HeroDuelFightView();
            this.Actions = new Dictionary<Hero, string>();
            this.AttackingHero = attackingArmy.Hero;
            this.DefendingHero = defendingArmy.Hero;
            this.attackingArmy = attackingArmy;
            this.defendingArmy = defendingArmy;
            this.attackedCastle = attackedCastle;
            GenerateUI(AttackingHero, DefendingHero);
            View.TurnTB.DataContext = this;
            Switcher.Switch(this.View);
        }

        private void GenerateUI(Hero attackingHero, Hero defendingHero)
        {
            HeroView AllyHeroView = new HeroView();
            AllyHeroView.HeroInfoSP.Visibility = System.Windows.Visibility.Collapsed;
            AllyHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            AllyHeroView.DataContext = attackingHero;
            foreach (Skill skill in attackingHero.Skills)
            {
                SkillViewModel newSkill;
                switch (skill.Type)
                {
                    case "Attaque":
                        newSkill = new SkillViewModel(skill);
                        newSkill.View.SkillLaunchButton.Click += AttaqueButton_Click;
                        newSkill.View.CounterTB.Visibility = System.Windows.Visibility.Collapsed;
                        newSkill.View.SetValue(Grid.ColumnProperty, 0);
                        View.SkillsGrid.Children.Add(newSkill.View);
                        break;
                    case "Parade":
                        newSkill = new SkillViewModel(skill);
                        newSkill.View.SkillLaunchButton.Click += ParadeButton_Click;
                        newSkill.View.SetValue(Grid.ColumnProperty, 1);
                        View.SkillsGrid.Children.Add(newSkill.View);
                        break;
                    case "Feinte":
                        newSkill = new SkillViewModel(skill);
                        newSkill.View.SkillLaunchButton.Click += FeinteButton_Click;
                        newSkill.View.SetValue(Grid.ColumnProperty, 2);
                        View.SkillsGrid.Children.Add(newSkill.View);
                        break;
                    default:
                        break;
                }

            }
            this.View.AllyHeroSP.Children.Add(AllyHeroView);
            this.View.AllyHeroAttributesSP.DataContext = this.AttackingHero;
            this.View.EnnemyHeroAttributesSP.DataContext = this.DefendingHero;

            HeroView EnnemyHeroView = new HeroView();
            EnnemyHeroView.HeroInfoSP.Visibility = System.Windows.Visibility.Collapsed;
            EnnemyHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            EnnemyHeroView.DataContext = defendingHero;
            this.View.ValidateButton.Click += ValidateButton_Click;
            this.View.EnnemyHeroSP.Children.Add(EnnemyHeroView);
        }

        private void ValidateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AITurn();
            DoAllActions();
        }

        private void FeinteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AttackingHero.Skills[2].UseCounter == 0)
            {
                System.Windows.MessageBox.Show("Vous avez utilisé toutes vos feintes");
            }
            else
            {
                if (Actions.ContainsKey(AttackingHero))
                {
                    Actions[AttackingHero] = "Feinte";
                }
                else
                    Actions.Add(AttackingHero, "Feinte");
            }
        }

        private void ParadeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AttackingHero.Skills[2].UseCounter == 0)
            {
                System.Windows.MessageBox.Show("Vous avez utilisé toutes vos parades");
            }
            else
            {
                if (Actions.ContainsKey(AttackingHero))
                {
                    Actions[AttackingHero] = "Parade";
                }
                else
                    Actions.Add(AttackingHero, "Parade");
            }
        }

        private void AttaqueButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Actions.ContainsKey(AttackingHero))
            {
                Actions[AttackingHero] = "Attaque";
            }
            else
                Actions.Add(AttackingHero, "Attaque");
        }

        /// <summary>
        /// Méthode permettant de jouer toutes les actions d'un tour
        /// On récupère l'action sélectionnée par chaque héro et on l'éxecute avec l'ordre suivant :
        /// 1°) Parade 2°) Feinte 3°) Attaque
        /// On remet ensuite toutes les parades à false
        /// </summary>
        private void DoAllActions()
        {
            foreach (KeyValuePair<Hero, string> heroWithAction in Actions)
            {
                if (heroWithAction.Value == "Parade")
                {
                    heroWithAction.Key.IsParing = true;
                }
            }
            foreach (KeyValuePair<Hero, string> heroWithAction in Actions)
            {
                if (heroWithAction.Value == "Feinte")
                {
                    if (heroWithAction.Key == AttackingHero)
                    {
                        Feinte(AttackingHero, DefendingHero);
                    }
                    else
                    {
                        Feinte(DefendingHero, AttackingHero);
                    }
                }
            }
            foreach (KeyValuePair<Hero, string> heroWithAction in Actions)
            {
                if (heroWithAction.Value == "Attaque")
                {
                    if (heroWithAction.Key == AttackingHero)
                    {
                        Attack(AttackingHero, DefendingHero);
                    }
                    else
                    {
                        Attack(DefendingHero, AttackingHero);
                    }
                }
                if (AttackingHero.Life <= 0 ||DefendingHero.Life <= 0)
                {
                    EndFight();
                    break;
                }
            }
            this.AttackingHero.IsParing = false;
            this.DefendingHero.IsParing = false;
            Turn++;
        }

        public void Feinte(Hero feintingHero, Hero ennemyHero)
        {
            feintingHero.Skills[2].UseCounter--;
            ennemyHero.IsParing = false;
        }

        public void Attack(Hero attackingHero, Hero defendingHero)
        {
            if (defendingHero.IsParing == false)
            {
                if (defendingHero.Armor > attackingHero.AttackValue)
                {
                    defendingHero.Armor -= attackingHero.AttackValue;
                }
                else if (defendingHero.Armor == 0)
                {
                    defendingHero.Life -= attackingHero.AttackValue;
                }
                else
                {
                    defendingHero.Life -= (attackingHero.AttackValue - defendingHero.Armor);
                    defendingHero.Armor = 0;
                }
            }
            else
            {
                Attack(attackingHero, attackingHero);
            }
        }

        /// <summary>
        /// Méthode gérant le tour de l'adversaire. (Intelligence artificielle)
        /// </summary>
        private void AITurn()
        {
            /*Génération d'un nombre au hasard entre 0 et 40 pour définir l'action à réaliser :
             * 0 à 10 Attaque
             * 11 à 20 Feinte
             * 21 à 30 Parade
             * >30 Objet
            */
            Random rand = new Random();
            int actionNumber = rand.Next(0, 41);
            if (actionNumber <= 10)
            {
                if (Actions.ContainsKey(DefendingHero))
                {
                    Actions[DefendingHero] = "Attaque";
                }
                else
                    Actions.Add(DefendingHero, "Attaque");
            }
            else if (actionNumber > 10 && actionNumber <= 20)
            {
                if (DefendingHero.Skills[1].UseCounter == 0)
                {
                    AITurn();
                }
                else
                {
                    if (Actions.ContainsKey(DefendingHero))
                    {
                        Actions[DefendingHero] = "Feinte";
                    }
                    else
                        Actions.Add(DefendingHero, "Feinte");
                }
            }
            else if (actionNumber > 20 && actionNumber <= 30)
            {
                if (DefendingHero.Skills[2].UseCounter == 0)
                {
                    AITurn();
                }
                else
                {
                    if (Actions.ContainsKey(DefendingHero))
                    {
                        Actions[DefendingHero] = "Parade";
                    }
                    else
                        Actions.Add(DefendingHero, "Parade");
                }
            }
            else
            {
                if (Actions.ContainsKey(DefendingHero))
                {
                    Actions[DefendingHero] = "Object";
                }
                else
                    Actions.Add(DefendingHero, "Object");
            }
        }

        private void EndFight()
        {
            MessageBoxResult msgBxResult;
            if (DefendingHero.Life <= 0)
            {
                msgBxResult = System.Windows.MessageBox.Show("You win");
            }
            else
                msgBxResult = System.Windows.MessageBox.Show("You lose");

            if (msgBxResult == MessageBoxResult.OK)
            {
                launchBattle();
            }

        }

        private void launchBattle()
        {
            GameViewModel.Instance.EnnemyCastle.Army.GenerateEnnemyArmy();
            Battle newBattle = new Battle(attackingArmy,defendingArmy,attackedCastle);
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
