package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Entity;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;
import java.util.stream.StreamSupport;

public abstract class DatabaseRepoUtils<ID, E extends Entity<ID>> {
    private final JdbcUtils dbUtils;
    private static final Logger logger= LogManager.getLogger(DatabaseRepoUtils.class);
    protected DatabaseRepoUtils(Properties props) {
        dbUtils=new JdbcUtils(props);
    }

    protected abstract E decodeResult(ResultSet result) throws SQLException,
            EntityRepoException;

    protected Iterable<E> select(String sql, Object... args) throws EntityRepoException {
        logger.traceEntry();
        logger.trace("Query: {}", sql);
        Connection con=dbUtils.getConnection();
        List<E> items = new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement(sql)){
            for(int i=0;i<args.length;i++) {
                preStmt.setObject(i+1, args[i]);
            }
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    items.add(decodeResult(result));
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
            throw new EntityRepoException(e);
        }
        logger.trace("Select query returned {} items", items.size());
        logger.traceExit();
        return items;
    }

    protected void executeNonQuery(String sql, Object... args) throws EntityRepoException {
        logger.traceEntry("executing query");
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement(sql)){
            for(int i=0; i<args.length; i++){
                preStmt.setObject(i+1, args[i]);
            }
            int result=preStmt.executeUpdate();
            logger.trace("Excuted query on {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
            throw new EntityRepoException(e);
        }
        logger.traceExit();
    }

    protected E selectFirst(String sql, Object... args) throws EntityRepoException {
        return StreamSupport.stream(
                        select(sql,args).spliterator(),
                        false)
                .findFirst().orElse(null);
    }

}
