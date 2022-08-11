public class Tragedy extends BaseGenre {
    public Tragedy(Genre type)
    {
        this.Type = type;
    }

    public int calculateAmount(int audience)
    {
        int thisAmount = 40000;

        if (audience > 30)
        {
            thisAmount += 1000 * (audience - 30);
        }

        return thisAmount;
    }

    public int calculateExtra(int audience)
    {
        // add volume credits
        int volumeCredits = Math.max(audience - 30, 0);

        return volumeCredits;
    }
}
