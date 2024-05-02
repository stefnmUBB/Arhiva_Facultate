public interface Service<ID,E extends Entity<ID>> {
    void add(E device);
    Iterable<Device> findAll();
}
