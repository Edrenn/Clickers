using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

using Clickers.Models;
using Clickers.Models.Items;
using Clickers.Models.Buildings;

namespace Clickers.Json
{
    class JsonManager
    {

        private static volatile JsonManager instance;
        private static object syncRoot = new Object();

        private JsonManager() { }

        public static JsonManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new JsonManager();
                    }
                }
                return instance;
            }
        }

        public T ReadFile<T>(String path, String file)
        {

            T toReturn = default(T);
            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                JObject jObject = (JObject)JToken.ReadFrom(reader);
                toReturn = jObject.ToObject<T>();
            }

            return toReturn;
        }

        public T ReadFileToList<T>(String path, String file)
        {

            T toReturn = default(T);
            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                JArray jObject = (JArray)JToken.ReadFrom(reader);
                toReturn = jObject.ToObject<T>();
            }

            return toReturn;
        }


        public Castle GetCastleFromJSon(string path)
        {
            Castle existingCastle = new Castle();

            using (StreamReader fileItem = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingCastle = JsonConvert.DeserializeObject<Castle>(jSonContent, new JsonSerializerSettings());
            }
            if (String.IsNullOrEmpty(existingCastle.Name))
            {
                existingCastle.Name = ConvertToUTF8(existingCastle.Name);
            }
            return existingCastle;
        }

        public List<RessourceProducer> GetAllGoldProducersFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "GoldProducer.Json";
            List<RessourceProducer> existingProducer = new List<RessourceProducer>();

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingProducer = JsonConvert.DeserializeObject<List<RessourceProducer>>(jSonContent, new JsonSerializerSettings());
            }
            foreach (RessourceProducer producer in existingProducer)
            {
                producer.Name = ConvertToUTF8(producer.Name);
                producer.TypeRessource = ConvertToUTF8(producer.TypeRessource);
            }
            return existingProducer;
        }

        public List<SoldiersProducer> GetAllSoldierProducersFromJSon(string path)
        {
            List<SoldiersProducer> existingProducer = new List<SoldiersProducer>();

            using (StreamReader fileItem = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingProducer = JsonConvert.DeserializeObject<List<SoldiersProducer>>(jSonContent, new JsonSerializerSettings());
            }
            foreach (SoldiersProducer producer in existingProducer)
            {
                producer.Name = ConvertToUTF8(producer.Name);
            }
            return existingProducer;
        }

        public List<Soldier> GetAllSoldiersFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "Soldiers.Json";
            List<Soldier> existingSoldier = new List<Soldier>();

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingSoldier = JsonConvert.DeserializeObject<List<Soldier>>(jSonContent, new JsonSerializerSettings());
            }
            foreach (Soldier soldier in existingSoldier)
            {
                soldier.Name = ConvertToUTF8(soldier.Name);

            }
            return existingSoldier;
        }

        public List<Hero> GetAllHerosFromJSon(string path)
        {
            List<Hero> existingHero = new List<Hero>();

            using (StreamReader fileItem = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingHero = JsonConvert.DeserializeObject<List<Hero>>(jSonContent, new JsonSerializerSettings());
            }
            foreach (Hero hero in existingHero)
            {
                hero.Name = ConvertToUTF8(hero.Name);

            }
            return existingHero;
        }

        public HealerHouse GetHealerHouseFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "HealerHouse.Json";
            HealerHouse existingHealerHouse = new HealerHouse();

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingHealerHouse = JsonConvert.DeserializeObject<HealerHouse>(jSonContent, new JsonSerializerSettings());
            }
            foreach (Potion potion in existingHealerHouse.PotionList)
            {
                potion.Name = ConvertToUTF8(potion.Name);
                potion.Description = ConvertToUTF8(potion.Description);

            }
            return existingHealerHouse;
        }

        public List<Shield> GetShieldsFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "Shields.Json";
            List<Shield> existingShields = new List<Shield>();

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingShields = JsonConvert.DeserializeObject<List<Shield>>(jSonContent, new JsonSerializerSettings());
            }
            foreach (Shield shield in existingShields)
            {
                shield.Name = ConvertToUTF8(shield.Name);
                shield.Description = ConvertToUTF8(shield.Description);

            }
            return existingShields;
        }

        public Blacksmith GetBlacksmithFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "Blacksmith.Json";
            Blacksmith existingBlacksmith = new Blacksmith();

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingBlacksmith = JsonConvert.DeserializeObject<Blacksmith>(jSonContent, new JsonSerializerSettings());
            }
            foreach (Shield equipment in existingBlacksmith.ShieldList)
            {
                equipment.Name = ConvertToUTF8(equipment.Name);
                equipment.Description = ConvertToUTF8(equipment.Description);

            }
            foreach (Weapon equipment in existingBlacksmith.WeaponList)
            {
                equipment.Name = ConvertToUTF8(equipment.Name);
                string toto = ConvertToUTF8(equipment.Name);
                equipment.Description = ConvertToUTF8(equipment.Description);

            }
            return existingBlacksmith;
        }

        public string ConvertToUTF8(string itemToConvert)
        {
            //byte[] utf8Bytes = new byte[itemToConvert.Length];
            //for (int i = 0; i < itemToConvert.Length; ++i)
            //{
            //    utf8Bytes[i] = (byte)itemToConvert[i];
            //}
            if (!String.IsNullOrEmpty(itemToConvert))
            {
                byte[] bytes = Encoding.Default.GetBytes(itemToConvert);
                string itemToReturn = Encoding.UTF8.GetString(bytes);
                return itemToReturn;
            }
            //string itemToReturn = Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
            return null;
        }

        public String SerializeHeroJson(Hero hero)
        {
            String newHero ="";
                newHero = new JavaScriptSerializer().Serialize(hero);
                Console.WriteLine(newHero);
            return newHero;
        }

        public String SerializeSoldierProducerJson(SoldiersProducer soldiersProducer)
        {
            String newSoldiersProducer = "";
            newSoldiersProducer = new JavaScriptSerializer().Serialize(soldiersProducer);
            Console.WriteLine(newSoldiersProducer);
            return newSoldiersProducer;
        }

        public String SerializeCastleJson(Castle castle)
        {
            String newCastle = "";
            newCastle = new JavaScriptSerializer().Serialize(castle);
            Console.WriteLine(newCastle);
            return newCastle;
        }
    }
}
