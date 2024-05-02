package container;

import model.Task;

import static utils.Constants.INITIAL_CONTAINER_SIZE;

public class StackContainer extends ArrayBasedContainer {

    public StackContainer() {
        super();
    }

    @Override
    public Task remove() {
        if(!isEmpty()) {
            size--;
            return tasks[size];
        }
        return null;
    }
}
