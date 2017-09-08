using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Models.Buildings;
using Clickers.Views;
using Clickers.ViewModel.Army;
using Clickers.Views.ArmyView;

namespace Clickers.ViewModel
{
    public class MainCastleViewModel
    {
        private MainCastleView view;

        public MainCastleViewModel(MainCastleView view)
        {
            this.view = view;
            this.view.InfoBarUC.Content = InfoBarViewModel._Instance.View;
            EventGenerator();
        }

        private void EventGenerator()
        {
            view.BlackSmithButton.Click += BlackSmithButton_Click;
            view.ThiefButton.Click += ThiefButton_Click;
            view.GoldFieldButton.Click += GoldFieldButton_Click;
            view.CaserneButton.Click += CaserneButton_Click;
            view.ArmyButton.Click += ArmyButton_Click;
            view.ToBattleButton.Click += ToBattleButton_Click;
            view.TaverneButton.Click += TaverneButton_Click;
            view.HealerHouseButton.Click += HealerHouse_Click;
            view.SaveButton.Click += SaveButton_Click;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            GameViewModel.Instance.Save();
        }

        private void BlackSmithButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new BlackSmithView());
        }

        private void ThiefButton_Click(object sender, RoutedEventArgs e)
        {
            GameViewModel.Instance.GoldCounter += 100;
        }

        private void TaverneButton_Click(object sender, RoutedEventArgs e)
        {
            TaverneViewModel Taverne = TaverneViewModel.Instance;
            Switcher.Switch(Taverne.View);
        }

        private void ToBattleButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameViewModel.Instance.MainCastle.Army.Hero != null)
            {
                if (GameViewModel.Instance.MainCastle.Army.Hero.Life > 0)
                {

                    if (GameViewModel.Instance.MainCastle.Army.AllSoldiers.Count >= 5)
                    {
                        InfoBarViewModel._Instance.TokenSource.Cancel();
                        GameViewModel.Instance.EnnemyCastle.Army.GenerateEnnemyArmy();
                        Random rng = new Random();
                        rng.Next(0, 100);
                        if (rng.Next(0, 100) > 30)
                        {
                            GameViewModel.Instance.EnnemyCastle.Army.GenerateHero();
                        }
                        if (GameViewModel.Instance.MainCastle.Army.Hero != null && GameViewModel.Instance.EnnemyCastle.Army.Hero != null)
                        {
                            if (!(GameViewModel.Instance.MainCastle.Army.Hero.Life <= 0) || !(GameViewModel.Instance.EnnemyCastle.Army.Hero.Life <= 0))
                            {
                                HeroFightViewModel newDuel = new HeroFightViewModel(GameViewModel.Instance.MainCastle, GameViewModel.Instance.EnnemyCastle, false);
                            }
                        }
                        else
                        {
                            Battle newBattle = new Battle(GameViewModel.Instance.EnnemyCastle, GameViewModel.Instance.MainCastle, false);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Il vous faut au minimum 5 soldats");
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("Attention votre héros n'a plus de points de vie. Soignez-le ou choisissez en un autre.");
                }
            }
            else
            {
                InfoBarViewModel._Instance.TokenSource.Cancel();
                GameViewModel.Instance.EnnemyCastle.Army.GenerateEnnemyArmy();
                Random rng = new Random();
                rng.Next(0, 100);
                if (rng.Next(0, 100) > 30)
                {
                    GameViewModel.Instance.EnnemyCastle.Army.GenerateHero();
                }
                if (GameViewModel.Instance.EnnemyCastle.Army.Hero != null)
                {
                    GameViewModel.Instance.EnnemyCastle.Army.BoostArmy();
                    Battle newBattle = new Battle( GameViewModel.Instance.EnnemyCastle, GameViewModel.Instance.MainCastle, false);
                }
                else
                {
                    Battle newBattle = new Battle(GameViewModel.Instance.EnnemyCastle, GameViewModel.Instance.MainCastle, false);
                }
            }
        }

        private void ArmyButton_Click(object sender, RoutedEventArgs e)
        {
            ArmyView newArmyView = new ArmyView();
            Switcher.Switch(newArmyView);
        }

        private void CaserneButton_Click(object sender, RoutedEventArgs e)
        {
            SoldiersBuildingsView newSoldiersBuildingView = new SoldiersBuildingsView();
            Switcher.Switch(newSoldiersBuildingView);
        }

        private void GoldFieldButton_Click(object sender, RoutedEventArgs e)
        {
            GoldFieldView newGoldFieldView = new GoldFieldView();
            Switcher.Switch(newGoldFieldView);
        }

        private void HealerHouse_Click(object sender, RoutedEventArgs e)
        {
            HealerHouseView newHealerHouseView = new HealerHouseView(GameViewModel.Instance.MainCastle.Healer);
            Switcher.Switch(newHealerHouseView);
        }
    }
}
