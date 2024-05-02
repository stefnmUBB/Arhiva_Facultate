package com.helpinghands.repo;

import com.helpinghands.domain.ProfilePic;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public class ProfilePicRepo extends AbstractRepo<ProfilePic> implements IProfilePicRepo {
    private final static Logger logger = LogManager.getLogger(ProfilePicRepo.class);
    private String profilePicPath;
    public ProfilePicRepo(String ppPath) throws IOException {
        super(ProfilePic.class);
        this.profilePicPath = ppPath;
        new File(profilePicPath).mkdirs();
    }

    @Override
    public void add(int userId, byte[] img) throws IOException {
        logger.trace("");
        String path= Path.of(profilePicPath, userId+".jpg").toString();
        logger.info("PP path = "+path);
        File outputFile = new File(path);
        logger.info("Writing");
        try (var outputStream = new FileOutputStream(outputFile)) {
            outputStream.write(img);
        }
        logger.info("Updating");
        var pp=new ProfilePic(path);
        pp.setId(userId);

        if(getById(userId)==null)
            add(pp);
        else
            update(pp);
        logger.traceExit();
    }

    private final byte[] defaultImage = ProfilePicRepo.class.getClassLoader()
            .getResourceAsStream("default_pic.jpg").readAllBytes();

    @Override
    public byte[] getImage(int userId) throws IOException {
        var pp = getById(userId);
        if(pp==null){
            return defaultImage;
        }
        return Files.readAllBytes(Path.of(pp.getPath()));
    }
}
