using Clickers.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models
{
    [Table("soldiersProducer")]
    public class SoldiersProducer : BaseDBEntity
    {
        private int id;
        [Key]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
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

        private int price;
        public int Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        private int level;
        public int Level
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }

        private Soldier soldierType;
        public Soldier SoldierType
        {
            get
            {
                return soldierType;
            }

            set
            {
                soldierType = value;
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
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
