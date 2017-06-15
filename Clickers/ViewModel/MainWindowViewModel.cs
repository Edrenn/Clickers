using Clickers.DataBaseManager;
using Clickers.Models;
using Clickers.Views;
using Clickers.Views.OptionView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Clickers.ViewModel
{
    public class MainWindowViewModel
    {
        MySQLManager<Castle> myCastleManager = new MySQLManager<Castle>();
        MainWindow view;

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
            this.view.OptionButton.Click += OptionButton_Click;
            view.newGameButton.Click += NewGameButton_Click;
            view.loadGameButton.Click += LoadGameButton_Click;
            view.QuitButton.Click += QuitButton_Click;
        }

        private void NewGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string button1 = "Normale";
            string button2 = "Custom";
            string caption = "Choix de partie";
            string message = "Quel type de partie voulez vous jouez ?";
            MessageBoxResult result = WPFCustomMessageBox.CustomMessageBox.ShowYesNo(message, caption, button1, button2);
            if (result == MessageBoxResult.No)
            {
                CheckDatabaseCustom();
            }
            else
            {
                CheckDatabase();
            }
        }

        #region Normal
        private void CheckDatabase()
        {
            MySQLFullDB mySQLfullDB = new MySQLFullDB();
            if (mySQLfullDB.Database.Exists())
            {
                if (MessageBox.Show("Voulez-vous écraser la partie précédente ?", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    MessageBox.Show("Cliquez sur \"Reprendre la partie\"");
                }
                else
                {
                    mySQLfullDB.DeleteDatabase();
                    popUpNewGame();
                }
            }
            else
            {
                popUpNewGame();
            }
        }

        private void popUpNewGame()
        {
            Window newPopUp = new Window() { Width = 500, Height = 450 };
            StackPanel newVerticalSP = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            Label newGameNameLabel = new Label() { Content = "Nommez votre château !" };
            TextBox GameNameTbx = new TextBox() { Name = "GameNameTbx", Width = 150 };
            Button validateButton = new Button() { Width = 100, Height = 60, Content = "Valider" };
            validateButton.Click += ValidateButton_Click;
            newVerticalSP.Children.Add(newGameNameLabel);
            newVerticalSP.Children.Add(GameNameTbx);
            newVerticalSP.Children.Add(validateButton);
            newPopUp.Content = newVerticalSP;
            newPopUp.Show();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StackPanel sp = (StackPanel)button.Parent;
            TextBox tb = (TextBox)sp.Children[1];
            if (String.IsNullOrEmpty(tb.Text))
            {
                System.Windows.MessageBox.Show("Veuillez donner un nom à votre château");
            }
            else
                NormalGameGeneration(tb.Text);
            Window window = (Window)sp.Parent;
            window.Close();
        }

        private async void NormalGameGeneration(string CastleName)
        {
            MySQLFullDB mySQLFullDB = new MySQLFullDB();
            mySQLFullDB.InitLocalMySQL();
            Castle newCastle = new Castle() { Name = CastleName, Life = 100 };
            await myCastleManager.Insert(newCastle);
            Castle enemyCastle = new Castle() { Name = "Méchant chato", Life = 100 };
            await myCastleManager.Insert(enemyCastle);
            MainCastleView newPage = new MainCastleView();
            Switcher.Switch(newPage);
        }
        #endregion

        #region Custom
        private void CheckDatabaseCustom()
        {
            MySQLFullDB mySQLfullDB = new MySQLFullDB();
            if (mySQLfullDB.Database.Exists())
            {
                if (MessageBox.Show("Voulez-vous écraser la partie précédente ?", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    MessageBox.Show("Cliquez sur \"Reprendre la partie\"");
                }
                else
                {
                    mySQLfullDB.DeleteDatabase();
                    List<String> allFoldersName = new List<string>();
                    foreach (string itemName in Directory.EnumerateDirectories(AllPath.Instance.JsonCustomFolder))
                    {
                        allFoldersName.Add(itemName.Replace(AllPath.Instance.JsonCustomFolder, ""));
                    }
                    popUpCustomNewGame(allFoldersName);
                }
            }
            else
            {
                List<String> allFoldersName = new List<string>();
                foreach (string itemName in Directory.EnumerateDirectories(AllPath.Instance.JsonCustomFolder))
                {
                    allFoldersName.Add(itemName.Replace(AllPath.Instance.JsonCustomFolder, ""));
                }
                popUpCustomNewGame(allFoldersName);
            }
        }

        private void popUpCustomNewGame(List<String> allFolder)
        {
            Window newPopUp = new Window() { Width = 500, Height = 450 };
            StackPanel newVerticalSP = new StackPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            Label newGameNameLabel = new Label() { Content = "Nommez votre château !" };
            TextBox GameNameTbx = new TextBox() { Name = "GameNameTbx", Width = 150 };
            Label newLabel = new Label() { Content = "Choisissez votre customisation !" };
            ListView newListView = new ListView() { ItemsSource = allFolder };
            newListView.SelectionChanged += NewListView_SelectionChanged;
            newVerticalSP.Children.Add(newGameNameLabel);
            newVerticalSP.Children.Add(GameNameTbx);
            newVerticalSP.Children.Add(newLabel);
            newVerticalSP.Children.Add(newListView);
            newPopUp.Content = newVerticalSP;
            newPopUp.Show();
        }

        private void NewListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            StackPanel sp = (StackPanel)listView.Parent;
            Window window = (Window)sp.Parent;
            TextBox CastleNameTbx = (TextBox)sp.Children[1];
            if (!String.IsNullOrEmpty(CastleNameTbx.Text))
            {
                CustomGameGeneration(CastleNameTbx.Text, listView.SelectedItem.ToString());
                window.Close();
            }
            else
            {
                if (listView.SelectedIndex != -1)
                {
                    System.Windows.MessageBox.Show("Veuillez donner un nom à votre château");
                    listView.SelectedIndex = -1;
                }
            }
        }

        private async void CustomGameGeneration(string CastleName,string customFolder)
        {
            MySQLFullDB mySQLFullDB = new MySQLFullDB();
            mySQLFullDB.InitCustomLocalMySQL(customFolder + "//");
            Castle newCastle = Json.JsonManager.Instance.GetCastleFromJSon(AllPath.Instance.JsonCustomFolder + customFolder + "//" + AllPath.Instance.CustomCastle);
            newCastle.Name = CastleName;
            await myCastleManager.Insert(newCastle);
            Castle enemyCastle = new Castle() { Name = "Méchant chato", Life = 100 };
            await myCastleManager.Insert(enemyCastle);
            MainCastleView newPage = new MainCastleView();
            Switcher.Switch(newPage);
        }
        #endregion




        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            OptionUserControl optionPage = new OptionUserControl();
            optionPage.Show();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.view.Close();
        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            MainCastleView newPage = new MainCastleView();
            LoadCastle();
            Switcher.Switch(newPage);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            myCastleManager.initDatabase();
            MainCastleView newPage = new MainCastleView();
            Switcher.Switch(newPage);
        }

        private async void LoadCastle()
        {
            Task<Castle> castleToLoad = myCastleManager.Get(1);
            GameViewModel.Instance.MainCastle = castleToLoad.Result;
        }
        #endregion
    }
}
