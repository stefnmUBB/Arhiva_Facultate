package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.LogManager;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Angajat;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Properties;
import java.util.stream.StreamSupport;

public class AngajatDbRepo extends DatabaseRepoUtils<Integer, Angajat> implements IAngajatRepo {
    private static final Logger logger= LogManager.getLogger(AngajatDbRepo.class);

    public AngajatDbRepo(Properties props){
        super(props);
        logger.info("Initializing AngajatDbRepo with properties: {} ",props);
    }

    @Override
    public Angajat decodeResult(ResultSet result) throws SQLException {
        var id = result.getInt("id");
        var username = result.getString("username");
        var password = result.getString("password");
        var email = result.getString("email");
        var angajat = new Angajat(username, password, email);
        angajat.setId(id);
        return angajat;
    }

    @Override
    public Angajat findByCredentials(String username, String password) throws EntityRepoException {
        return selectFirst("select * from \"Angajat\" where " +
                "\"username\"=? and \"password\"=?"
                ,username, password);
    }

    @Override
    public void add(Angajat angajat) throws EntityRepoException {
        logger.trace("Inserting {}", angajat);

        executeNonQuery("insert into \"Angajat\" (\"username\", \"password\", \"email\") values (?, ?, ?)",
                angajat.getUsername(),
                angajat.getPassword(),
                angajat.getEmail());

        logger.info("Inserted successfully");
        logger.traceExit();
    }

    @Override
    public void update(Angajat angajat) throws EntityRepoException {
        throw new EntityRepoException("Angajat update is not allowed");
    }

    @Override
    public void remove(Integer integer) throws EntityRepoException {
        throw new EntityRepoException("Angajat removal is not allowed");
    }

    @Override
    public Angajat getById(Integer id) throws EntityRepoException {
        return selectFirst("select * from \"Angajat\" where \"id\"=?",id);
    }

    @Override
    public Iterable<Angajat> getAll() throws EntityRepoException {
        return select("select * from \"Angajat\"");
    }
}
