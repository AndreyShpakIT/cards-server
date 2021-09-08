using AppServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AppServer
{
    public static class CardsStorage
    {
        static CardsStorage()
        {
            try
            {
                Load();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private static readonly string path = "cards.json";
        public static List<SCard> Cards = new List<SCard>();

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Cards);
            File.WriteAllText(path, json);
        }
        public static void Load()
        {
            Cards = JsonConvert.DeserializeObject<List<SCard>>(File.ReadAllText(path));
            if (Cards == null)
            {
                Cards = new List<SCard>();
            }
        }
    }
}
