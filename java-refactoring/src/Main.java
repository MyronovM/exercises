import java.io.File;
import java.nio.file.Files;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;

/**
 * 
 */
public class Main {

	private enum Genre {
		tragedy, comedy, none
	}

	static Genre getGenre(String inString) {
		switch (inString)
		{
			case "tragedy":
				return Genre.tragedy;
			case "comedy":
				return Genre.comedy;
			default:
				return Genre.none;
		}
	}

	static void statement(JsonArray invoices, JsonObject plays) {

		float totalAmount = 0;
		int volumeCredits = 0;

		try	{
			for(JsonElement item : invoices.getAsJsonArray()){
				String result = "";
				String customer = item.getAsJsonObject().get("customer").getAsString();
				result = String.format("Statement for %s \n", customer);

				var performances = item.getAsJsonObject().get("performances").getAsJsonArray();
				for(JsonElement performance : performances){
					float thisAmount = 0;
					String playId = performance.getAsJsonObject().get("playID").getAsString();
					var play = plays.getAsJsonObject().get(playId);

					String playType = play.getAsJsonObject().get("type").getAsString();
					String playName = play.getAsJsonObject().get("name").getAsString();

					int audience = performance.getAsJsonObject().get("audience").getAsInt();
					Genre genre = getGenre(playType); //  Enum.Parse(typeof(StringCode), playType, true);

					thisAmount = calculateAmount(genre, audience);
					volumeCredits += calculateExtra(genre, audience);

					// print line for this order
					result += String.format(" %s: $%.2f (%d seats)\n", playName, thisAmount / 100, audience);
					totalAmount += thisAmount;
				}
				result += String.format("Amount owed is $%.2f \n", totalAmount / 100);
				result += String.format("You earned %d credits \n", volumeCredits);
				System.out.println(result);
			}
		}
		catch (Exception e)	{
			System.out.println("Something went wrong" + e);
		}
	}

	static int calculateAmount(Genre type, int audience) {
		int thisAmount = 0;
		switch (type)	{
			case tragedy:
				thisAmount = 40000;
				if (audience > 30)
				{
					thisAmount += 1000 * (audience - 30);
				}

				break;
			case comedy:
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

	static int calculateExtra(Genre type, int audience) {
		// add volume credits
		int volumeCredits = Math.max(audience - 30, 0);
		// add extra credit for every ten comedy attendees
		if (type.equals(Genre.comedy))
			volumeCredits += (int) Math.floor((double) audience / 5.0d);

		return volumeCredits;
	}

	public static void main(String[] args) throws Exception {

		JsonParser parser = new JsonParser();

		String playsFile = new String(Files.readAllBytes(new File("plays.json").toPath()));

		String invoicesFile = new String(Files.readAllBytes(new File("invoices.json").toPath()));

		JsonObject plays = parser.parse(playsFile).getAsJsonObject();

		JsonArray invoices = parser.parse(invoicesFile).getAsJsonArray();

		statement(invoices, plays);
	}

}
