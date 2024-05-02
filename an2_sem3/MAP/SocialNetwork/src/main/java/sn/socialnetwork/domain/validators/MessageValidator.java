package sn.socialnetwork.domain.validators;

import sn.socialnetwork.domain.Message;
import sn.socialnetwork.domain.User;
import sn.socialnetwork.repo.IRepo;

public class MessageValidator implements IValidator<Message> {
    IRepo<Long, User> usersRepo;

    public MessageValidator(IRepo<Long, User> usersRepo) {
        this.usersRepo = usersRepo;
    }

    @Override
    public void validate(Message entity) throws ValidationException {
        String content = entity.getContent();
        if(content==null) {
            throw new ValidationException("Message content cannot be null");
        }
        if(content.length()<=3) {
            throw new ValidationException("Message content too short");
        }
        if(!content.matches("^[a-zA-Z0-9.\\-! ]*$")){
            throw new ValidationException("Invalid characters detected in message content");
        }
        if (usersRepo.getById(entity.getAuthorID()) == null) {
            throw new ValidationException("User author id does not exist.");
        }
    }
}
