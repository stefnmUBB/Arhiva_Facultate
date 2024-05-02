package sn.socialnetwork.domain;

public class Entity<ID> {
    private ID id;

    /**
     * Retrieves entity Id
     * @return Id of entity
     */

    public ID getId() {
        return id;
    }

    /**
     * Assigns an id to the entity
     * @param id new id of the entity
     */

    public void setId(ID id) {
        this.id = id;
    }

    private boolean deleted = false;

    public boolean isDeleted() {
        return deleted;
    }

    public void setDeleted() {
        deleted = true;
    }

    public void unsetDeleted() {
        deleted = false;
    }
}
