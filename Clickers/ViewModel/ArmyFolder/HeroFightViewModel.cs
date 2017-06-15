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
        private Random rand;
        public Random Rand
        {
            get { return rand; }
            set { rand = value; }
        }


        private HeroDuelFightView view;
        public HeroDuelFightView View
        {
            get { return view; }
            set { view = value; }
        }

        private Hero allyHero;
        public Hero AllyHero
        {
            get { return allyHero; }
            set { allyHero = value; }
        }

        private Hero enemyHero;
        public Hero EnemyHero
        {
            get { return enemyHero; }
            set { enemyHero = value; }
        }

        private Clickers.Models.Army attackingArmy;

        private Clickers.Models.Army defendingArmy;

        private Castle attackedCastle;
        private Castle attackingCastle;

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

        public HeroFightViewModel(Clickers.Models.Army attackingArmy, Clickers.Models.Army defendingArmy, Castle attackedCastle, Castle attackingCastle)
        {
            this.Rand = new Random();
            this.View = new HeroDuelFightView();
            this.Actions = new Dictionary<Hero, string>();
            this.AllyHero = attackingArmy.Hero;
            this.EnemyHero = defendingArmy.Hero;
            this.attackingArmy = attackingArmy;
            this.defendingArmy = defendingArmy;
            this.attackedCastle = attackedCastle;
            this.attackingCastle = attackingCastle;
            GenerateUI(AllyHero, EnemyHero);
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
            this.View.AllyHeroAttributesSP.DataContext = this.AllyHero;
            this.View.EnnemyHeroAttributesSP.DataContext = this.EnemyHero;

            HeroView EnnemyHeroView = new HeroView();
            EnnemyHeroView.HeroInfoSP.Visibility = System.Windows.Visibility.Collapsed;
            EnnemyHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            EnnemyHeroView.DataContext = defendingHero;
            this.View.EnnemyHeroSP.Children.Add(EnnemyHeroView);
            this.View.ItemsButton.Click += ItemsButton_Click;
        }

        private void ItemsButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AllyHero.Potion != null)
            {
                DoAllActions("Objet", AITurn());
                this.View.AllyHeroSkillUsed.Text = "Objet";
            }
            else
                System.Windows.MessageBox.Show("Vous n'avez pas de potion dans votre inventaire");
        }

        private void FeinteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DoAllActions("Feinte", AITurn());
            this.View.AllyHeroSkillUsed.Text = "Feinte";
            //if (AllyHero.Skills[2].UseCounter == 0)
            //{
            //    System.Windows.MessageBox.Show("Vous avez utilisé toutes vos feintes");
            //}
            //else
            //{
            //    if (Actions.ContainsKey(AllyHero))
            //    {
            //        Actions[AllyHero] = "Feinte";
            //    }
            //    else
            //        Actions.Add(AllyHero, "Feinte");
            //}
        }

        private void ParadeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AllyHero.Skills[1].UseCounter == 0)
            {
                System.Windows.MessageBox.Show("Vous avez utilisé toutes vos parades");
            }
            else
            {

                DoAllActions("Parade", AITurn());
                AllyHero.Skills[1].UseCounter--;
                this.View.AllyHeroSkillUsed.Text = "Parade";
                //if (Actions.ContainsKey(AllyHero))
                //{
                //    Actions[AllyHero] = "Parade";
                //}
                //else
                //    Actions.Add(AllyHero, "Parade");
            }
        }

        private void AttaqueButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DoAllActions("Attaque", AITurn());
            this.View.AllyHeroSkillUsed.Text = "Attaque";
        }

        /// <summary>
        /// Méthode permettant de jouer toutes les actions d'un tour
        /// On récupère l'action sélectionnée par chaque héros et on l'éxecute avec l'ordre suivant :
        /// 1°) Parade 2°) Feinte 3°) Attaque
        /// On remet ensuite toutes les parades à false
        /// </summary>
        private void DoAllActions(String allyHeroAction, String enemyHeroAction)
        {
            if (allyHeroAction == "Objet")
            {
                if (this.AllyHero.Potion != null)
                {
                    this.Heal(this.AllyHero);
                }
            }
            if (enemyHeroAction == "Objet")
            {
                this.Heal(this.EnemyHero);
            }
            if (allyHeroAction == "Parade")
            {
                this.AllyHero.IsParing = true;
            }
            if (enemyHeroAction == "Parade")
            {
                this.EnemyHero.IsParing = true;
            }
            if (allyHeroAction == "Feinte")
            {
                this.Feinte(this.AllyHero, this.EnemyHero);
            }
            if (enemyHeroAction == "Feinte")
            {
                this.Feinte(this.EnemyHero, this.AllyHero);
            }
            if (allyHeroAction == "Attaque")
            {
                this.Attack(this.AllyHero, this.EnemyHero);
                if (this.AllyHero.Life <= 0 || this.EnemyHero.Life <= 0)
                {
                    EndFight();
                }
            }
            if (enemyHeroAction == "Attaque")
            {
                this.Attack(this.EnemyHero, this.AllyHero);

                if (this.AllyHero.Life <= 0 || this.EnemyHero.Life <= 0)
                {
                    EndFight();
                }
            }
            this.AllyHero.IsParing = false;
            this.EnemyHero.IsParing = false;
            Turn++;
        }

        public void Heal(Hero hero)
        {
            hero.Life += hero.Potion.HealValue;
            if (hero.Life > hero.BaseLife)
            {
                hero.Life = hero.BaseLife;
            }
            hero.Potion = null;
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
                if (defendingHero.Armor > attackingHero.Attack)
                {
                    defendingHero.Armor -= attackingHero.Attack;
                }
                else if (defendingHero.Armor == 0)
                {
                    defendingHero.Life -= attackingHero.Attack;
                }
                else
                {
                    defendingHero.Life -= (attackingHero.Attack - defendingHero.Armor);
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
        private String AITurn()
        {
            String actionToReturn = "";
            /*Génération d'un nombre au hasard entre 0 et 40 pour définir l'action à réaliser :
             * 0 à 10 Attaque
             * 11 à 20 Feinte
             * 21 à 30 Parade
             * >30 Objet
            */
            int actionNumber = this.Rand.Next(0, 41);
            if (actionNumber <= 10)
            {
                actionToReturn = "Attaque";
                this.View.EnemyHeroSkillUsed.Text = actionToReturn;
            }
            else if (actionNumber > 10 && actionNumber <= 20)
            {
                actionToReturn = "Feinte";
                this.View.EnemyHeroSkillUsed.Text = actionToReturn;
            }
            else if (actionNumber > 20 && actionNumber <= 30)
            {

                if (EnemyHero.Skills[1].UseCounter == 0)
                {
                    AITurn();
                }
                else
                {
                    actionToReturn = "Parade";
                    EnemyHero.Skills[1].UseCounter--;
                    this.View.EnemyHeroSkillUsed.Text = actionToReturn;
                }
            }
            else
            {
                if (this.EnemyHero.Potion != null)
                {
                    actionToReturn = "Potion";
                    this.View.EnemyHeroSkillUsed.Text = actionToReturn;
                }
                else
                    AITurn();
            }

            return actionToReturn;
        }

        private void EndFight()
        {
            EnemyHero.Skills[1].UseCounter = 3;
            AllyHero.Skills[1].UseCounter = 3;
            MessageBoxResult msgBxResult;
            if (EnemyHero.Life <= 0)
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
            Battle newBattle = new Battle(attackingArmy, defendingArmy, attackedCastle,attackingCastle);
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
