package com.helpinghands.repo;

import com.helpinghands.domain.ChatRoom;

public class ChatRoomRepo extends AbstractRepo<ChatRoom> implements IChatRoomRepo {
    public ChatRoomRepo(Class<ChatRoom> entityType) {
        super(entityType);
    }
}
