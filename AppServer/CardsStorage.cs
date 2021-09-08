using AppServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

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
        public static List<Card> Cards = new List<Card>();

        public static void Save()
        {
            string jsonCard = "";
            string jsonCards = "";
            foreach(Card item in Cards)
            {
                jsonCard = JsonSerializer.Serialize(item);
                jsonCards += jsonCard;
            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(jsonCards);
            }
        }
        public static void Load()
        {
            string jsonCard = "";
            Card card;
            List<Card> items = new List<Card>();
            using (StreamReader reader = new StreamReader(path))
            {
                jsonCard = reader.ReadLine();
                card = JsonSerializer.Deserialize<Card>(jsonCard);
                items.Add(card);
            }
            Cards = items;
        }
        public static async void AddCardAsync(Card card)
        {
            string jsonCard = JsonSerializer.Serialize(card);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(jsonCard);
            }
        }
    }
}
