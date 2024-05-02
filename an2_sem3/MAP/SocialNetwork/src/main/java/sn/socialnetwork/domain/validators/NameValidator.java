package sn.socialnetwork.domain.validators;

import java.util.Objects;
import java.util.regex.Pattern;

public class NameValidator implements IValidator<String> {

    /**
     * Validates a name.
     * Conditions for a name to be considered valid:
     * - is not null
     * - is not empty
     * - does not contain multiple spaces (e.g "John    Smith")
     * - can contain multiple individual names, of at least 3 characters,
     *   but not longer than 100 characters, and each individual name should
     *   start with a capital letter
     * - up to 5 inidivual names are accepted
     * @param entity name to validate
     * @throws ValidationException if one of the conditions above is not fulfilled
     */
    @Override
    public void validate(String entity) throws ValidationException {
        if(entity==null) {
            throw new ValidationException("User name must not be null");
        }

        if(Objects.equals(entity, "")) {
            throw new ValidationException("Name must not be empty");
        }

        if(entity.matches(".*\\s\\s+.*")){
            throw new ValidationException("Multiple spaces detected");
        }

        String[] words = entity.split("\\s");
        if(words.length>5) {
            throw new ValidationException("Are you Spanish? Piccaso?");
        }
        for(String word : words) {
            if(!Pattern.matches("[A-Z](([a-z]+)|(\\.))", word)) {
                throw new ValidationException("Incorrect name : '" + word +"' in '"+ entity +"'");
            }
            if(word.length()<3) {
                throw new ValidationException("Name too short : '" + word +"' in '"+ entity +"'");
            }
            if(word.length()>100) {
                throw new ValidationException("Name too long : '" + word +"' in '"+ entity +"'");
            }
        }
    }
}
