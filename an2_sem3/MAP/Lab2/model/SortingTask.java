package model;

import sorter.AbstractSorter;
import sorter.BubbleSorter;
import sorter.SortStrategy;
import sorter.SorterFactory;

public class SortingTask extends Task{

    AbstractSorter sorter;

    public SortingTask(String taskId, String description, int[] numbers, SortStrategy strategy) {
        super(taskId, description);
        this.sorter= SorterFactory.getInstance().getSorter(numbers, strategy);
    }

    @Override
    public void execute() {
        sorter.sort();
        for(int x : sorter.getNumbers()){
            System.out.print(x+" ");
        }
        System.out.println("");
    }
}
