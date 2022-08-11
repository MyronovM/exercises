import com.fasterxml.jackson.databind.ObjectMapper;

import java.util.ArrayList;
import java.util.List;
import java.io.File;
import java.nio.file.Files;

public class Start {
    public RootPlays plays;
    public ArrayList<RootPerformance> rootPerformances = new ArrayList<>();

    public Start(){
        try {
            // deserialization
            ObjectMapper objectMapper = new ObjectMapper();

            String playsFile = new String(Files.readAllBytes(new File("plays.json").toPath()));
            String invoicesFile = new String(Files.readAllBytes(new File("invoices.json").toPath()));

            plays = objectMapper.readValue(playsFile, RootPlays.class);
            rootPerformances =
                    objectMapper.readValue(invoicesFile, objectMapper.getTypeFactory().constructCollectionType(List.class, RootPerformance.class));
        }catch (Exception e){
            System.out.println("Something went wrong" + e);
        }
        Statement();
    }

    // getting Genre of its string name
    Genre getGenre(String inString)
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
        Service service = new Service();

        try
        {
            for (RootPerformance rootPerformance : rootPerformances)
            {
                String result = "";
                String customer = rootPerformance.customer;
                result = String.format("Statement for %s \n", customer);

                var performances = rootPerformance.performances;
                for(var performance : performances){
                    Plays found = new Plays();
                    float thisAmount = 0;
                    String playId = performance.playID;
                    // searching for particular plays
                    for (Plays o: plays.Plays){
                        if (o.playID.equals(playId)){
                            found = o;
                            break;
                        }
                    }

                    String playType = found.type;
                    String playName = found.name;
                    Genre foundGenre = getGenre(playType);

                    int audience = performance.audience;
                    BaseGenre genre = service.getGenre(foundGenre); //  Enum.Parse(typeof(StringCode), playType, true);

                    thisAmount = genre.calculateAmount(audience);
                    volumeCredits += genre.calculateExtra(audience);

                    // print line for this order
                    result += String.format(" %s: $%.2f (%d seats)\n", playName, thisAmount / 100, audience);
                    totalAmount += thisAmount;
                }
                result += String.format("Amount owed is $%.2f \n", totalAmount / 100);
                result += String.format("You earned %d credits \n", volumeCredits);
                System.out.println(result);
            }
        }
        catch (Exception e)
        {
            System.out.println("Something went wrong " + e);
        }
    }
}
