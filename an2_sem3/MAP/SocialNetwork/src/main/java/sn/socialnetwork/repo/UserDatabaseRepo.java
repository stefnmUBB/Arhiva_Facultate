package sn.socialnetwork.repo;

import sn.socialnetwork.domain.User;
import sn.socialnetwork.domain.validators.IValidator;

import java.sql.*;

public class UserDatabaseRepo extends AbstractDatabaseRepo<Long, User>{

    public UserDatabaseRepo(IValidator<User> validator) {
        super(validator);
    }

    public UserDatabaseRepo(String url, String username, String password, IValidator<User> validator) {
        super(url, username, password, validator);
    }

    @Override
    protected PreparedStatement getSelectQuery(Connection conn) throws SQLException {
        String sql = "Select * FROM public.\"User\" ORDER BY id ASC";
        return conn.prepareStatement(sql);
    }

    @Override
    protected PreparedStatement getInsertQuery(Connection conn, User user) throws SQLException {
        String[] generatedColumns = { "id" };
        String sql = "INSERT INTO public.\"User\" (\"firstName\", \"lastName\", \"email\", \"password\", \"age\") VALUES (?,?,?,?,?)";
        PreparedStatement ps = conn.prepareStatement(sql, generatedColumns);
        ps.setString(1, user.getFirstName());
        ps.setString(2, user.getLastName());
        ps.setString(3,user.getEmail());
        ps.setString(4,user.getPassword());
        ps.setInt(5, user.getAge());
        return ps;
    }

    @Override
    protected PreparedStatement getUpdateQuery(Connection conn, User user) throws SQLException {
        String sql = "UPDATE public.\"User\" SET " +
                "\"firstName\"=?,  \"lastName\"=?, \"email\"=?, \"password\"=?, \"age\"=?" +
                "WHERE \"id\"=?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setString(1, user.getFirstName());
        ps.setString(2, user.getLastName());
        ps.setString(3,user.getEmail());
        ps.setString(4,user.getPassword());
        ps.setInt(5, user.getAge());
        ps.setLong(6, user.getId());
        return ps;
    }

    @Override
    protected PreparedStatement getRemoveQuery(Connection conn, Long userId) throws SQLException {
        String sql = "DELETE FROM public.\"User\" WHERE \"id\" = ?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setLong(1, userId);
        return ps;
    }

    @Override
    protected PreparedStatement getByIdQuery(Connection conn, Long id) throws SQLException
    {
        String sql = "Select * FROM public.\"User\" WHERE \"id\" = ?";
        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setLong(1, id);
        return ps;
    }

    @Override
    protected User buildEntity(ResultSet result) throws SQLException {
        String firstName = result.getString("firstName");
        String lastName = result.getString("lastName");
        String email = result.getString("email");
        String password = result.getString("password");
        int age = result.getInt("age");
        User user = new User(firstName,lastName,email,password,age);
        Long id = result.getLong("id");
        user.setId(id);
        return user;
    }
}
