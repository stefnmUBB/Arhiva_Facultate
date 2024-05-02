package com.helpinghands.repo;

import com.helpinghands.domain.Post;

import java.util.List;

public interface IPostRepo extends IRepo<Post>{

    List<Post> getNewestPosts();

    public List<Post> getPostsOfVol(int volId);
}
