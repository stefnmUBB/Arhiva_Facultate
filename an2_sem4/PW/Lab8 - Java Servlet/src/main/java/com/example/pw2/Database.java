package com.example.pw2;

import javax.xml.crypto.Data;
import java.sql.*;
import java.util.*;
import java.util.stream.StreamSupport;

public class Database {

    private JdbcUtils dbUtils;

    public Database(JdbcUtils dbUtils) {
        this.dbUtils = dbUtils;
    }

    public int executeNonQuery(String sql, Object... args) {
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement(sql)){
            for(int i=0; i<args.length; i++){
                preStmt.setObject(i+1, args[i]);
            }
            return preStmt.executeUpdate();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public int insert(String sql, Object... args) {
        Connection con = dbUtils.getConnection();
        String generatedColumns[] = { "game_id" };
        try(PreparedStatement preStmt=con.prepareStatement(sql, generatedColumns)){
            for(int i=0; i<args.length; i++){
                preStmt.setObject(i+1, args[i]);
            }
            preStmt.executeUpdate();
            try (ResultSet keys = preStmt.getGeneratedKeys()) {
                if(keys.next())
                    return keys.getInt(1);
                return -1;
            }
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public <E> List<E> select(Class<E> classType, String sql, Object... args) {
        Connection con = dbUtils.getConnection();
        List<E> items = new ArrayList<>();
        try (PreparedStatement preStmt = con.prepareStatement(sql)) {
            for (int i = 0; i < args.length; i++) {
                preStmt.setObject(i + 1, args[i]);
            }
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    items.add(ResultDecoder.decode(result, classType));
                }
            } catch (Exception e) {
                throw new RuntimeException(e);
            }
            return items;
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    protected <E> E selectFirst(Class<E> classType, String sql, Object... args) {
        return StreamSupport.stream(
                        select(classType, sql,args).spliterator(), false)
                .findFirst().orElse(null);
    }

    private static final Database instance = new Database(
            new JdbcUtils("jdbc:sqlite:C:/db/pw.sqlite","",""));
    public static Database getInstance(){
        return instance;
    }

}
