package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Reflection {
    static List<Field> getAllFields(List<Field> fields, Class<?> type) {
        fields.addAll(Arrays.asList(type.getDeclaredFields()));

        if (type.getSuperclass() != null) {
            getAllFields(fields, type.getSuperclass());
        }

        return fields;
    }

    public static List<Field> getAllFields(Class<?> type){
        return getAllFields(new ArrayList<>(), type);
    }

    public static Field getField(Class<?> type, String name) {
        return getAllFields(type).stream().filter(f-> f.getName().equals(name))
                .findFirst().orElse(null);
    }

    public static Class<?> getClassBySimpleName(String name) {
        for(var pack: Package.getPackages()){
            if(pack.getName().startsWith("ro.ubbcluj.cs.stefnmubb")) {
                try{
                    return Class.forName(pack.getName()+"."+name);
                } catch (ClassNotFoundException ignored) {}
            }
        }
        throw new RuntimeException("Class not found : "+name);
    }
}
