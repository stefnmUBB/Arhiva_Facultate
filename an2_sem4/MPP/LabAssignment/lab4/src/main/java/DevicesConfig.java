import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

@Configuration
public class DevicesConfig {
    @Bean
    Properties getProps(){
        Properties props=new Properties();
        try {
            //System.out.println((new File(".")).getAbsolutePath());
            props.load(new FileReader("file.properties"));
            //props.load(new FileReader("bd.properties"));

        } catch (IOException e) {
            System.out.println("Cannot find properties file "+e);
        }
        return props;
    }

    @Bean
    DeviceRepository deviceRepository(){
        //return new DeviceDBRepository(getProps());
        return new DeviceFileRepository(getProps());
    }

    @Bean
    DeviceService deviceService() {
        return new DeviceService(deviceRepository());
    }
}
