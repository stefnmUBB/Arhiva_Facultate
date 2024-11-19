package pizzashop.utils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class ListUtils {
    @SafeVarargs
    public static <T> List<T> of(T... args){
        List<T> list=new ArrayList<>();
        Collections.addAll(list, args);
        return list;
    }
}
