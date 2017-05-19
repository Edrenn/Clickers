using Clickers.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<SoldiersProducer> GetAllSoldierProducersFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "SoldiersProducer.Json";
            List<SoldiersProducer> existingProducer = new List<SoldiersProducer>();

            using (StreamReader fileItem = File.OpenText(path + file))
            using (JsonTextReader reader = new JsonTextReader(fileItem))
            {
                string jSonContent = fileItem.ReadToEnd();
                existingProducer = JsonConvert.DeserializeObject<List<SoldiersProducer>>(jSonContent, new JsonSerializerSettings());
            }
            foreach (SoldiersProducer producer in existingProducer)
            {
                producer.Name = ConvertToUTF8(producer.Name);
                //producer.AllUnitsType.Name = ConvertToUTF8(producer.AllUnitsType.Name);
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

        public List<Hero> GetAllHerosFromJSon()
        {
            string path = "D:\\Workspaces\\Clickers\\Clickers\\JsonConfig\\";
            string file = "Heros.Json";
            List<Hero> existingHero = new List<Hero>();

            using (StreamReader fileItem = File.OpenText(path + file))
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

        public string ConvertToUTF8(string itemToConvert)
        {
            byte[] utf8Bytes = new byte[itemToConvert.Length];
            for (int i = 0; i < itemToConvert.Length; ++i)
            {
                utf8Bytes[i] = (byte)itemToConvert[i];
            }
            string itemToReturn = Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
            return itemToReturn;
        }
    }
}
