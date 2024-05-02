import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

public class MainSpring {
    static DeviceService getServiceFromXML(){
        ApplicationContext context
                = new ClassPathXmlApplicationContext("DevicesConfig.xml");
        return context.getBean(DeviceService.class);
    }

    static DeviceService getServiceFromConfig(){
        ApplicationContext context
                = new AnnotationConfigApplicationContext(DevicesConfig.class);
        return context.getBean(DeviceService.class);
    }

    public static void main(String[] args){
        //var service = getServiceFromXML();
        var service = getServiceFromConfig();
        System.out.println("\nDevices:");
        service.findAll().forEach(System.out::println);
    }
}
