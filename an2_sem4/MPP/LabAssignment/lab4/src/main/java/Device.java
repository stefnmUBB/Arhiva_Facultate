public class Device extends Entity<Integer> {
    private String name;
    private String manufacturer;
    private int cpuHz;
    private int year;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getManufacturer() {
        return manufacturer;
    }

    public void setManufacturer(String manufacturer) {
        this.manufacturer = manufacturer;
    }

    public int getCpuHz() {
        return cpuHz;
    }

    public void setCpuHz(int cpuHz) {
        this.cpuHz = cpuHz;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public Device(String name, String manufacturer, int cpuHz, int year) {
        this.name = name;
        this.manufacturer = manufacturer;
        this.cpuHz = cpuHz;
        this.year = year;
    }

    @Override
    public String toString() {
        return "Device{" +
                "name='" + name + '\'' +
                ", manufacturer='" + manufacturer + '\'' +
                ", cpuHz=" + cpuHz +
                ", year=" + year +
                ", id=" + id +
                '}';
    }
}
