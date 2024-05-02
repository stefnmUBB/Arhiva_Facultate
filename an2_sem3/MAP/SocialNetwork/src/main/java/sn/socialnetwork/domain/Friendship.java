package sn.socialnetwork.domain;

import java.time.LocalDateTime;
import java.util.Arrays;
import java.util.Objects;

import static java.lang.Math.max;
import static java.lang.Math.min;

public class Friendship extends Entity<Long> {
    Long uid1;
    Long uid2;
    LocalDateTime friendsFrom;

    boolean pending = true;
    Long sender = -1L;

    public boolean isSender(Long id) {
        return Objects.equals(id, sender);
    }

    public void setSender(Long sender) {
        this.sender = sender;
    }

    public Long getSender() {
        return sender;
    }

    public Friendship(){}

    /**
     * Creates a new friendship instance
     * @param userId1 one of the friend's Id
     * @param userId2 the other friend's Id
     */
    public Friendship(Long userId1, Long userId2, LocalDateTime friendsFrom) {
        uid1= min(userId1, userId2);
        uid2 = max(userId1, userId2);
        this.friendsFrom = friendsFrom;
    }

    public boolean isPending() {
        return pending;
    }

    public void setPending(boolean pending) {
        this.pending = pending;
    }

    /**
     * Gets users implied in a friendship, sorted by their Ids
     * @return array of two friend users
     */
    public Long[] getUserIds() {
        return new Long[]{uid1, uid2};
    }

    /**
     * get date friendship was made
     * @return date since the two users are friends
     */
    public LocalDateTime getFriendsFrom() {
        return friendsFrom;
    }

    /**
     * Checks if a user is part of the friendship
     * @param id Id of the user to check
     * @return true if user represents one end of the friendship, false otherwise
     */
    public boolean containsUser(Long id) {
        return uid1.equals(id) || uid2.equals(id);
    }

    /**
     * Gets a text representation of the friendship
     *
     * @return String containing user-friendly friendship data
     */
    @Override
    public String toString() {
        return "Friendship{" +
                "uid1=" + uid1 +
                ", uid2=" + uid2 +
                ", friendsFrom=" + friendsFrom +
                ", pending=" + pending +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Friendship that = (Friendship) o;
        return pending == that.pending && uid1.equals(that.uid1) && uid2.equals(that.uid2) && friendsFrom.equals(that.friendsFrom);
    }

    @Override
    public int hashCode() {
        return Objects.hash(uid1, uid2, friendsFrom, pending);
    }

    /**
     * Knowing one of the friends, the other one is retrieved.
     * @param id The id of the known user implied in friendship
     * No checks are performed over the user id. An id of an user
     * that is not part of the friendship can be provided, in which case
     * the function returns any of the two actual friends. However, the function is guaranteed
     * to work when the user exists in a friendship
     * @return the other user in the friendship, or any of the friends if the user is not part of friendship
     */
    public Long getTheOtherOne(Long id) {
        return uid1+uid2-id; // id.equals(uid1) ? uid2 : uid1;
    }
}

