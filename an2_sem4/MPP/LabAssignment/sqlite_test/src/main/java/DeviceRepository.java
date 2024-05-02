import java.util.List;

public interface DeviceRepository extends Repository<Integer, Device> {
    List<Device> findByManufacturer(String manufacturer);
    List<Device> findBetweenYears(int min, int max);
}
