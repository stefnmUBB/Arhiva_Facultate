package sn.socialnetwork.repo;

import sn.socialnetwork.domain.Friendship;
import sn.socialnetwork.domain.validators.IValidator;
import sn.socialnetwork.utils.Constants;

import java.time.LocalDateTime;
import java.util.List;

public class FriendshipFileRepo extends FileRepo<Long, Friendship> {
    /**
     * Creates new file sn.socialnetwork.repo for friendship
     * @param fileName friendships file name
     * @param validator friendship validator
     */
    public FriendshipFileRepo(String fileName, IValidator<Friendship> validator) {
        super(fileName, validator);
    }

    @Override
    public Friendship extractEntity(List<String> attributes) {
        Long id = Long.parseLong(attributes.get(0));
        Long uid1 = Long.parseLong(attributes.get(1));
        Long uid2 = Long.parseLong(attributes.get(2));
        LocalDateTime friendsFrom = LocalDateTime.parse(attributes.get(3),Constants.DATE_TIME_FORMATTER);
        Friendship f = new Friendship(uid1, uid2, friendsFrom);
        f.setId(id);
        return f;
    }

    @Override
    public String entityAsString(Friendship entity) {
        String[] vals = new String[]
        {
                entity.getId().toString(),
                entity.getUserIds()[0].toString(),
                entity.getUserIds()[1].toString(),
                entity.getFriendsFrom().format(Constants.DATE_TIME_FORMATTER)
        };
        return String.join(";", vals);
    }
}
