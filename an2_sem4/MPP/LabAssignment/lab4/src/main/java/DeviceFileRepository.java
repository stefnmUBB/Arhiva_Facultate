import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.*;
import java.util.*;
import java.util.stream.Collectors;

public class DeviceFileRepository implements DeviceRepository{
    private static final Logger logger = LogManager.getLogger(DeviceFileRepository.class);

    private final String filePath;

    private final List<Device> devices = new ArrayList<>();

    public DeviceFileRepository(Properties props){
        logger.info("Initializing DeviceFileRepository with properties: {} ",props);
        filePath = props.getProperty("path");
        Load();
    }

    @Override
    public List<Device> findByManufacturer(String manufacturer) {
        return devices.stream().filter(d-> Objects.equals(d.getManufacturer(), manufacturer))
                .collect(Collectors.toList());
    }

    @Override
    public List<Device> findBetweenYears(int min, int max) {
        return devices.stream().filter(d
                -> min <= d.getYear() && d.getYear()<=max )
                .collect(Collectors.toList());
    }

    @Override
    public void add(Device elem) {
        elem.setId(getMaxId()+1);
        devices.add(elem);
        Save();
    }

    Integer getMaxId(){
        return devices.stream().map(Entity::getId)
                .max(Comparator.comparingInt(a -> a)).orElse(0);
    }

    @Override
    public void update(Integer id, Device elem) {
        for(var device:devices){
            if(Objects.equals(device.getId(), id)){
                device.setName(elem.getName());
                device.setManufacturer(elem.getManufacturer());
                device.setYear(elem.getYear());
                device.setCpuHz(elem.getCpuHz());
                device.setId(getMaxId()+1);
                Save();
                return;
            }
        }
    }

    @Override
    public Iterable<Device> findAll() {
        return new ArrayList<>(devices);
    }

    private void Save() {
        try {
            File file = new File(filePath);
            try(FileWriter fw = new FileWriter(file)) {
                for (var device : devices) {
                    fw.write(device.getId()
                        +","+device.getName()
                        +","+device.getManufacturer()
                        +","+device.getYear()
                        +","+device.getCpuHz()
                        +"\n"
                    );
                }
            }
        } catch (IOException e) {
            throw new RuntimeException(e);
        }

    }

    private void Load(){
        try {
            File file = new File(filePath);
            FileReader fr = new FileReader(file);
            BufferedReader br = new BufferedReader(fr);
            String line;
            while ((line = br.readLine()) != null) {
                var attr = line.split(",");
                var id = Integer.parseInt(attr[0]);
                var name = attr[1];
                var manufacturer = attr[2];
                var year = Integer.parseInt(attr[3]);
                var cpuHz = Integer.parseInt(attr[4]);
                var device = new Device(name, manufacturer, cpuHz, year);
                device.setId(id);
                devices.add(device);
            }
            fr.close();
        }
        catch(IOException e) {
            e.printStackTrace();
        }
    }
}
