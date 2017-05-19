using Clickers.Models;
using Clickers.Views;
using Clickers.Views.ArmyView;
using Clickers.Views.TaverneView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Clickers.ViewModel.Army
{
    public class ArmyViewModel
    {
        ArmyView view;
        int numberChevalier;
        public ArmyViewModel(ArmyView view)
        {
            this.view = view;
            NewSoldierViewCreation("Chevalier", "../../Assets/Image/chevalier.jpg");
            NewSoldierViewCreation("Archer", "../../Assets/Image/archer.jpg");
            NewSoldierViewCreation("Cavalier", "../../Assets/Image/cavalier.jpg");
            if (GameViewModel.Instance.MainCastle.Army.Hero != null) {
                NewHeroView();
            }
            EventGenerator();
        }

        private void EventGenerator()
        {
            view.ToCastleButton.Click += ToCastleButton_Click;
        }

        private void ToCastleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainCastleView castleView = new MainCastleView();
            Switcher.Switch(castleView);
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

        private void NewHeroView()
        {
            HeroView newHeroView = new HeroView();
            newHeroView.DataContext = GameViewModel.Instance.MainCastle.Army.Hero;
            newHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            view.Units.Children.Add(newHeroView);

        }
    }
}
