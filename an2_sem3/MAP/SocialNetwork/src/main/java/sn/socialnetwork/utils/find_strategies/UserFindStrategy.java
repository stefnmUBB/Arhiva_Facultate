package sn.socialnetwork.utils.find_strategies;

import sn.socialnetwork.domain.User;

public abstract class UserFindStrategy {
    public abstract boolean matches(User u);
}
