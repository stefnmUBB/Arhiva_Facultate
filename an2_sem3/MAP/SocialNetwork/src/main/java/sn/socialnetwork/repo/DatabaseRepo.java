package sn.socialnetwork.repo;

import sn.socialnetwork.domain.Entity;
import sn.socialnetwork.domain.validators.IValidator;
import sn.socialnetwork.utils.Constants;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.sql.*;
import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.IntStream;

public class DatabaseRepo<ID extends Long, E extends Entity<ID>> implements IRepo<ID,E> {
    private final String url;
    private final String username;
    private final String password;

    final IValidator<E> validator;
    public DatabaseRepo(Class<E> type, IValidator<E> validator) {
        this(type,"jdbc:postgresql://localhost:5432/SocialNetwork",
                "postgres","0000", validator);
    }

    public DatabaseRepo(Class<E> type, String url, String username, String password, IValidator<E> validator) {
        this.type = type;
        this.url = url;
        this.username = username;
        this.password = password;
        this.validator = validator;
    }

    @Override
    public Iterable<E> getAll() {
        String sql = "Select * FROM public.\"" + getEntityTypeName()+"\" ORDER BY id ASC";
        System.out.println(sql);
        List<E> entities = new ArrayList<>();
        try(Connection connection = DriverManager.getConnection(url,username,password);
            PreparedStatement ps = connection.prepareStatement(sql);
            ResultSet result = ps.executeQuery()){
            while (result.next()){
                Long id = result.getLong("id");
                E entity = buildEntity(result);
                entity.setId((ID) id);
                entities.add(entity);
            }
        }catch (SQLException e){
            System.out.println(e.getMessage());
            System.exit(-1);
        }
        return entities;
    }

    @Override
    public E add(E entity) throws EntityAlreadyExistsException {
        validator.validate(entity);
        List<String> columns = new ArrayList<>();
        getEntityFieldNames().forEach(f->columns.add("\""+f+"\""));

        String sql = "INSERT INTO public.\"" + getEntityTypeName() + "\" ("
                + String.join(", ", columns) + ")"
                + " VALUES ";

        List<String> values = new ArrayList<>();
        for(var fname : getEntityFieldNames()) {
            Object val = getEntityFieldValue(entity, fname);
            System.out.println(val);
            switch(getEntityFieldType(fname)){
                case "String" :
                    values.add("'"+(String)val+"'");
                    break;
                case "Integer" :
                    assert val instanceof Integer;
                    values.add(((Integer)val).toString());
                    break;
                case "Long" :
                    assert val instanceof Long;
                    values.add(((Long)val).toString());
                    break;
                case "LocalDateTime":
                    values.add("'"+((LocalDateTime)val).format(Constants.DATE_TIME_FORMATTER)+"'");
                    break;
                default:
                    throw new RuntimeException("Not handled type:"+getEntityFieldType(fname));
            }
        }
        sql+="("+String.join(", ",values)+")";
        System.out.println(sql);
        String generatedColumns[] = { "id" };

        try(Connection connection = DriverManager.getConnection(url,username,password);
            PreparedStatement ps = connection.prepareStatement(sql, generatedColumns);
            ){
            ps.executeUpdate();
            try(ResultSet result = ps.getGeneratedKeys()){
                if(result.next()){
                    Long id = result.getLong(1);
                    entity.setId((ID)id);
                    return entity;
                } else {
                    return null;
                }
            }
        }catch (SQLException e){
            System.out.println(e.getMessage());
            System.exit(-1);
        }
        return null;
    }

    @Override
    public E update(E entity) {
        List<String> columns = new ArrayList<>();
        getEntityFieldNames().forEach(f->columns.add("\""+f+"\""));

        List<String> values = new ArrayList<>();
        for(var fname : getEntityFieldNames()) {
            Object val = getEntityFieldValue(entity, fname);
            System.out.println(val);
            switch(getEntityFieldType(fname)){
                case "String" :
                    values.add("'"+(String)val+"'");
                    break;
                case "Integer" :
                    assert val instanceof Integer;
                    values.add(((Integer)val).toString());
                    break;
                default:
                    throw new RuntimeException("Not handled type:"+getEntityFieldType(fname));
            }
        }

        List<String> kv = IntStream.range(0, columns.size())
                        .mapToObj(i -> Map.entry(columns.get(i), values.get(i)))
                        .map(entry -> "\"" + entry.getKey() + "\" = " + entry.getValue())
                        .toList();

        String sql = "UPDATE public.\"" + getEntityTypeName() + "\" SET "
                + String.join(", ", kv) + " WHERE \"id\" = "
                + entity.getId();

        System.out.println(sql);

        try(Connection connection = DriverManager.getConnection(url,username,password);
            PreparedStatement ps = connection.prepareStatement(sql);
        ){
            ps.executeUpdate();
            try(ResultSet result = ps.getGeneratedKeys()){
                if(result.next()){
                    return entity;
                } else {
                    return null;
                }
            }
        }catch (SQLException e){
            System.out.println(e.getMessage());
            System.exit(-1);
        }
        return null;
    }

    @Override
    public E remove(ID id) {
        E entity = getById(id);
        if(entity==null) return null;
        String sql = "DELETE FROM public.\"" + getEntityTypeName() + "\""
                + " WHERE \"id\" = " + id;

        System.out.println(sql);

        try(Connection connection = DriverManager.getConnection(url,username,password);
            PreparedStatement ps = connection.prepareStatement(sql);
        ){
            ps.executeUpdate();
            return entity;
        }catch (SQLException e){
            System.out.println(e.getMessage());
            System.exit(-1);
        }
        return null;
    }

    @Override
    public E getById(ID id) {
        String sql = "Select * FROM public.\"" + getEntityTypeName()+"\" WHERE \"id\" = "+id;
        try(Connection connection = DriverManager.getConnection(url,username,password);
            PreparedStatement ps = connection.prepareStatement(sql);
            ResultSet result = ps.executeQuery()){
            if (result.next()){
                E entity = buildEntity(result);
                entity.setId((ID) id);
                return entity;
            }
            return null;
        }catch (SQLException e){
            System.out.println(e.getMessage());
            System.exit(-1);
        }
        return null;
    }

    /// entity type data

    private Class<E> type;

    public Iterable<String> getEntityFieldNames() {
        return Arrays.stream(type.getDeclaredFields())
                .map(Field::getName)
                .toList();
    }

    public String getEntityFieldType(String fieldName) {
        try {
            return type.getDeclaredField(fieldName).getType().getSimpleName();
        } catch (NoSuchFieldException e) {
            return null;
        }
    }

    public String getEntityTypeName() {
        return type.getSimpleName();
    }

    public void setEntityFieldValue(E e, String fieldName, Object value) {
        try {
            var field = type.getDeclaredField(fieldName);
            field.setAccessible(true);
            field.set(e, value);
        } catch (NoSuchFieldException ex) {
            throw new RuntimeException(ex);
        } catch (IllegalAccessException ex) {
            throw new RuntimeException(ex);
        }
    }

    public Object getEntityFieldValue(E e, String fieldName) {
        try {
            Field f = type.getDeclaredField(fieldName);
            f.setAccessible(true);
            return f.get(e);
        } catch (IllegalAccessException ex) {
            throw new RuntimeException(ex);
        } catch (NoSuchFieldException ex) {
            throw new RuntimeException(ex);
        }
    }

    public E buildEntity(ResultSet result) throws SQLException {
        try {
            E entity = type.getConstructor().newInstance();

            for (String field : getEntityFieldNames()) {
                String ftype = getEntityFieldType(field);

                switch (ftype){
                    case "String" :
                        setEntityFieldValue(entity,field,result.getString(field));
                        break;
                    case "Integer" :
                        setEntityFieldValue(entity,field,result.getInt(field));
                        break;
                    case "Long":
                        setEntityFieldValue(entity,field,result.getLong(field));
                        break;
                    case "LocalDateTime":
                        setEntityFieldValue(entity,field,result.getTimestamp(field).toLocalDateTime());
                        break;
                    default: {
                        throw new RuntimeException("Type not handled : "+ftype);
                    }
                }
            }
            return entity;
        } catch (InvocationTargetException e) {
            throw new RuntimeException(e);
        } catch (InstantiationException e) {
            throw new RuntimeException(e);
        } catch (IllegalAccessException e) {
            throw new RuntimeException(e);
        } catch (NoSuchMethodException e) {
            throw new RuntimeException(e);
        }
    }

    public void loadFromRepo(IRepo<ID,E> repo) throws EntityAlreadyExistsException {
        for (E e : repo.getAll()) {
            add(e);
        }
    }
}
