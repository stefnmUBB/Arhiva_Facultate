public class DeviceService implements  Service<Integer, Device> {
    private DeviceRepository deviceRepo;

    public DeviceService(DeviceRepository deviceRepo) {
        this.deviceRepo = deviceRepo;
    }

    public void add(Device device){
        deviceRepo.add(device);
    }

    public Iterable<Device> findAll(){
        return deviceRepo.findAll();
    }
}
