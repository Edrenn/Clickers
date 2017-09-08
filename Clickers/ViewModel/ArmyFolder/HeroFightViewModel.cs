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

        private Hero leftHero;
        public Hero LeftHero
        {
            get { return leftHero; }
            set { leftHero = value; }
        }

        private Hero rightHero;
        public Hero RightHero
        {
            get { return rightHero; }
            set { rightHero = value; }
        }

        private Clickers.Models.Army attackingArmy;

        private Clickers.Models.Army defendingArmy;

        private Castle attackingCastle;
        private Castle defendingCastle;

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

        private bool isFightOver = false;

        private bool isEnnemyAttack = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="HeroFightViewModel"/> class.
        /// </summary>
        /// <param name="attackingCastle">The attacking castle.</param>
        /// <param name="defendingCastle">The defending castle.</param>
        /// <param name="isEnnemyAttack">if set to <c>true</c> [it is an ennemy attack].</param>
        public HeroFightViewModel(Castle attackingCastle, Castle defendingCastle, bool isEnnemyAttack)
        {
            this.Rand = new Random();
            this.View = new HeroDuelFightView();
            this.Actions = new Dictionary<Hero, string>();

            this.defendingCastle = defendingCastle;
            this.attackingCastle = attackingCastle;
            this.attackingArmy = attackingCastle.Army;
            this.defendingArmy = defendingCastle.Army;
            this.isEnnemyAttack = isEnnemyAttack;

            // Visual change to be sure that our hero will be on the left side
            if (isEnnemyAttack)
            {
                this.LeftHero = defendingArmy.Hero;
                this.RightHero = attackingArmy.Hero;
            }
            else
            {
                this.LeftHero = attackingArmy.Hero;
                this.RightHero = defendingArmy.Hero;
            }

            GenerateUI(LeftHero, RightHero);

            View.TurnTB.DataContext = this;
            Switcher.Switch(this.View);
        }

        /// <summary>
        /// Generates the UI of the Duel for each hero.
        /// </summary>
        /// <param name="LeftHero">The left hero.</param>
        /// <param name="RightHero">The right hero.</param>
        private void GenerateUI(Hero LeftHero, Hero RightHero)
        {
            HeroView AllyHeroView = new HeroView();
            AllyHeroView.HeroInfoSP.Visibility = System.Windows.Visibility.Collapsed;
            AllyHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            AllyHeroView.DataContext = LeftHero;
            foreach (Skill skill in LeftHero.Skills)
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
            this.View.AllyHeroAttributesSP.DataContext = this.LeftHero;
            this.View.EnnemyHeroAttributesSP.DataContext = this.RightHero;

            HeroView EnnemyHeroView = new HeroView();
            EnnemyHeroView.HeroInfoSP.Visibility = System.Windows.Visibility.Collapsed;
            EnnemyHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            EnnemyHeroView.DataContext = RightHero;
            this.View.EnnemyHeroSP.Children.Add(EnnemyHeroView);
            this.View.ItemsButton.Click += ItemsButton_Click;
        }

        private void ItemsButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.LeftHero.Potion != null)
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
        }

        private void ParadeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (LeftHero.Skills[1].UseCounter == 0)
            {
                System.Windows.MessageBox.Show("Vous avez utilisé toutes vos parades");
            }
            else
            {

                DoAllActions("Parade", AITurn());
                LeftHero.Skills[1].UseCounter--;
                this.View.AllyHeroSkillUsed.Text = "Parade";
            }
        }

        private void AttaqueButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DoAllActions("Attaque", AITurn());
            this.View.AllyHeroSkillUsed.Text = "Attaque";
        }

        /// <summary>
        /// this method will play all action of a round
        /// it gets each heroes's action :
        /// 1°) Parade 2°) Feinte 3°) Attaque
        /// each parade properties will be set to false at the end
        /// </summary>
        private void DoAllActions(String allyHeroAction, String enemyHeroAction)
        {
            if (allyHeroAction == "Objet")
            {
                if (this.LeftHero.Potion != null)
                {
                    this.Heal(this.LeftHero);
                }
            }
            if (enemyHeroAction == "Objet")
            {
                this.Heal(this.RightHero);
            }
            if (allyHeroAction == "Parade")
            {
                this.LeftHero.IsParing = true;
            }
            if (enemyHeroAction == "Parade")
            {
                this.RightHero.IsParing = true;
            }
            if (allyHeroAction == "Feinte")
            {
                this.Feinte(this.LeftHero, this.RightHero);
            }
            if (enemyHeroAction == "Feinte")
            {
                this.Feinte(this.RightHero, this.LeftHero);
            }
            if (allyHeroAction == "Attaque")
            {
                this.Attack(this.LeftHero, this.RightHero);
                if (this.LeftHero.Life <= 0 || this.RightHero.Life <= 0)
                {
                    if (!isFightOver)
                    {
                        isFightOver = true;
                        EndFight();
                    }
                }
            }
            if (enemyHeroAction == "Attaque")
            {
                this.Attack(this.RightHero, this.LeftHero);

                if (this.LeftHero.Life <= 0 || this.RightHero.Life <= 0)
                {
                    if (!isFightOver)
                    {
                        isFightOver = true;
                        EndFight();
                    }
                }
            }
            this.LeftHero.IsParing = false;
            this.RightHero.IsParing = false;
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
                //if (Actions.ContainsKey(EnemyHero))
                //{
                //    Actions[EnemyHero] = "Attaque";
                //}
                //else
                //    Actions.Add(EnemyHero, "Attaque");
            }
            else if (actionNumber > 10 && actionNumber <= 20)
            {
                actionToReturn = "Feinte";
                this.View.EnemyHeroSkillUsed.Text = actionToReturn;
                //if (Actions.ContainsKey(EnemyHero))
                //{
                //    Actions[EnemyHero] = "Feinte";
                //}
                //else
                //    Actions.Add(EnemyHero, "Feinte");
            }
            else if (actionNumber > 20 && actionNumber <= 30)
            {

                if (RightHero.Skills[1].UseCounter == 0)
                {
                    AITurn();
                }
                else
                {
                    actionToReturn = "Parade";
                    RightHero.Skills[1].UseCounter--;
                    this.View.EnemyHeroSkillUsed.Text = actionToReturn;
                }
                //if (EnemyHero.Skills[2].UseCounter == 0)
                //{
                //    AITurn();
                //}
                //else
                //{
                //    if (Actions.ContainsKey(EnemyHero))
                //    {
                //        Actions[EnemyHero] = "Parade";
                //    }
                //    else
                //        Actions.Add(EnemyHero, "Parade");
                //}
            }
            else
            {
                if (this.RightHero.Potion != null)
                {
                    actionToReturn = "Potion";
                    this.View.EnemyHeroSkillUsed.Text = actionToReturn;
                }
                else
                    AITurn();
                //if (Actions.ContainsKey(EnemyHero))
                //{
                //    Actions[EnemyHero] = "Object";
                //}
                //else
                //    Actions.Add(EnemyHero, "Object");
            }

            return actionToReturn;
        }

        private void EndFight()
        {
            MessageBoxResult msgBxResult;

            if (RightHero.Life <= 0)
            {
                msgBxResult = System.Windows.MessageBox.Show("You win");
                if (!this.isEnnemyAttack)
                {
                    this.attackingCastle.Army.BoostArmy();
                }
                else
                {
                    this.defendingCastle.Army.BoostArmy();
                }
            }
            else
            {
                msgBxResult = System.Windows.MessageBox.Show("You lose");
                if (!this.isEnnemyAttack)
                {
                    this.defendingCastle.Army.BoostArmy();
                }
                else
                {
                    this.attackingCastle.Army.BoostArmy();
                }
            }



            if (msgBxResult == MessageBoxResult.OK)
            {
                launchBattle();
            }

        }

        private void launchBattle()
        {
            GameViewModel.Instance.EnnemyCastle.Army.GenerateEnnemyArmy();
            Battle newBattle = new Battle(attackingArmy, defendingArmy, defendingCastle, attackingCastle,this.isEnnemyAttack);
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
