using System.ComponentModel.Design;
using System.Text.Json;


public class Program
{
    private enum Genre
    {
        Tragedy,
        Comedy,
        None
    }

    static Genre GetGenre(string inString)
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

    static void Statement(JsonDocument invoices, JsonDocument plays)
    {
        int totalAmount = 0;
        int volumeCredits = 0;
      
        foreach (JsonElement item in invoices.RootElement.EnumerateArray())
        {
            try
            {
                string result = "";
                string customer = item.GetProperty("customer").GetString();
                result = $"Statement for {customer} \n";
                
                var performances = item.GetProperty("performances").EnumerateArray();
                foreach (var performance in performances)
                {
                    int thisAmount = 0;
                    string playId = performance.GetProperty("playID").GetString();
                    var play = plays.RootElement.GetProperty(playId);

                    string playType = play.GetProperty("type").GetString();
                    string playName = play.GetProperty("name").GetString();

                    int audience = performance.GetProperty("audience").GetInt32();
                    Genre genre = GetGenre(playType); //  Enum.Parse(typeof(StringCode), playType, true);

                    thisAmount = CalculateAmount(genre, audience);
                    volumeCredits += CalculateExtra(genre, audience);

                    // print line for this order
                    result += $" {playName}: ${(thisAmount / 100)} ({audience} seats) \n";
                    totalAmount += thisAmount;
                }

                result += $"Amount owed is $ {(totalAmount / 100)} \n";
                result += $"You earned {volumeCredits} credits \n";
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong " + e.Message);
            }
        }
    }
    static int CalculateAmount(Genre type, int audience)
    {
        int thisAmount = 0;
        switch (type)
        {
            case Genre.Tragedy:
                thisAmount = 40000;
                if (audience > 30)
                {
                    thisAmount += 1000 * (audience - 30);
                }

                break;
            case Genre.Comedy:
                thisAmount = 30000;
                if (audience > 20)
                {
                    thisAmount += 10000 + 500 * (audience - 20);
                }

                thisAmount += 300 * audience;
                break;
            default:
                return thisAmount;
        }

        return thisAmount;
    }

    static int CalculateExtra(Genre type, int audience)
    {
        // add volume credits
        int volumeCredits = Math.Max(audience - 30, 0);
        // add extra credit for every ten comedy attendees
        if (type == Genre.Comedy)
            volumeCredits += (int) Math.Floor((double) audience / 5.0d);

        return volumeCredits;
    }

    static void Main(string[] args)
    {
        var playsFile =
            File.ReadAllText("plays.json");
        var invoicesFile =
            File.ReadAllText("invoices.json");

        var plays = JsonDocument.Parse(playsFile);
        var invoices = JsonDocument.Parse(invoicesFile);

        Statement(invoices, plays);
    }
}

