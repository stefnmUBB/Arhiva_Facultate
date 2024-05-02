package com.helpinghands.repo;

import com.helpinghands.domain.Admin;
import com.helpinghands.domain.UserSession;
import com.helpinghands.domain.Utilizator;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.sqlite.hibernate.dialect.SQLiteMetadataBuilderInitializer;

import java.util.concurrent.atomic.AtomicReference;

public class UserSessionRepo extends AbstractRepo<UserSession> implements IUserSessionRepo {
    private final static Logger logger = LogManager.getLogger(UserSessionRepo.class);
    public UserSessionRepo() {
        super(UserSession.class);
    }

    private static String padLeftZeros(String inputString, int length) {
        if (inputString.length() >= length) {
            return inputString;
        }
        StringBuilder sb = new StringBuilder();
        while (sb.length() < length - inputString.length()) {
            sb.append('0');
        }
        sb.append(inputString);

        return sb.toString();
    }

    private String generateToken(Utilizator user){
        String timestamp =Long.toHexString(System.currentTimeMillis());
        String userId = Integer.toHexString(user.getId());
        return padLeftZeros(timestamp, 8)+padLeftZeros(userId,4);
    }

    @Override
    public String createToken(Utilizator user) {
        logger.trace("");
        logger.info("Creating token for {}", user);
        var token = generateToken(user);
        var type = user instanceof Admin ? "Admin" : "Voluntar";
        var session = new UserSession(token, user, type);
        if(add(session)==null) {
            logger.info("Session add fail");
            logger.traceExit();
            return null;
        }
        logger.info("Token created successfully");
        logger.traceExit();
        return token;
    }

    @Override
    public UserSession findByToken(String token){
        logger.trace("");
        logger.info("Finding by token {}", token);
        AtomicReference<UserSession> result=new AtomicReference<>();
        Session.doTransaction((session, tx)->{
            result.set(session
                    .createQuery("from UserSession where token=:token", UserSession.class)
                    .setParameter("token", token)
                    .setMaxResults(1)
                    .uniqueResult());
            tx.commit();
        });
        logger.info("Ok");
        logger.traceExit();
        return result.get();
    }

    @Override
    public void close(String token) {
        logger.trace("");
        logger.info("Closing {}", token);
        var session = findByToken(token);
        if(session==null) {
            logger.info("Close fail");
            logger.traceExit();
            return;
        }

        logger.info("Ok.. removing session");
        remove(session);
        logger.info("Done.");
        logger.traceExit();
    }

    @Override
    public String getRole(String token) {
        var session = findByToken(token);
        if(session==null)
            return null;
        return session.getType();
    }
}
