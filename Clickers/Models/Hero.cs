﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clickers.Models.Items;
using Clickers.Models.Base;
using Clickers.Models.Skills;

namespace Clickers.Models
{
    public class Hero : BaseDBEntity, INotifyPropertyChanged
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        int life;
        public int Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
                RaisePropertyChanged("Life");
            }
        }

        #region Base
        private int baseLife;
        public int BaseLife
        {
            get { return baseLife; }
            set { baseLife = value; }
        }

        private int baseArmor;
        public int BaseArmor
        {
            get { return baseArmor; }
            set { baseArmor = value; }
        }

        private int baseAttack;
        public int BaseAttack
        {
            get { return baseAttack; }
            set { baseAttack = value; }
        }

        #endregion


        int armor;
        public int Armor
        {
            get
            {
                return armor;
            }

            set
            {
                armor = value;
                RaisePropertyChanged("Armor");
            }
        }

        int attack;
        public int Attack
        {
            get
            {
                return attack;
            }

            set
            {
                attack = value;
                RaisePropertyChanged("Attack");
            }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private Weapon weapon;
        public Weapon Weapon
        {
            get { return weapon; }
            set
            {
                weapon = value;
                if (value != null)
                {
                    this.Attack = (this.BaseAttack + value.DamageValue);
                }
                RaisePropertyChanged("Weapon");
            }
        }

        private Shield shield;
        public Shield Shield
        {
            get { return shield; }
            set
            {
                shield = value;
                if (value != null)
                {
                    this.Armor = (this.BaseArmor + value.ArmorValue);
                }
                RaisePropertyChanged("Shield");
            }
        }

        private Potion potion;
        public Potion Potion
        {
            get { return potion; }
            set
            {
                potion = value;
                RaisePropertyChanged("Potion");
            }
        }

        private List<Skill> skills;
        public List<Skill> Skills
        {
            get { return skills; }
            set { skills = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private bool isParing;
        public bool IsParing
        {
            get { return isParing; }
            set { isParing = value; }
        }

        public Hero()
        {
            
        }


        public Hero(string name, int life, int armor, int attackValue, int level, string type, string imagePath)
        {
            this.Name = name;
            this.Life = life;
            this.Armor = armor;
            this.Attack = attackValue;
            this.Type = type;
            this.ImagePath = imagePath;
            this.Skills = new List<Skill>();
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
