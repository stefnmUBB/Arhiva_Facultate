package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils;

import org.apache.commons.text.StringEscapeUtils;

import java.util.Arrays;

public class Stringifier {

    public static String encode(Object object) {
        if(object instanceof Integer) {
            return Integer.toString((int)object);
        }
        if(object instanceof String){
            return "\""+StringEscapeUtils.escapeJava((String)object)+"\"";
        }

        if(object == null){
            return "null";
        }

        if(object instanceof IStringifiable) {
            return ((IStringifiable) object).encode();
        }

        if(object.getClass().isArray()) {
            if(((Object[])object).length==0)
                return "empty";
            return String.join(",", Arrays.stream(((Object[])object))
                    .map(Stringifier::encode).toList());
        }

        throw new RuntimeException("Cannot stringify object of type "+object.getClass().toString());
    }

    public static<T> T decode(String input, Class<T> tClass){
        return tClass.cast(new SerParser().parse(input));
    }
}
