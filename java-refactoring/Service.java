import com.fasterxml.jackson.annotation.JsonSubTypes;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class Service {
    List<BaseGenre> list = new ArrayList<>();

    public Service()
    {
        // initializing all types of genres that we have
        Tragedy tragedy = new Tragedy(Genre.Tragedy);
        Comedy comedy = new Comedy(Genre.Comedy);
        list.add(tragedy);
        list.add(comedy);
    }

    // getter of genre
    public BaseGenre getGenre(Genre type)
    {
        for(BaseGenre baseGenre : list){
            if (baseGenre.Type == type)
                return baseGenre;
        }
        return null;
    }
}
