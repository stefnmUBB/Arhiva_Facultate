package sn.socialnetwork.repo;

import sn.socialnetwork.domain.Friendship;
import sn.socialnetwork.domain.validators.IValidator;

import java.sql.*;
import java.time.LocalDateTime;

public class FriendshipDatabaseRepo extends AbstractDatabaseRepo<Long, Friendship> {

    public FriendshipDatabaseRepo(IValidator<Friendship> validator) {
        super(validator);
    }

    public FriendshipDatabaseRepo(String url, String username, String password, IValidator<Friendship> validator) {
        super(url, username, password, validator);
    }
    @Override
    protected PreparedStatement getSelectQuery(Connection conn) throws SQLException {
        String sql = "Select * FROM public.\"Friendship\" ORDER BY id ASC";
        return conn.prepareStatement(sql);
    }

    @Override
    protected PreparedStatement getByIdQuery(Connection conn, Long id) throws SQLException
    {
        String sql = "Select * FROM public.\"Friendship\" WHERE \"id\" = ?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setLong(1, id);
        return ps;
    }

    @Override
    protected PreparedStatement getInsertQuery(Connection conn, Friendship friendship) throws SQLException {
        String[] generatedColumns = { "id" };
        String sql = "INSERT INTO public.\"Friendship\" (\"uid1\", \"uid2\", \"friendsFrom\", \"pending\",\"sender\") VALUES (?,?,?,?,?)";
        PreparedStatement ps = conn.prepareStatement(sql, generatedColumns);
        ps.setLong(1, friendship.getUserIds()[0]);
        ps.setLong(2, friendship.getUserIds()[1]);
        ps.setTimestamp(3, Timestamp.valueOf(friendship.getFriendsFrom()));
        ps.setBoolean(4,friendship.isPending());
        ps.setLong(5,friendship.getSender());
        return ps;
    }

    @Override
    protected PreparedStatement getUpdateQuery(Connection conn, Friendship friendship) throws SQLException {
        String sql = "UPDATE public.\"Friendship\" SET " +
                "\"uid1\"=?,  \"uid2\"=?, \"friendsFrom\"=?, \"pending\"=?, \"sender\"=? " +
                "WHERE \"id\"=?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setLong(1, friendship.getUserIds()[0]);
        ps.setLong(2, friendship.getUserIds()[1]);
        ps.setTimestamp(3, Timestamp.valueOf(friendship.getFriendsFrom()));
        ps.setBoolean(4, friendship.isPending());
        ps.setLong(5,friendship.getSender());
        ps.setLong(6, friendship.getId());
        return ps;
    }

    @Override
    protected PreparedStatement getRemoveQuery(Connection conn, Long friendshipId) throws SQLException {
        String sql = "DELETE FROM public.\"Friendship\" WHERE \"id\" = ?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setLong(1, friendshipId);
        return ps;
    }

    @Override
    protected Friendship buildEntity(ResultSet result) throws SQLException {
        Long uid1 = result.getLong("uid1");
        Long uid2 = result.getLong("uid2");
        LocalDateTime friendsFrom = result.getTimestamp("friendsFrom").toLocalDateTime();
        Friendship friendship = new Friendship(uid1, uid2, friendsFrom);
        friendship.setPending(result.getBoolean("pending"));
        friendship.setSender(result.getLong("sender"));
        Long id = result.getLong("id");
        friendship.setId(id);
        return friendship;
    }
}
