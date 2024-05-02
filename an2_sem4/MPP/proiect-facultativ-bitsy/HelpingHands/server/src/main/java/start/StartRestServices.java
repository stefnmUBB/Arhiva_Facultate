package start;

import com.helpinghands.repo.*;
import com.helpinghands.service.AppService;
import com.helpinghands.service.IService;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.*;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

@ComponentScan({"com.helpinghands","com.helpinghands.repo"})
@ComponentScan(basePackages = "com.helpinghands.rest_services")
@Configuration
@SpringBootApplication
public class StartRestServices {
    public static void main(String[] args) throws IOException {
        InputStream inputStream=null;
        try {
            Properties prop = new Properties();
            String propFileName = "config.properties";
            inputStream = StartRestServices.class.getClassLoader().getResourceAsStream(propFileName);
            if (inputStream != null) {
                prop.load(inputStream);
            } else {
                throw new FileNotFoundException("property file '" + propFileName + "' not found in the classpath");
            }
            String path = prop.getProperty("pp_path");
            System.out.println(path);

            profilePicRepo=new ProfilePicRepo(path);

            System.out.println(new File(".").getAbsolutePath());
            SpringApplication.run(StartRestServices.class, args);

        } catch (Exception e) {
            throw new RuntimeException(e);
        } finally {
            inputStream.close();
        }
    }

    static IInterestRepo interestRepo = new InterestRepo();
    static IVoluntarRepo voluntarRepo = new VoluntarRepo();
    static IEvenimentRepo eventRepo = new EvenimentRepo();
    static IParticipantRepo partRepo = new ParticipantRepo();
    static IUserSessionRepo userSessionRepo = new UserSessionRepo();
    static IAdminRepo adminRepo = new AdminRepo();
    static ICerereSponsorRepo cerereSponsorRepo = new CerereSponsorRepo();
    static ISponsorTypeRepo sponsorTypeRepo = new SponsorTypesRepo();
    static IPostRepo postRepo = new PostRepo();
    static IProfilePicRepo profilePicRepo;

    @Bean
    static IService getService() {
        return new AppService(adminRepo, cerereSponsorRepo, null, eventRepo, interestRepo, null, null, partRepo, postRepo, voluntarRepo, userSessionRepo, sponsorTypeRepo, profilePicRepo);
    }
}
