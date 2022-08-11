// abstract class which is realized by classes: comedy and tragedy
public abstract class BaseGenre {
    protected Genre Type;

    public abstract int calculateAmount(int audience);

    public abstract int calculateExtra(int audience);
}
