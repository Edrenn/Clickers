using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Clickers.Views;
using Clickers.Views.TaverneView;
using Clickers.Models;

namespace Clickers.ViewModel
{
    public class HealerHouseViewModel
    {
        private HealerHouseView view;
        public HealerHouseView View
        {
            get { return view; }
            set { view = value; }
        }

        Window popUp;

        public HealerHouseViewModel(HealerHouseView view)
        {
            this.View = view;
            this.View.DataContext = this;
            EventGenerator();
        }

        public HealerHouseViewModel()
        {
            this.View = new HealerHouseView();
            EventGenerator();
        }

        private void EventGenerator()
        {
            this.View.ToCastleButton.Click += ToCastleButton_Click;
            this.View.HeroHealButton.Click += HeroHealButton_Click;
        }

        private void HeroHealButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (popUp != null)
            {
                popUp.Visibility = Visibility.Visible;
            }
            else
                popUpHeroChoiceGenerator();
        }

        private void ToCastleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainCastleView());
        }

        private void popUpHeroChoiceGenerator()
        {
            this.popUp = new Window();
            this.popUp.MaxHeight = 400;
            this.popUp.MaxWidth = 900;
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Orientation = Orientation.Horizontal;

            foreach (Hero hero in GameViewModel.Instance.MainCastle.Heroes)
            {
                Button bigButton = new Button();
                bigButton.Background = System.Windows.Media.Brushes.Transparent;
                bigButton.BorderBrush = System.Windows.Media.Brushes.Transparent;
                bigButton.Click += BigButton_Click;
                bigButton.Width = 300;
                bigButton.Height = 350;

                HeroView newHeroView = new HeroView(hero);
                newHeroView.Width = 300;
                newHeroView.Height = 350;
                newHeroView.SelectHeroButton.Visibility = Visibility.Collapsed;

                bigButton.Content = newHeroView;
                newStackPanel.Children.Add(bigButton);
            }

            scrollViewer.Content = newStackPanel;
            this.popUp.Content = scrollViewer;
            this.popUp.Show();
        }

        private void BigButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            HeroView heroView = (HeroView)button.Content;
            heroView.HeroContext.Life = heroView.HeroContext.MaxLife;
        }
    }
}
