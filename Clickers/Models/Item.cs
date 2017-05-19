﻿using Clickers.Models.Base;
using Clickers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.Models
{
    public class Item : BaseDBEntity
    {

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

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        private Enums.ItemTypes type;
        public Enums.ItemTypes Type
        {
            get { return type; }
            set { type = value; }
        }

        public Item()
        {

        }

    }
}
