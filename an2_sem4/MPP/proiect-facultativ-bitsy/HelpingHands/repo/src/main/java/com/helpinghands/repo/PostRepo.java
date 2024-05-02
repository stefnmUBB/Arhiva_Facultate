package com.helpinghands.repo;

import com.helpinghands.domain.Post;

import java.util.List;
import java.util.concurrent.atomic.AtomicReference;

public class PostRepo extends AbstractRepo<Post> implements IPostRepo{
    public PostRepo(Class<Post> entityType) {
        super(entityType);
    }
    public PostRepo() {
        super(Post.class);
    }



    @Override
    public List<Post> getNewestPosts() {
        String query = "FROM Post ORDER BY id DESC";

        AtomicReference<List<Post>> result = new AtomicReference<>();
        Session.doTransaction((session, tx) -> {
            result.set(session.createQuery(query, Post.class)
                    .setFirstResult(0)
                    .setMaxResults(10)
                    .getResultList()
            );
            tx.commit();
        });
        return result.get();
    }

    @Override
    public List<Post> getPostsOfVol(int volId) {
        String query = "SELECT p FROM Post AS p INNER JOIN p.eveniment AS ev"
        + " INNER JOIN ev.participants AS part WHERE part.voluntar.id = :volId";

        AtomicReference<List<Post>> result = new AtomicReference<>();
        Session.doTransaction((session, tx) -> {
            result.set(session.createQuery(query, Post.class)
                    .setParameter("volId", volId)
                    .getResultList());
            tx.commit();
        });
        return result.get();
    }
}
