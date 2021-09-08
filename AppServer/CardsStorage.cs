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


    //public static class CardsStorage
    //{
    //    static CardsStorage()
    //    {
    //        try
    //        {
    //            Load();
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.WriteLine(e.Message);
    //        }
    //    }

    //    private static readonly string path = "cards.json";
    //    public static List<SCard> Cards = new List<SCard>();

    //    public static void Save()
    //    {
    //        string jsonCard = "";
    //        string jsonCards = "";
    //        foreach(SCard item in Cards)
    //        {
    //            jsonCard = JsonSerializer.Serialize(item);
    //            jsonCards += jsonCard;
    //        }
    //        using (StreamWriter writer = new StreamWriter(path))
    //        {
    //            writer.WriteLine(jsonCards);
    //        }
    //    }
    //    public static void Load()
    //    {
    //        string jsonCard = "";
    //        SCard card;
    //        List<SCard> items = new List<SCard>();
    //        using (StreamReader reader = new StreamReader(path))
    //        {
    //            while (!reader.EndOfStream)
    //            {
    //                jsonCard = reader.ReadLine();
    //                card = JsonSerializer.Deserialize<SCard>(jsonCard);
    //                items.Add(card);
    //            }
    //        }
    //        Cards = items;
    //    }
    //    public static async void AddCardAsync(SCard card)
    //    {
    //        string jsonCard = JsonSerializer.Serialize(card);

    //        using (StreamWriter writer = new StreamWriter(path, true))
    //        {
    //            await writer.WriteLineAsync(jsonCard);
    //        }
    //    }
    //}
}
