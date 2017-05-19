using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Skills;
using Clickers.Views.HeroFightViews;

namespace Clickers.ViewModel.Army
{
    public class SkillViewModel : INotifyPropertyChanged
    {
        private Skill skill;
        public Skill Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        private int useCounter;
        public int UseCounter
        {
            get { return useCounter; }
            set
            {
                useCounter = value;
                RaisePropertyChanged("UserCounter");
            }
        }

        private SkillView view;
        public SkillView View
        {
            get { return view; }
            set { view = value; }
        }

        public SkillViewModel() { }

        public SkillViewModel(Skill skill)
        {
            this.Skill = skill;
            this.UseCounter = skill.UseCounter;
            GenerateView();
        }

        private void GenerateView()
        {
            this.View = new SkillView();
            this.View.DataContext = this;
            View.SkillLaunchButton.Content = Skill.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
