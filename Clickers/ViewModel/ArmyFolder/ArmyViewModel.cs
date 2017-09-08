using Clickers.Models;
using Clickers.Views;
using Clickers.Views.ElementViews;
using Clickers.Views.ArmyView;
using Clickers.Views.TaverneView;
using Clickers.ViewModel.SoldierProducer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Clickers.ViewModel.Army
{
    public class ArmyViewModel
    {
        ArmyView view;
        int numberChevalier;
        public ArmyViewModel(ArmyView view)
        {
            this.view = view;
            this.view.InfoBarUC.Content = InfoBarViewModel._Instance.View;
            NewSoldierViewCreation("Chevalier", "../../Assets/Image/chevalier.jpg");
            NewSoldierViewCreation("Archer", "../../Assets/Image/archer.jpg");
            NewSoldierViewCreation("Cavalier", "../../Assets/Image/cavalier.jpg");
            if (GameViewModel.Instance.MainCastle.Army.Hero != null) {
                NewHeroView(GameViewModel.Instance.MainCastle.Army.Hero);
            }
        }

        private void NewSoldierViewCreation(string SoldierName, string ImagePath)
        {
            numberChevalier = 0;
            UnitView newSoldier = new UnitView();
            newSoldier.SoldierName.Text = SoldierName;
            newSoldier.UnitImage.Source = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            foreach (Soldier soldier in GameViewModel.Instance.MainCastle.Army.AllSoldiers)
            {
                if (soldier.Name == SoldierName)
                {
                    numberChevalier++;
                }
            }
            newSoldier.NumberInArmy.Text = numberChevalier.ToString();
            view.Units.Children.Add(newSoldier);
        }

        private void NewHeroView(Hero hero)
        {
            HeroViewModel newHeroViewModel = new HeroViewModel(hero);
            newHeroViewModel.InitEquipView();

            view.Units.Children.Add(newHeroViewModel.View);

        }


    }
}
