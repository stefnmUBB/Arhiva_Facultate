package com.helpinghands.repo;

import com.helpinghands.domain.Message;

public class MessageRepo extends AbstractRepo<Message> implements IMessageRepo {
    public MessageRepo(Class<Message> entityType) {
        super(entityType);
    }
}
