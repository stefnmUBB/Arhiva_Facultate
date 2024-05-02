package sn.socialnetwork.domain.validators;

import sn.socialnetwork.domain.Friendship;
import sn.socialnetwork.domain.User;
import sn.socialnetwork.repo.IRepo;

public class FriendshipValidator implements IValidator<Friendship> {

    IRepo<Long, User> context;

    /**
     * Creates a new friendship validator instance based on a sn.socialnetwork.repo as validation context
     * @param context the users sn.socialnetwork.repo in which the friendship should be valid
     */
    public FriendshipValidator(IRepo<Long, User> context) {
        this.context = context;
    }

    /**
     * Validates a friendship in the specified context
     * The partners of a friendship must be different and must all exist in the users sn.socialnetwork.repo.
     * @param entity entity to validate
     * @throws ValidationException if the two parts of the friendship are identical, or at least
     * one of the participants does not exist in the context
     */
    @Override
    public void validate(Friendship entity) throws ValidationException {
        Long id1 = entity.getUserIds()[0];
        Long id2 = entity.getUserIds()[1];
        if(id1.equals(id2)){
            throw new ValidationException("User can't be friends with themselves");
        }
        if(context.getById(id1)==null) {
            throw new ValidationException("Can't find user with id " + id1);
        }
        if(context.getById(id2)==null) {
            throw new ValidationException("Can't find user with id " + id2);
        }
    }
}
