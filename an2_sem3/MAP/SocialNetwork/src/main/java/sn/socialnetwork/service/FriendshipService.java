package sn.socialnetwork.service;

import sn.socialnetwork.domain.Friendship;
import sn.socialnetwork.repo.EntityAlreadyExistsException;
import sn.socialnetwork.repo.IRepo;
import sn.socialnetwork.reports.AbstractReport;

import java.util.Date;

public class FriendshipService extends Service<Long, Friendship> {

    /**
     * Creates a new sn.socialnetwork.service to handle friendships
     * @param frepo friendship sn.socialnetwork.repo to use
     */
    public FriendshipService(IRepo<Long, Friendship> frepo) {
        super(frepo);
    }

    /**
     * adds new friendship to the sn.socialnetwork.repo
     * @param f friendship to add
     * @return operation report
     * @throws EntityAlreadyExistsException if friendship id is already found in sn.socialnetwork.repo
     */
    public AbstractReport add(Friendship f) throws EntityAlreadyExistsException {
        if(exists(friendship ->
                friendship.containsUser(f.getUserIds()[0])
             && friendship.containsUser(f.getUserIds()[1]))
        )
            throw new EntityAlreadyExistsException();
        return super.add(f);
    }


    public Long generateNewId() {
        return new Date().getTime();
    }
}
