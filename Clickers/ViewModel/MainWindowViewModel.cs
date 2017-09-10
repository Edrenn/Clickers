using Clickers.DataBaseManager;
using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Models;
using Clickers.Views;
using Clickers.Views.OptionView;
using Clickers.ViewModel.GoldProducer;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="view">The view to manage.</param>
        public MainWindowViewModel(MainWindow view)
        {
            this.view = view;
            EventGenerator();
            Switcher.pageSwitcher = view;

            MySQLFullDB mySQLfullDB = new MySQLFullDB();
            // We check if there is a game to load
            if (!mySQLfullDB.Database.Exists())
            {
                this.view.loadGameButton.IsEnabled = false;
                this.view.loadGameButton.Opacity = 0.3;
            }

        }

        /// <summary>
        /// Navigates the specified next page.
        /// </summary>
        /// <param name="nextPage">Page to Navigate.</param>
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
            {
                Switcher.Switch(new LoadingPage());
                Window window = (Window)sp.Parent;
                window.Close();
                NormalGameGeneration(tb.Text);
            }
        }

        private async void NormalGameGeneration(string CastleName)
        {
            Task newDBTask = new Task(new Action(() =>
            {
                MySQLFullDB mySQLFullDB = new MySQLFullDB();
                mySQLFullDB.InitLocalMySQL(CastleName);
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    MainCastleView newPage = new MainCastleView();
                    Switcher.Switch(newPage);
                }));
            }));
            newDBTask.Start();
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
                window.Close();
                Switcher.Switch(new LoadingPage());
                CustomGameGeneration(CastleNameTbx.Text, listView.SelectedItem.ToString());
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
            Task newDBTask = new Task(new Action(() =>
            {
                //Load CustomCastle
                MySQLFullDB mySQLFullDB = new MySQLFullDB();
                mySQLFullDB.InitCustomLocalMySQL(customFolder + "//");
                Castle castleWithCustomValues = Json.JsonManager.Instance.GetCastleFromJSon(AllPath.Instance.JsonCustomFolder + customFolder + "//" + AllPath.Instance.CustomCastle);


                // Update MainCastle
                MySQLCastle CastleEntities = new MySQLCastle();
                Castle newCastle = CastleEntities.Get(1).Result;
                CastleEntities.GetProperties(newCastle);
                newCastle.Name = CastleName;
                newCastle.Life = castleWithCustomValues.Life;
                newCastle.Golds = castleWithCustomValues.Golds;
                CastleEntities.Update(newCastle);




                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    MainCastleView newPage = new MainCastleView();
                    Switcher.Switch(newPage);
                }));
            }));
            newDBTask.Start();
        }
        #endregion




        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            OptionUserControl optionPage = new OptionUserControl();
            optionPage.Show();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            MySQLFullDB mySQLfullDB = new MySQLFullDB();
            if (!mySQLfullDB.Database.Exists())
            {
                this.view.loadGameButton.Opacity = 0.3;
                this.view.loadGameButton.IsEnabled = false;
                MessageBox.Show("Il n'y a aucune partie à charger");
            }
            else
            {
                Switcher.Switch(new LoadingPage());

                Task loadTask = new Task(new Action(() =>
                {
                    LoadGame();
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MainCastleView newPage = new MainCastleView();
                        Switcher.Switch(newPage);
                    }));
                }));
                loadTask.Start();

            }

        }

        /// <summary>
        /// Loads all the info of the last save
        /// </summary>
        private async void LoadGame()
        {
            //Load the main castle
            DataBaseManager.EntitiesLink.MySQLCastle castleEntitiesManager = new DataBaseManager.EntitiesLink.MySQLCastle();
             GameViewModel.Instance.MainCastle = myCastleManager.Get(1).Result;
            castleEntitiesManager.GetProperties(GameViewModel.Instance.MainCastle);

            //Load the ennemy castle
            GameViewModel.Instance.EnnemyCastle = myCastleManager.Get(2).Result;
            castleEntitiesManager.GetArmy(GameViewModel.Instance.EnnemyCastle);

            //Load the army
            DataBaseManager.EntitiesLink.MySQLArmy armyEntities = new DataBaseManager.EntitiesLink.MySQLArmy();
            armyEntities.GetHero(GameViewModel.Instance.MainCastle.Army);
            GetAllSoldierFromSavedArmy();

            //Load heroes 
            DataBaseManager.EntitiesLink.MySQLHero heroesEntities = new DataBaseManager.EntitiesLink.MySQLHero();
            foreach (Hero hero in GameViewModel.Instance.MainCastle.Heroes)
            {
                heroesEntities.GetProperties(hero);
            }

            RestartProducers();
        }

        private void RestartProducers()
        {
            foreach (GoldProducerViewModel GoldProducerVM in GameViewModel.Instance.AllGoldProducerVM)
            {
                if (GoldProducerVM.RessourceProducer.IsActive)
                {
                    GoldProducerVM.StartProducing();
                }
            }

        }


        private void GetAllSoldierFromSavedArmy()
        {
            MySQLManager<Soldier> SoldierManager = new MySQLManager<Soldier>();
            Soldier knight = SoldierManager.Get(1).Result;
            Soldier horseman = SoldierManager.Get(2).Result;
            Soldier archer  = SoldierManager.Get(3).Result;
            // Load all Knight
            for (int i = 0; i < GameViewModel.Instance.MainCastle.Army.chevCounter; i++)
            {
                Soldier newSoldier = new Soldier();
                newSoldier.CopySoldier(knight);
                GameViewModel.Instance.MainCastle.Army.AllSoldiers.Add(newSoldier);
            }

            // Load all HorseMan
            for (int i = 0; i < GameViewModel.Instance.MainCastle.Army.cavCounter; i++)
            {
                Soldier newSoldier = new Soldier();
                newSoldier.CopySoldier(horseman);
                GameViewModel.Instance.MainCastle.Army.AllSoldiers.Add(newSoldier);
            }

            // Load all Archer
            for (int i = 0; i < GameViewModel.Instance.MainCastle.Army.cavCounter; i++)
            {
                Soldier newSoldier = new Soldier();
                newSoldier.CopySoldier(archer);
                GameViewModel.Instance.MainCastle.Army.AllSoldiers.Add(newSoldier);
            }
        }
        #endregion
    }
}
