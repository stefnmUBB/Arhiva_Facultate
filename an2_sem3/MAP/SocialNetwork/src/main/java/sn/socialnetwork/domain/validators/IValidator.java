package sn.socialnetwork.domain.validators;

public interface IValidator<T> {
    /**
     * Validates an entity
     * @param entity entity to validate
     * @throws ValidationException if validation fails, an exception throws with a message explaining why it fails
     */
    void validate(T entity) throws ValidationException;
}
