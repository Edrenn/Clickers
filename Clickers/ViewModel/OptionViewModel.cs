using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using Clickers.Models;
using Clickers.Models.Buildings;
using Clickers.DataBaseManager;

using Clickers.Views.OptionView;

namespace Clickers.ViewModel
{
    public class OptionViewModel
    {
        private OptionUserControl view;
        public OptionUserControl View
        {
            get { return view; }
            set { view = value; }
        }

        private List<Hero> baseHero;
        public List<Hero> BaseHero
        {
            get { return baseHero; }
            set { baseHero = value; }
        }

        private List<SoldiersProducer> baseSoldiersProducer;
        public List<SoldiersProducer> BaseSoldiersProducer
        {
            get { return baseSoldiersProducer; }
            set { baseSoldiersProducer = value; }
        }

        private String customFolderName;
        public String CustomFolderName
        {
            get { return customFolderName; }
            set { customFolderName = value; }
        }

        private TextBox newTB;

        public OptionViewModel(OptionUserControl view)
        {
            this.BaseSoldiersProducer = new List<SoldiersProducer>();
            this.BaseHero = new List<Hero>();
            this.View = view;
            EventGenerator();
            GenerateUI();
        }

        private void GenerateUI()
        {
            this.View.CastleLifeTBX.Text = "100";
            this.View.CastleBaseGoldTBX.Text = "0";
            this.BaseHero = Json.JsonManager.Instance.GetAllHerosFromJSon(AllPath.Instance.JsonFolder + AllPath.Instance.BaseHero);
            this.View.ChevalierHeroUC.DataContext = BaseHero[0];
            this.View.ArcherHeroUC.DataContext = BaseHero[1];
            this.View.CavalierHeroUC.DataContext = BaseHero[2];

            List<SoldiersProducer> allSoldiersProducer = Json.JsonManager.Instance.GetAllSoldierProducersFromJSon(AllPath.Instance.JsonFolder + AllPath.Instance.BaseSoldierProducer);
            foreach (SoldiersProducer soldiersProducer in allSoldiersProducer)
            {
                BaseSoldiersProducer.Add(soldiersProducer);
            }
            this.View.ChevalierUC.DataContext = allSoldiersProducer[0].SoldierType;
            this.View.ArcherUC.DataContext = allSoldiersProducer[1].SoldierType;
            this.View.CavalierUC.DataContext = allSoldiersProducer[2].SoldierType;
        }

        #region Events
        private void EventGenerator()
        {
            this.View.SaveButton.Click += SaveButton_Click;
            this.View.HeroButton.Click += HeroButton_Click;
            this.View.SoldierButton.Click += SoldierButton_Click;
            this.View.CastleButton.Click += CastleButton_Click;
            this.View.BackButton.Click += BackButton_Click;
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            popUp();
        }

        private void HeroButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.CastleButton.Opacity = 0.3;
            this.View.HeroButton.Opacity = 1;
            this.View.SoldierButton.Opacity = 0.3;

            this.View.CastleOptionsGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.HeroOptionsGrid.Visibility = System.Windows.Visibility.Visible;
            this.View.SoldierOptionsGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void SoldierButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.CastleButton.Opacity = 0.3;
            this.View.HeroButton.Opacity = 0.3;
            this.View.SoldierButton.Opacity = 1;

            this.View.CastleOptionsGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.HeroOptionsGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.SoldierOptionsGrid.Visibility = System.Windows.Visibility.Visible;
        }

        private void CastleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.CastleButton.Opacity = 1;
            this.View.HeroButton.Opacity = 0.3;
            this.View.SoldierButton.Opacity = 0.3;

            this.View.CastleOptionsGrid.Visibility = System.Windows.Visibility.Visible;
            this.View.HeroOptionsGrid.Visibility = System.Windows.Visibility.Collapsed;
            this.View.SoldierOptionsGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.View.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        private void GenerateCustomJson()
        {
            Directory.CreateDirectory(DataBaseManager.AllPath.Instance.JsonCustomFolder + this.CustomFolderName);
            CreateHerosJson();
            CreateSoldiersJson();
            CreateCastleJson();
            System.Windows.MessageBox.Show("Sauvegardé !");
        }

        private void popUp()
        {
            Window newPopUp = new Window() { Width = 300, Height = 250 };
            StackPanel newVerticalSP = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            Label newLabel = new Label() { Content = "Donnez un nom à votre customisation !" };
            this.newTB = new TextBox() { Name = "OptionNameTBx",Width = 150};
            Button ValidationButton = new Button() { Content = "Valider",Width = 50,Height = 50};
            ValidationButton.Click += ValidationButton_Click;
            newVerticalSP.Children.Add(newLabel);
            newVerticalSP.Children.Add(newTB);
            newVerticalSP.Children.Add(ValidationButton);
            newPopUp.Content = newVerticalSP;
            newPopUp.Show();
        }

        private void ValidationButton_Click(object sender, RoutedEventArgs e)
        {
            this.CustomFolderName = this.newTB.Text;
            GenerateCustomJson();
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Parent;
            Window window = (Window)sp.Parent;
            window.Close();
        }

        private void CreateSoldiersJson()
        {
            BaseSoldiersProducer[0].SoldierType = CreateCustomSoldier(this.View.ChevalierUC,0);
            BaseSoldiersProducer[1].SoldierType = CreateCustomSoldier(this.View.ArcherUC,1);
            BaseSoldiersProducer[2].SoldierType = CreateCustomSoldier(this.View.CavalierUC,2);
            int i = 1;

            AddToCustomFolder("[", AllPath.Instance.CustomSoldierProducer);
            foreach (SoldiersProducer soldierProducer in baseSoldiersProducer)
            {
                String serializedSoldier = Json.JsonManager.Instance.SerializeSoldierProducerJson(soldierProducer);
                if (i != baseSoldiersProducer.Count())
                {
                    AddToCustomFolder(serializedSoldier + ",", AllPath.Instance.CustomSoldierProducer);
                    i++;
                }
                else
                {
                    AddToCustomFolder(serializedSoldier, AllPath.Instance.CustomSoldierProducer);
                }
            }
            AddToCustomFolder("]", AllPath.Instance.CustomSoldierProducer);
        }

        private void CreateHerosJson()
        {
            List<Hero> allHeroes = new List<Hero>();
            allHeroes.Add(CreateCustomHero(this.View.ChevalierHeroUC,0));
            allHeroes.Add(CreateCustomHero(this.View.ArcherHeroUC,1));
            allHeroes.Add(CreateCustomHero(this.View.CavalierHeroUC,2));

            AddToCustomFolder("[" , AllPath.Instance.CustomHero);
            int i = 1;
            foreach (Hero hero in allHeroes)
            {
                String serializedHero = Json.JsonManager.Instance.SerializeHeroJson(hero);
                if (i != allHeroes.Count())
                {
                    AddToCustomFolder(serializedHero + ",", AllPath.Instance.CustomHero);
                    i++;
                }
                else
                {
                    AddToCustomFolder(serializedHero, AllPath.Instance.CustomHero);
                }
            }

            AddToCustomFolder("]", AllPath.Instance.CustomHero);
        }

        private void CreateCastleJson()
        {
            Castle castle = new Castle() { Life = Int32.Parse(this.View.CastleLifeTBX.Text), Golds = Int32.Parse(this.View.CastleBaseGoldTBX.Text) };
            string newName = "CustomCastle.json";
            AddToCustomFolder(Json.JsonManager.Instance.SerializeCastleJson(castle), newName);
        }

        private void AddToCustomFolder(String stringToAdd,String FileName)
        {
            File.AppendAllText(DataBaseManager.AllPath.Instance.JsonCustomFolder +this.CustomFolderName + "//" + FileName, stringToAdd);
        }

        private Hero CreateCustomHero(HeroOptionUC heroOptionUC, int HeroNumber)
        {
            Hero customHero = new Hero()
            {
                Name = heroOptionUC.HeroNameTBx.Text,
                Life = Int32.Parse(heroOptionUC.HeroLifeTBx.Text),
                Armor = Int32.Parse(heroOptionUC.HeroArmorTBx.Text),
                Attack = Int32.Parse(heroOptionUC.HeroAttackTBx.Text),
                BaseLife = Int32.Parse(heroOptionUC.HeroLifeTBx.Text),
                BaseArmor = Int32.Parse(heroOptionUC.HeroArmorTBx.Text),
                BaseAttack = Int32.Parse(heroOptionUC.HeroAttackTBx.Text),
                Skills = BaseHero[HeroNumber].Skills,
                ImagePath = BaseHero[HeroNumber].ImagePath,
                Type = BaseHero[HeroNumber].Type

            };
            return customHero;
        }

        private Soldier CreateCustomSoldier(SoldierOptionUC soldierOptionUC, int soldierNumber)
        {
            Soldier customSoldier = new Soldier()
            {
                Health = Int32.Parse(soldierOptionUC.SoldierLifeTBx.Text),
                AttackValue = Int32.Parse(soldierOptionUC.SoldierAttackTBx.Text),
                Price = Int32.Parse(soldierOptionUC.SoldierPriceTBx.Text),
                Name = this.baseSoldiersProducer[soldierNumber].SoldierType.Name,
                ImagePath = this.baseSoldiersProducer[soldierNumber].SoldierType.ImagePath,
                Type = this.baseSoldiersProducer[soldierNumber].SoldierType.Type,
                StrongAgainst = this.baseSoldiersProducer[soldierNumber].SoldierType.StrongAgainst

            };
            return customSoldier;
        }
    }
}
