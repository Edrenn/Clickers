using Clickers.Models;
using Clickers.Views.TaverneView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.ViewModel.SoldierProducer
{
    public class HeroViewModel
    {
        private HeroView view;
        public HeroView View
        {
            get { return view; }
            set { view = value; }
        }

        private Hero hero;
        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }


        public HeroViewModel(Hero hero)
        {
            View = new HeroView();
            this.Hero = hero;
            EventGenerator();
        }

        private void EventGenerator()
        {
            View.SelectHeroButton.Click += SelectHeroButton_Click;
        }

        private void SelectHeroButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GameViewModel.Instance.MainCastle.Army.Hero = this.Hero;
        }
    }
}
