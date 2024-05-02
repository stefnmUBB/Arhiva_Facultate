package factory;

import container.Container;
import container.QueueContainer;
import container.StackContainer;
import container.Strategy;

public class TaskContainerFactory implements Factory {

    private TaskContainerFactory() {
    }

    private static TaskContainerFactory instance = new TaskContainerFactory();
    public static TaskContainerFactory getInstance() {
        return instance;
    }

    @Override
    public Container createContainer(Strategy strategy) {
        switch (strategy){
            case LIFO: return new StackContainer();
            case FIFO:return new QueueContainer();
        }
        return null;
    }
}
