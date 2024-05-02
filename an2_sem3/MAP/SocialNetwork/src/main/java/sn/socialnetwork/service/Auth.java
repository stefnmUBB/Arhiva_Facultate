package sn.socialnetwork.service;

import sn.socialnetwork.domain.User;

import java.util.Objects;
import java.util.Optional;
import java.util.stream.Stream;
import java.util.stream.StreamSupport;

public class Auth {
    private Network network;

    public Auth(Network network) {
        this.network = network;
    }

    public User login(String email, String password) {
        Optional<User> user = StreamSupport
                .stream(network.getAllUsers().spliterator(),false)
                .filter(u->Objects.equals(u.getEmail(), email)
                        && Objects.equals(u.getPassword(),password)
                ).findFirst();
        if(user.isEmpty()) return null;
        return user.get();
    }
}
