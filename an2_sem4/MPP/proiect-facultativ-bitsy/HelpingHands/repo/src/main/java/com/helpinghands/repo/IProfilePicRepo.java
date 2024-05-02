package com.helpinghands.repo;

import com.helpinghands.domain.ProfilePic;

import java.io.IOException;

public interface IProfilePicRepo extends IRepo<ProfilePic> {
    byte[] getImage(int userId) throws IOException;
    void add(int userId, byte[] img) throws IOException;
}
