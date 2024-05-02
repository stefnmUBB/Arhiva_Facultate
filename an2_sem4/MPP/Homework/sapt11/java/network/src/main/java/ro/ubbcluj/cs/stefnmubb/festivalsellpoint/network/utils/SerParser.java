package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.network.utils;

import org.apache.commons.text.StringEscapeUtils;

import javax.lang.model.SourceVersion;
import java.lang.reflect.Array;
import java.lang.reflect.InvocationTargetException;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class SerParser {

    interface BuildMethod {
        Object run(Object[] identifiers);
    }

    List<ParseRule> parseRules = new ArrayList<>();

    private Object self(Object[] idt){return idt[0];}

    private Object buildEmptyArrayProperty(Object[] idt) {
        return new PropertyValue((String) idt[0], new Object[0]);
    }

    private Object buildProperty(Object[] idt) {
        if(idt[1]==null) {
            var p=new PropertyValue((String)idt[0], null);
            System.out.println(p);
            return p;
        }

        if(!idt[1].getClass().isArray()){
            var p=new PropertyValue((String)idt[0], List.of(idt[1]).toArray());
            System.out.println(p);
            return p;
        }

        var p=new PropertyValue((String)idt[0], (Object[])idt[1]);
        System.out.println(p);
        return p;
    }

    private Object prepend(Object[] idt) {
        if(!idt[1].getClass().isArray()) {
            return Arrays.asList(idt[0], idt[1]).toArray();
        }

        Object first = idt[0];
        Object[] arr = (Object[])idt[1];
        Object[] result = new Object[1+arr.length];
        result[0]=first;
        System.arraycopy(arr, 0, result, 1, arr.length);
        return result;
    }

    private Object buildClassNoProps(Object[] idt) {
        var classType = Reflection.getClassBySimpleName((String)idt[0]);
        try {
            var result = classType.getDeclaredConstructor().newInstance();
            return result;
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    private Object buildClass(Object[] idt) {
        /*System.out.println("Class "+String.join(" ", Arrays.stream(idt).map(o->{
            if(o.getClass().isArray()) {
                return Arrays.toString((Object[])o);
            }
            return o.toString();
        }).toList()));*/

        var classType = Reflection.getClassBySimpleName((String)idt[0]);

        List<PropertyValue> vprops = null;
        if(idt[1].getClass().isArray()) {
            vprops = Arrays.stream((Object[]) idt[1]).map(x -> (PropertyValue) x).toList();
        }
        else{
            vprops = List.of((PropertyValue) idt[1]);
        }

        System.out.println(classType);
        System.out.println(Arrays.toString(vprops.toArray()));

        try {
            var result = classType.getDeclaredConstructor().newInstance();

            for(var pv : vprops) {
                var field = Reflection.getField(classType, pv.getName());
                if(field==null) throw new NullPointerException("Invalid field");
                field.setAccessible(true);

                if(pv.getValue()==null) {
                    field.set(result, null);
                }
                else if(!field.getType().isArray()) {
                    field.set(result, pv.getValue()[0]);
                }
                else {
                    var arr = Array.newInstance(field.getType().getComponentType(),pv.getValue().length);
                    System.arraycopy(pv.getValue(),0,arr,0,pv.getValue().length);
                    field.set(result, arr);
                }
            }
            return result;
        } catch (Exception e) {
            throw new RuntimeException(e);
        }

        /*return "Class "+String.join(" ", Arrays.stream(idt).map(o->{
            if(o.getClass().isArray()) {
                return Arrays.toString((Object[])o);
            }
            return o.toString();
        }).toList());*/
    }


    public SerParser(){
        registerRule("@E", "NAME { }", this::buildClassNoProps);
        registerRule("@E", "NAME { @PROPS }", this::buildClass);
        registerRule("@E", "STRING", this::self);
        registerRule("@E", "NUMBER", this::self);
        registerRule("@E", "null", this::nullValue);

        registerRule("@ELIST", "@E , @ELIST", this::prepend);
        registerRule("@ELIST", "@E", this::self);

        registerRule("@PROPS", "@PROP ; @PROPS", this::prepend);
        registerRule("@PROPS", "@PROP", this::self);
        registerRule("@PROP", "NAME = @ELIST", this::buildProperty);
        registerRule("@PROP", "NAME = empty", this::buildEmptyArrayProperty);
    }

    private Object nullValue(Object[] objects) {
        return null;
    }

    public Object parse(String input) {
        var result =lookFor("@E", splitToTokens(input),0);
        if(result==null) return null;
        return result.getValue();
    }

    private Record lookFor(ParseRule rule, List<String> tokens, int pos){
        System.out.println("Checking "+rule + " at "+pos);
        var pattern = rule.parsePattern;

        List<Record> wildcardMatches = new ArrayList<>();

        int originalPos = pos;
        for (String wildcard : pattern) {
            if (pos >= tokens.size()) return null;

            if (wildcard.startsWith("@")) {
                var rec = lookFor(wildcard, tokens, pos);
                if (rec == null)
                    return null;
                pos += rec.getSize();
                wildcardMatches.add(rec);
            } else if ("NUMBER".equals(wildcard)) {
                try {
                    int number = Integer.parseInt(tokens.get(pos));
                    var rec = new Record(wildcard, number, pos, 1);
                    pos++;
                    addToCache(rec);
                    wildcardMatches.add(rec);
                } catch (NumberFormatException e) {
                    return null;
                }
            } else if ("STRING".equals(wildcard)) {
                var token = tokens.get(pos);
                if (!(token.startsWith("\"") && token.endsWith("\"")))
                    return null;
                String str =  token.substring(1, token.length() - 1);
                str = StringEscapeUtils.unescapeJava(str);
                var rec = new Record(wildcard, str, pos, 1);
                pos++;
                addToCache(rec);
                wildcardMatches.add(rec);
            } else if ("NAME".equals(wildcard)) {
                if (!SourceVersion.isIdentifier(tokens.get(pos)))
                    return null;
                var rec = new Record(wildcard, tokens.get(pos), pos, 1);
                pos++;
                addToCache(rec);
                wildcardMatches.add(rec);
            } else {
                if (!Objects.equals(wildcard, tokens.get(pos)))
                    return null;
                pos++;
            }
        }

        var value = rule.buildMethod.run(wildcardMatches.stream().map(Record::getValue)
                .toArray());
        var rec=new Record(rule.key, value, originalPos, pos-originalPos);
        addToCache(rec);
        return rec;
    }

    private Record lookFor(String ruleKey, List<String> tokens, int pos) {
        var fromCache = getFromCache(ruleKey, pos);
        if(fromCache!=null)
            return fromCache;

        for(var rule:getRules(ruleKey)){
            var rec = lookFor(rule, tokens, pos);
            if(rec!=null) {
                parseCache.add(rec);
                return rec;
            }
        }

        return null;
    }


    private Iterable<ParseRule> getRules(String ruleKey) {
        return parseRules.stream().filter(r-> Objects.equals(r.key, ruleKey))
                .toList();
    }

    private final List<Record> parseCache = new ArrayList<>();

    private void addToCache(Record r) {
        if(getFromCache(r.ruleKey, r.position)==null)
            parseCache.add(r);
    }

    private Record getFromCache(String ruleKey, int pos){
        return parseCache.stream().filter(r->r.position==pos
                && Objects.equals(r.ruleKey, ruleKey)).findFirst().orElse(null);
    }

    static class Record {
        private final String ruleKey;
        private final Object value;
        private final int position;
        private final int size;

        @Override
        public String toString() {
            return "Record{" +
                    "ruleKey='" + ruleKey + '\'' +
                    ", value=" + value +
                    ", position=" + position +
                    ", size=" + size +
                    '}';
        }

        public int getSize() {
            return size;
        }

        public int getPosition() {
            return position;
        }

        public String getRuleKey() {
            return ruleKey;
        }

        public Object getValue() {
            return value;
        }

        public Record(String ruleKey, Object value, int position, int size) {
            this.ruleKey = ruleKey;
            this.value = value;
            this.position = position;
            this.size = size;
        }
    }


    private void registerRule(String key, String rule, BuildMethod buildMethod) {
        parseRules.add(new ParseRule(key, rule, buildMethod));
    }

    static class PropertyValue {
        private final String name;
        private final Object[] value;

        public String getName() {
            return name;
        }

        public Object[] getValue() {
            return value;
        }

        public PropertyValue(String name, Object[] value) {
            this.name = name;
            this.value = value;
        }

        @Override
        public String toString() {
            return "PropertyValue{" +
                    "name='" + name + '\'' +
                    ", value=" + Arrays.toString(value) +
                    '}';
        }
    }


    static class ParseRule {
        String key;
        String[] parsePattern;

        BuildMethod buildMethod;

        @Override
        public String toString() {
            return "ParseRule{" +
                    "key='" + key + '\'' +
                    ", parsePattern=" + Arrays.toString(parsePattern) +
                    '}';
        }

        public ParseRule(String key, String rule, BuildMethod buildMethod){
            this.key = key;
            this.parsePattern = rule.split(" ");
            this.buildMethod = buildMethod;
        }
    }


    // in  : ErrorRespone{message="error!"}
    // out : "ErrorResponse", "{", "message", "=", "\"error!\"", "}"
    public static List<String> splitToTokens(String input) {
        Pattern pattern = Pattern.compile("([\"])(.*?)(?<!\\\\)(?>\\\\\\\\)*\\1|([^\"\\s]+)", Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(input);
        String splitPattern = "((?=[{};=,])|(?<=[{};=,]))";

        List<String> matches = new ArrayList<>();
        while(matcher.find()) matches.add(matcher.group());

        return matches.stream()
                .filter(s-> !Objects.equals(s, ""))
                .map(s->{
                    if(s.charAt(0)=='"') return List.of(s);
                    return Arrays.stream(s.split(splitPattern)).toList();
                })
                .flatMap(List::stream)
                .toList();
    }

}
