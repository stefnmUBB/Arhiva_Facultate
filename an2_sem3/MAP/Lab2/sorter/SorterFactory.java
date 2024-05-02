package sorter;

public final class SorterFactory {
    private SorterFactory(){

    }

    private static final SorterFactory instance = new SorterFactory();

    public static SorterFactory getInstance() {return instance;}

    public AbstractSorter getSorter(int[] numbers, SortStrategy strategy) {
        if(strategy==SortStrategy.MERGESORT) {
            return new MergeSorter(numbers);
        }
        else if(strategy==SortStrategy.BUBBLESORT){
            return new BubbleSorter(numbers);
        }
        return null;
    }
}
