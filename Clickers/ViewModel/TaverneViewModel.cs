
using Clickers.DataBaseManager;
using Clickers.DataBaseManager.EntitiesLink;
using Clickers.Models;
using Clickers.ViewModel.SoldierProducer;
using Clickers.Views;
using Clickers.Views.TaverneView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.ViewModel
{
    public class TaverneViewModel
    {
        private TaverneView view;
        public TaverneView View
        {
            get { return view; }
            set { view = value; }
        }

        private Dictionary<string, Hero> heros;
        public Dictionary<string, Hero> Heros
        {
            get { return heros; }
            set { heros = value; }
        }


        private static TaverneViewModel instance;
        public static TaverneViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TaverneViewModel();
                }
                return instance;
            }
        }


        public TaverneViewModel()
        {
            //MySQLManager<Hero> mySQLHeroManager = new MySQLManager<Hero>();
            Heros = new Dictionary<string, Hero>();
            this.View = new TaverneView();
            //int heroNumber = 1;
            //bool isOk = true;
            //List<Hero> herosList = new List<Hero>();
            //while (isOk)
            //{
            //    Task<Hero> allHeros = mySQLHeroManager.Get(heroNumber);
            //    if (allHeros.Result != null)
            //    {
            //        herosList.Add(allHeros.Result);
            //        Hero test = allHeros.Result;
            //        MySQLHero testReference = new MySQLHero();
            //        test = testReference.GetSkills(test);
            //        heroNumber += 1;
            //    }
            //    else
            //        isOk = false;
            //}

            foreach (Hero hero in GameViewModel.Instance.MainCastle.Heroes)
            {
                Heros.Add(hero.Name, hero);
                NewHeroView(hero);
            }

        }

        private void NewHeroView(Hero hero)
        {
            HeroViewModel heroViewModel = new HeroViewModel(hero);
            heroViewModel.View.DataContext = hero;
            View.AllHeroesSP.Children.Add(heroViewModel.View);
        }
    }
}
