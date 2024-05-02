package container;

import model.Task;
import utils.Constants;

public class QueueContainer extends ArrayBasedContainer {

    public QueueContainer() {
        super();
    }

    @Override
    public Task remove() {
        if(size==0)
            return null;
        Task target=tasks[0];
        size--;
        for(int i=0;i<size;i++)
            tasks[i]=tasks[i+1];

        return target;
    }
}
