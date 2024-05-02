package sn.socialnetwork.service;

import sn.socialnetwork.domain.Message;
import sn.socialnetwork.repo.IRepo;

import java.util.Date;

public class MessageService extends Service<Long, Message>{

    /**
     * Creates new sn.socialnetwork.service
     *
     * @param repo sn.socialnetwork.repo to handle
     */
    public MessageService(IRepo<Long, Message> repo) {
        super(repo);
    }

    @Override
    public Long generateNewId() {
        return new Date().getTime();
    }
}
