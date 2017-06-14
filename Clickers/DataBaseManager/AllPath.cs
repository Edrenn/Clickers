using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clickers.DataBaseManager
{
    public class AllPath
    {
        private static AllPath instance;
        public static AllPath Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AllPath();
                }
                return instance;
            }
        }

        private string jsonCustomFolder = "../..//JsonConfig//CustomJson//";
        public string JsonCustomFolder
        {
            get { return jsonCustomFolder; }
        }

        private string jsonFolder = "../..//JsonConfig//";
        public string JsonFolder
        {
            get { return jsonFolder; }
        }

        private string baseSoldierProducer = "SoldiersProducer.json";
        public string BaseSoldierProducer
        {
            get { return baseSoldierProducer; }
        }

        private string baseHero = "Heros.json";
        public string BaseHero
        {
            get { return baseHero; }
        }

        private string customSoldierProducer = "CustomSoldiersProducer.json";
        public string CustomSoldierProducer
        {
            get { return customSoldierProducer; }
        }

        private string customHero = "CustomHero.json";
        public string CustomHero
        {
            get { return customHero; }
        }

        private string customCastle = "CustomCastle.json";
        public string CustomCastle
        {
            get { return customCastle; }
        }



    }
}
