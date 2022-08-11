public class Comedy extends BaseGenre{
    public Comedy(Genre type)
    {
        this.Type = type;
    }

    public int calculateAmount(int audience)
    {
        int thisAmount = 30000;
        if (audience > 20)
        {
            thisAmount += 10000 + 500 * (audience - 20);
        }

        thisAmount += 300 * audience;
        return thisAmount;
    }

    public int calculateExtra(int audience)
    {
        // add volume credits
        int volumeCredits = Math.max(audience - 30, 0);
        // add extra credit for every ten comedy attendees
        volumeCredits += (int)Math.floor((double)audience / 5.0d);

        return volumeCredits;
    }
}
