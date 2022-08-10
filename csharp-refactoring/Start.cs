using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace c__refactoring
{
    public class Start
    {
        public Start()
        {
            // deserialization
            var playsFile =
               File.ReadAllText("plays.json");
            var invoicesFile =
                File.ReadAllText("invoices.json");
            plays = JsonConvert.DeserializeObject<RootPlays>(playsFile);
            rootPerfomance = JsonConvert.DeserializeObject<List<RootPerfomance>>(invoicesFile);

            Statement();
        }

        public RootPlays plays = new RootPlays();
        public List<RootPerfomance> rootPerfomance = new List<RootPerfomance>();

        // getting Genre of its string name
        Genre GetGenre(string inString)
        {
            switch (inString)
            {
                case "tragedy":
                    return Genre.Tragedy;
                case "comedy":
                    return Genre.Comedy;
                default:
                    return Genre.None;
            }
        }

        // the main part, here is console output created
        void Statement()
        {
            float totalAmount = 0;
            int volumeCredits = 0;

            // initializing service for interaction with different types of genres
            Test service = new Test();

            try
            {
                foreach (RootPerfomance rootPerfomance in rootPerfomance)
                {
                    string result = "";
                    result = $"Statement for {rootPerfomance.customer} \n";
                    foreach (Performance perfomance in rootPerfomance.performances)
                    {
                        float thisAmount = 0;
                        string playId = perfomance.playID;
                        Plays play = plays.Plays.Find(x => x.playID.Equals(playId));

                        string playType = play.type;
                        string playName = play.name;

                        int audience = perfomance.audience;
                        Genre genreType = GetGenre(playType);

                        //calculating 
                        var genre = service.GetGenre(genreType);
                        thisAmount = genre.CalculateAmount(audience);
                        volumeCredits += genre.CalculateExtra(audience);

                        // print line for this order
                        result += $" {playName}: ${(thisAmount / 100)} ({audience} seats) \n";
                        totalAmount += thisAmount;
                    }

                    result += $"Amount owed is $ {(totalAmount / 100)} \n";
                    result += $"You earned {volumeCredits} credits \n";
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong " + ex.Message);
            }
        }

        // another variant instead of Newtonsoft
        static void MoveToObjects(JsonDocument invoices, JsonDocument plays)
        {
            try
            {
                foreach (JsonElement item in invoices.RootElement.EnumerateArray())
                {
                    RootPerfomance root = new RootPerfomance();
                    string customer = item.GetProperty("customer").GetString();
                    root.customer = customer;

                    var performances = item.GetProperty("performances").EnumerateArray();
                    List<Performance> performancesList = new List<Performance>();
                    foreach (var performance in performances)
                    {
                        Performance performanceAdd = new Performance();
                        string playId = performance.GetProperty("playID").GetString();
                        performanceAdd.playID = playId;

                        var foundPlay = plays.RootElement.GetProperty(playId);

                        string playType = foundPlay.GetProperty("type").GetString();
                        string playName = foundPlay.GetProperty("name").GetString();

                        // listPlay.Add(play);

                        int audience = performance.GetProperty("audience").GetInt32();
                        performanceAdd.audience = audience;

                        performancesList.Add(performanceAdd);
                    }

                    root.performances = performancesList;
                    //    listPerformance.Add(root);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong " + ex.Message);
            }
        }
    }
}
