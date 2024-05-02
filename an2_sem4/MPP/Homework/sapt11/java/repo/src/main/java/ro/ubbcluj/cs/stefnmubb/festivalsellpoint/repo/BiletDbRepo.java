package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Bilet;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Properties;
import java.util.stream.StreamSupport;

public class BiletDbRepo extends DatabaseRepoUtils<Integer, Bilet>
    implements  IBiletRepo {

    private static final Logger logger= LogManager.getLogger(BiletDbRepo.class);

    ISpectacolRepo spectacolRepo;
    public BiletDbRepo(Properties props, ISpectacolRepo spectacolRepo) {
        super(props);
        this.spectacolRepo = spectacolRepo;
    }

    @Override
    public Bilet decodeResult(ResultSet result) throws SQLException, EntityRepoException {
        var id = result.getInt("id");
        var numeCumparator = result.getString("numeCumparator");
        var nrLocuri = result.getInt("nrLocuri");
        var spectacolId = result.getInt("spectacol");
        var spectacol = spectacolRepo.getById(spectacolId);
        var bilet = new Bilet(numeCumparator, nrLocuri, spectacol);
        bilet.setId(id);
        return bilet;
    }

    @Override
    public Iterable<Bilet> getBySpectacol(Spectacol spectacol) throws EntityRepoException {
        return select("select * from \"Bilet\" where \"spectacol\"=?", spectacol);
    }

    @Override
    public void add(Bilet bilet) throws EntityRepoException {
        logger.trace("Inserting {}", bilet);

        executeNonQuery("insert into \"Bilet\" (\"numeCumparator\", \"nrLocuri\", \"spectacol\") values (?, ?, ?)",
                bilet.getNumeCumparator(),
                bilet.getNrLocuri(),
                bilet.getSpectacol().getId());

        logger.info("Inserted successfully");
        logger.traceExit();
    }

    @Override
    public void update(Bilet bilet) throws EntityRepoException {
        logger.trace("Updating bilet {}", bilet);
        executeNonQuery("update \"Bilet\" set " +
                "\"numeCumparator\"=?," +
                "\"nrLocuri\"=?," +
                "\"spectacol\"=? where \"id\"=?",
                bilet.getNumeCumparator(),
                bilet.getNrLocuri(),
                bilet.getSpectacol().getId(),
                bilet.getId());
        logger.info("Updated bilet {}", bilet);
        logger.traceExit();
    }

    @Override
    public void remove(Integer id) throws EntityRepoException {
        logger.trace("Removing bilet id={}", id);
        executeNonQuery("delete from \"Bilet\" where id = ?", id);
        logger.info("Removed bilet id={}", id);
        logger.traceExit();
    }

    @Override
    public Bilet getById(Integer id) throws EntityRepoException {
        return selectFirst("select * from \"Bilet\" where \"id\"=?",id);
    }

    @Override
    public Iterable<Bilet> getAll() throws EntityRepoException {
        return select("select * from \"Bilet\"");
    }
}
