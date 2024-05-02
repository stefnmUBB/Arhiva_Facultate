package sn.socialnetwork.domain.validators;

import java.util.regex.Pattern;

public class EmailValidator implements  IValidator<String> {
    /**
     * Validates a string as an email.
     *
     * An email address must contain an username composed of alphanumeric characters, underscores or dots,
     * followed by the (at) sign, and the sn.socialnetwork.domain name.
     * @param entity email to validate
     * @throws ValidationException if one of the conditions above is not satisfied
     */
    @Override
    public void validate(String entity) throws ValidationException {
        if(entity==null){
            throw new ValidationException("Email address must not be null");
        }
        if(!Pattern.matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", entity)) {
            throw new ValidationException("Incorrect email address");
        }
    }
}
