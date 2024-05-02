package com.helpinghands.repo;

import com.helpinghands.domain.Notification;

public class NotificareRepo extends AbstractRepo<Notification> implements INotificareRepo {
    public NotificareRepo(Class<Notification> entityType) {
        super(entityType);
    }
}
