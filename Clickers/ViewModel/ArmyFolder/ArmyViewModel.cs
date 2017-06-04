using Clickers.Models;
using Clickers.Views;
using Clickers.Views.ElementViews;
using Clickers.Views.ArmyView;
using Clickers.Views.TaverneView;

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
            NewSoldierViewCreation("Chevalier", "../../Assets/Image/chevalier.jpg");
            NewSoldierViewCreation("Archer", "../../Assets/Image/archer.jpg");
            NewSoldierViewCreation("Cavalier", "../../Assets/Image/cavalier.jpg");
            if (GameViewModel.Instance.MainCastle.Army.Hero != null) {
                NewHeroView();
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

        private void NewHeroView()
        {
            HeroView newHeroView = new HeroView();
            newHeroView.DataContext = GameViewModel.Instance.MainCastle.Army.Hero;
            newHeroView.SelectHeroButton.Visibility = System.Windows.Visibility.Collapsed;
            Button equipButton = new Button();
            equipButton.Content = "Equiper";
            equipButton.Height = 40;
            newHeroView.ButtonSP.Children.Add(equipButton);
            equipButton.Click += EquipButton_Click;

            InventoryUC newIventoryUC = new InventoryUC();
            newIventoryUC.DataContext = GameViewModel.Instance.MainCastle.Army.Hero;
            newHeroView.HeroInfoSP.Children.Add(newIventoryUC);
            
            view.Units.Children.Add(newHeroView);

        }

        private void EquipButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.Window popUp = new System.Windows.Window();
            EquipmentListing EquipmentListing = new EquipmentListing();
            EquipmentListing.Controller.initEquipView();
            popUp.Content = EquipmentListing;
            popUp.Show();
            //if (GameViewModel.Instance.MainCastle.WeaponStock.Count > 0)
            //{
            //    GameViewModel.Instance.MainCastle.Army.Hero.Weapon = GameViewModel.Instance.MainCastle.WeaponStock[0];
            //}
            //if (GameViewModel.Instance.MainCastle.ShieldStock.Count > 0)
            //{
            //    GameViewModel.Instance.MainCastle.Army.Hero.Shield = GameViewModel.Instance.MainCastle.ShieldStock[0];
            //}
            //if (GameViewModel.Instance.MainCastle.PotionStock.Count > 0)
            //{
            //    GameViewModel.Instance.MainCastle.Army.Hero.Potion = GameViewModel.Instance.MainCastle.PotionStock[0];
            //}
        }
    }
}
