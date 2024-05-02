package sn.socialnetwork.domain.validators;

import sn.socialnetwork.domain.User;

public class UserValidator implements  IValidator<User> {

    /**
     * Validates a User instance based on its attributes.
     * In order to be valid, a user:
     * - must not be null
     * - must have valid name and surname
     * - must be at least 14, but to older than 100 years
     * - must have a valid email address
     * @param entity user to validate
     * @throws ValidationException if one of the validation conditions fail
     */
    @Override
    public void validate(User entity) throws ValidationException {
        if(entity==null) {
            throw new ValidationException("User must not be null");
        }
        new NameValidator().validate(entity.getFirstName());
        new NameValidator().validate(entity.getLastName());

        if(entity.getAge()<14) {
            throw new ValidationException("User too young for this platform");
        }
        if(entity.getAge()>100) {
            throw new ValidationException("Is this user Iliescu?");
        }

        new EmailValidator().validate(entity.getEmail());
    }
}
