import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class DeviceDBRepository implements DeviceRepository {
    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger(DeviceRepository.class);

    public DeviceDBRepository(Properties props){
        logger.info("Initializing CarsDBRepository with properties: {} ",props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public List<Device> findByManufacturer(String sManufacturer) {
        logger.traceEntry();
        Connection con=dbUtils.getConnection();
        List<Device> devices=new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Devices where manufacturer=?")){
            preStmt.setString(1, sManufacturer);
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id=result.getInt("id");
                    String name=result.getString("name");
                    String manufacturer = result.getString("manufacturer");
                    int cpuHz=result.getInt("cpuHz");
                    int year=result.getInt("year");

                    Device device=new Device(name, manufacturer, cpuHz, year);
                    device.setId(id);
                    devices.add(device);
                }
            }

        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
        }
        logger.traceExit();
        return devices;
    }

    @Override
    public List<Device> findBetweenYears(int min, int max) {
        logger.traceEntry();
        Connection con=dbUtils.getConnection();
        List<Device> devices=new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Devices where year between ? and ?")){
            preStmt.setInt(1, min);
            preStmt.setInt(2, max);
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id=result.getInt("id");
                    String name=result.getString("name");
                    String manufacturer = result.getString("manufacturer");
                    int cpuHz=result.getInt("cpuHz");
                    int year=result.getInt("year");

                    Device device=new Device(name, manufacturer, cpuHz, year);
                    device.setId(id);
                    devices.add(device);
                }
            }

        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
        }
        logger.traceExit();
        return devices;
    }

    @Override
    public void add(Device elem) {
        logger.traceEntry("saving task {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("insert into Devices (name, manufacturer, cpuHz, year) values (?,?,?,?)")){
            preStmt.setString(1, elem.getName());
            preStmt.setString(2, elem.getManufacturer());
            preStmt.setInt(3, elem.getCpuHz());
            preStmt.setInt(4, elem.getYear());

            int result=preStmt.executeUpdate();
            logger.trace("Saved {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Integer id, Device elem) {
        logger.traceEntry("updating task {}", elem);
        Connection con = dbUtils.getConnection();
        try(PreparedStatement preStmt=con.prepareStatement("update Devices set name=?, manufacturer=?, cpuHz=?, year=? where id=?")){
            preStmt.setString(1, elem.getName());
            preStmt.setString(2, elem.getManufacturer());
            preStmt.setInt(3, elem.getCpuHz());
            preStmt.setInt(4, elem.getYear());
            preStmt.setInt(5, id);

            int result=preStmt.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
        }
        logger.traceExit();
    }

    @Override
    public Iterable<Device> findAll() {
        logger.traceEntry();
        Connection con=dbUtils.getConnection();
        List<Device> devices=new ArrayList<>();
        try(PreparedStatement preStmt=con.prepareStatement("select * from Devices")){
            try(ResultSet result = preStmt.executeQuery()){
                while(result.next()){
                    int id=result.getInt("id");
                    String name=result.getString("name");
                    String manufacturer = result.getString("manufacturer");
                    int cpuHz=result.getInt("cpuHz");
                    int year=result.getInt("year");

                    Device device=new Device(name, manufacturer, cpuHz, year);
                    device.setId(id);
                    devices.add(device);
                }
            }

        } catch (SQLException e) {
            logger.error(e);
            System.err.println("Error DB "+ e);
        }
        logger.traceExit();
        return devices;
    }
}
