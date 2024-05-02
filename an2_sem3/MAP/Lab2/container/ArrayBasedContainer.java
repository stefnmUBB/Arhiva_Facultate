package container;

import model.Task;

import static utils.Constants.INITIAL_CONTAINER_SIZE;

public abstract class ArrayBasedContainer implements Container {
    protected  Task[] tasks;

    public ArrayBasedContainer() {
        size = 0;
        tasks = new Task[INITIAL_CONTAINER_SIZE];
    }

    @Override
    public void add(Task task) {
        if(tasks.length==size) {
            Task[] t=new Task[tasks.length*2];
            System.arraycopy(tasks, 0, t, 0, tasks.length);
            tasks = t;
        }
        tasks[size] = task;
        size++;
    }

    protected int size;

    @Override
    public int size() {
        return size;
    }

    @Override
    public boolean isEmpty() {
        return size==0;
    }


}
