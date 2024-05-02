package sn.socialnetwork.repo;

import sn.socialnetwork.domain.User;
import sn.socialnetwork.domain.validators.IValidator;

import java.util.List;

public class UserFileRepo extends  FileRepo<Long, User> {
    /**
     * Creates new file sn.socialnetwork.repo for users
     * @param fileName users file name
     * @param validator user validator
     */
    public UserFileRepo(String fileName, IValidator<User> validator) {
        super(fileName, validator);
    }

    @Override
    public User extractEntity(List<String> attributes) {
        Long id = Long.parseLong(attributes.get(0));
        String firstName = attributes.get(1);
        String lastName = attributes.get(2);
        String email = attributes.get(3);
        String password = attributes.get(4);
        Integer age = Integer.parseInt(attributes.get(5));
        User u =new User(firstName, lastName, email, password, age);
        u.setId(id);
        return u;
    }

    @Override
    public String entityAsString(User entity) {
        String[] vals = new String[]
        {
                entity.getId().toString(),
                entity.getFirstName(),
                entity.getLastName(),
                entity.getEmail(),
                entity.getPassword(),
                entity.getAge().toString()
        };
        return String.join(";", vals);
    }
}
