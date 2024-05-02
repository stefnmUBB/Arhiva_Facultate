package sn.socialnetwork.service;

import sn.socialnetwork.domain.Friendship;
import sn.socialnetwork.domain.Message;
import sn.socialnetwork.domain.User;
import sn.socialnetwork.domain.validators.FriendshipValidator;
import sn.socialnetwork.domain.validators.MessageValidator;
import sn.socialnetwork.domain.validators.UserValidator;
import sn.socialnetwork.repo.*;
import sn.socialnetwork.reports.AbstractReport;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class Network {
    private IService<Long, User> userServ;
    private IService<Long, Friendship> friendshipServ;

    private IService<Long, Message> messageServ;

    public Network(
            IService<Long, User> userServ,
            IService<Long, Friendship> friendshipServ,
            IService<Long, Message> messageServ) {
        this.userServ = userServ;
        this.friendshipServ = friendshipServ;
        this.messageServ = messageServ;
    }

    public AbstractReport addUser(User u) throws EntityAlreadyExistsException {return userServ.add(u);}
    public AbstractReport updateUser(User u) {return userServ.update(u);}
    public AbstractReport removeUser(Long uid) throws EntityIdNotFoundException { return userServ.remove(uid);}

    public AbstractReport addFriendship(Friendship f) throws EntityAlreadyExistsException {
        return friendshipServ.add(f);
    }

    public AbstractReport updateFriendship(Friendship f) {
        return friendshipServ.update(f);
    }
    public AbstractReport removeFriendship(Long fid) throws EntityIdNotFoundException { return friendshipServ.remove(fid);}

    public static Network loadDefaultNetwork() {
        IRepo<Long, User> usersRepo =
                //new UserFileRepo(AppContext.USERS_FILE_NAME, new UserValidator());
                //new DatabaseRepo<>(User.class, new UserValidator());
                new UserDatabaseRepo(new UserValidator());
        IRepo<Long, Friendship> friendshipsRepo =
                //new FriendshipFileRepo(AppContext.FRIENDSHIPS_FILE_NAME, new FriendshipValidator(usersRepo));
                //new DatabaseRepo<>(Friendship.class, new FriendshipValidator(usersRepo));
                new FriendshipDatabaseRepo(new FriendshipValidator(usersRepo));

        IRepo<Long, Message> messageRepo =
                //new MessageFileRepo(AppContext.MESSAGES_FILE_NAME, new MessageValidator());
                new DatabaseRepo<>(Message.class, new MessageValidator(usersRepo));

        FriendshipService friendshipsServ = new FriendshipService(friendshipsRepo);
        UserService usersServ = new UserService(usersRepo, friendshipsServ);
        MessageService messageServ = new MessageService(messageRepo);
        return new Network(usersServ, friendshipsServ, messageServ);
    }

    public Iterable<User> getAllUsers() {
        return userServ.getAll();
    }

    public Iterable<Friendship> getAllFriendships() {
        return friendshipServ.getAll();
    }

    public Iterable<Message> getMessagesFrom(Long authorID) {
        return StreamSupport.stream(messageServ.getAll().spliterator(),false)
                .filter(m->m.getAuthorID().equals(authorID))
                .collect(Collectors.toList());
    }

    public AbstractReport postMessage(Message m) throws EntityAlreadyExistsException {
        return messageServ.add(m);
    }

    public List<Community> getCommunities() {
        List<Community> result = new ArrayList<>();
        getAllUsers().forEach(user->{
            boolean alreadyIn = result.stream()
                    .anyMatch(com->com.contains(user.getId()));

            if(!alreadyIn) {
                Community community = new Community(this, user.getId());
                result.add(community);
            }
        });
        return result;
    }

    public List<User> getFriendsOf(User u) {
        return StreamSupport.stream(getAllFriendships().spliterator(),false)
                .filter(f->!f.isPending() && f.containsUser(u.getId()))
                .map(f->f.getTheOtherOne(u.getId()))
                .map(userServ::getById)
                .collect(Collectors.toList());
    }

    public List<Friendship> getFriendRequestsOf(User u) {
        return StreamSupport.stream(getAllFriendships().spliterator(),false)
                .filter(f->f.isPending() && f.containsUser(u.getId()))
                .collect(Collectors.toList());
    }

    public User getUserById(Long id) {return userServ.getById(id);}

    /**
     * @return the community with the longest path
     */
    public Community mostSociableCommunity() {
        Community result = null;
        int max = 0;
        for(Community c : getCommunities()) {
            int cnt = c.getLongestPathLength();
            if(cnt>max) {
                max = cnt;
                result = c;
            }
        }
        return result;
    }

    public List<Message> getMessagesBetween(Long user1ID, Long user2ID) {
        return StreamSupport.stream(messageServ.getAll().spliterator(), false)
                .filter(m->
                        (Objects.equals(m.getAuthorID(), user1ID)
                      && Objects.equals(m.getReceiverID(), user2ID))
                      ||(Objects.equals(m.getAuthorID(), user2ID)
                      && Objects.equals(m.getReceiverID(), user1ID))
                        ).toList();
    }

    public int communitiesCount() {
        return getCommunities().size();
    }

    public AbstractReport addMessage(Message m) throws EntityAlreadyExistsException {
        return messageServ.add(m);
    }
}
