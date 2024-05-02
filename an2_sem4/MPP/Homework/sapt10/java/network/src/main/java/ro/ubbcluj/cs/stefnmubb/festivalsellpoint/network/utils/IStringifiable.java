package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils;

import org.apache.commons.text.StringEscapeUtils;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils.Reflection;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils.SerParser;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils.Stringifier;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public interface IStringifiable {

    default String encode() {
        var classType = this.getClass();
        var fields = Reflection.getAllFields(classType);

        var propsStr = new ArrayList<String>();

        for (var f : fields) {
            f.setAccessible(true);
            try {
                String value = Stringifier.encode(f.get(this));
                propsStr.add(f.getName() + "=" + value);
            } catch (IllegalAccessException e) {
                throw new RuntimeException(e);
            }
        }

        String result = String.join(";", propsStr);

        return classType.getSimpleName() + "{" + result + "}";
    }
}
