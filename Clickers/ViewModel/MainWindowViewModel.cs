using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Clickers.ViewModel
{
    public class MainWindowViewModel
    {
        MySQLManager<Castle> myCastleManager = new MySQLManager<Castle>();
        MainWindow view;
        UsernameRegister popUp;

        public MainWindowViewModel(MainWindow view)
        {
            this.view = view;
            EventGenerator();
            Switcher.pageSwitcher = view;
        }

        public void Navigate(UserControl nextPage)
        {
            view.Content = nextPage;
        }
        #region PrivateMethods
        private void EventGenerator()
        {
            view.newGameButton.Click += NewGameButton_Click;
            view.loadGameButton.Click += LoadGameButton_Click;
        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainCastleView newPage = new MainCastleView();
            LoadCastle();
            Switcher.Switch(newPage);
        }

        private void NewGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MySQLFullDB test = new MySQLFullDB();
            if (test.Database.Exists())
            {
                if (MessageBox.Show("Voulez-vous écraser la partie précédente ?", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    MessageBox.Show("Cliquez sur \"Reprendre la partie\"");
                }
                else
                {
                    test.DeleteDatabase();
                    popUp = new UsernameRegister();
                    popUp.CancelButton.Click += CancelButton_Click;
                    popUp.OkButton.Click += OkButton_Click;
                    popUp.Visibility = Visibility.Visible;
                }
            }
            else
            {
                popUp = new UsernameRegister();
                popUp.CancelButton.Click += CancelButton_Click;
                popUp.OkButton.Click += OkButton_Click;
                popUp.Visibility = Visibility.Visible;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            myCastleManager.initDatabase();
            MainCastleView newPage = new MainCastleView();
            CreateCastle();
            Switcher.Switch(newPage);
            popUp.Close();
        }

        private async void CreateCastle()
        {
            GameViewModel.Instance.MainCastle.Name = popUp.UsernameTB.Text;
            await myCastleManager.Insert(GameViewModel.Instance.MainCastle);
        }

        private async void LoadCastle()
        {
            Task<Castle> castleToLoad = myCastleManager.Get(1);
            GameViewModel.Instance.MainCastle = castleToLoad.Result;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            popUp.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
