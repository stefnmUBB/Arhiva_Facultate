package sorter;

public abstract class AbstractSorter {
    public AbstractSorter(int[] numbers, SortStrategy strategy) {
        this.numbers = numbers;
        this.strategy = strategy;
    }

    public int[] getNumbers() {
        return numbers;
    }

    private int[] numbers;
    private SortStrategy strategy;


    public SortStrategy getStrategy() {
        return strategy;
    }

    public abstract void sort();
}
