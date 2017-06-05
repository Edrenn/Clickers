
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
            Heros = new Dictionary<string, Hero>();
            this.View = new TaverneView();

            foreach (Hero hero in GameViewModel.Instance.MainCastle.Heroes)
            {
                Heros.Add(hero.Name, hero);
                NewHeroView(hero);
            }

        }

        private void NewHeroView(Hero hero)
        {
            HeroViewModel heroViewModel = new HeroViewModel(hero);
            heroViewModel.InitSelectView();
            heroViewModel.View.DataContext = hero;
            View.AllHeroesSP.Children.Add(heroViewModel.View);
        }
    }
}
