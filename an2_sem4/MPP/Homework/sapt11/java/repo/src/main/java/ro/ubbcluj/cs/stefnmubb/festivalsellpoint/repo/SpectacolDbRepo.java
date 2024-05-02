package ro.ubbcluj.cs.stefnmubb.festivalsellpoint.repo;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import ro.ubbcluj.cs.stefnmubb.festivalsellpoint.domain.Spectacol;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.time.LocalDateTime;
import java.util.Properties;
import java.util.stream.StreamSupport;

public class SpectacolDbRepo extends DatabaseRepoUtils<Integer, Spectacol>
    implements ISpectacolRepo{
    private static final Logger logger= LogManager.getLogger(SpectacolDbRepo.class);

    public SpectacolDbRepo(Properties props) {
        super(props);
        logger.info("Initializing SpectacolDbRepo with properties: {} ",props);
    }

    @Override
    public Spectacol decodeResult(ResultSet result) throws SQLException {
        var artist=result.getString("artist");
        var data = result.getTimestamp("data").toLocalDateTime();
        var locatie = result.getString("locatie");
        var nrLocuriDisponibile = result.getInt("nrLocuriDisponibile");
        var nrLocuriVandute = result.getInt("nrLocuriVandute");
        var id = result.getInt("id");
        var spectacol = new Spectacol(artist, data, locatie, nrLocuriDisponibile, nrLocuriVandute);
        spectacol.setId(id);
        return spectacol;
    }

    @Override
    public void add(Spectacol spectacol) throws EntityRepoException {
        logger.trace("Inserting {}", spectacol);
        var id = executeInsert("insert into \"Spectacol\"" +
                "(\"artist\", \"data\", \"locatie\", \"nrLocuriDisponibile\", \"nrLocuriVandute\")" +
                "values (?, ?, ?, ?, ?)",
                spectacol.getArtist(),
                spectacol.getData(),
                spectacol.getLocatie(),
                spectacol.getNrLocuriDisponibile(),
                spectacol.getNrLocuriVandute());
        spectacol.setId(id);
        logger.info("Inserted successfully");
        logger.traceExit();
    }

    @Override
    public void update(Spectacol spectacol) throws EntityRepoException {
        var sql = "update \"Spectacol\" set " +
                "artist = ?, " +
                "data = ?, " +
                "locatie = ?, " +
                "\"nrLocuriDisponibile\" = ?," +
                "\"nrLocuriVandute\" = ? " +
                "where id = ?";
        logger.trace("Updating spectacol {}", spectacol);
        executeNonQuery(sql,
                spectacol.getArtist(),
                spectacol.getData(),
                spectacol.getLocatie(),
                spectacol.getNrLocuriDisponibile(),
                spectacol.getNrLocuriVandute(),
                spectacol.getId());
        logger.info("Updated spectacol {}", spectacol);
        logger.traceExit();
    }

    @Override
    public void remove(Integer integer) throws EntityRepoException {
        executeNonQuery("DELETE FROM \"Spectacol\" WHERE \"id\"=?", integer);
        //throw new EntityRepoException("Spectacol removal is not allowed");
    }

    @Override
    public Spectacol getById(Integer id) throws EntityRepoException {
        return selectFirst("select * from \"Spectacol\" where \"id\"=?",id);
    }

    @Override
    public Iterable<Spectacol> getAll() throws EntityRepoException {
        return select("select * from \"Spectacol\"");
    }

    @Override
    public Iterable<Spectacol> getBetweenDates(LocalDateTime start, LocalDateTime end) throws EntityRepoException {
        return select("select * from \"Spectacol\" where \"data\" between ? and ?",
                start,
                end);
    }
}
