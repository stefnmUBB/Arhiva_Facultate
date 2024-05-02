package sorter;

public class BubbleSorter extends AbstractSorter {

    public BubbleSorter(int[] numbers) {
        super(numbers, SortStrategy.BUBBLESORT);
    }

    @Override
    public void sort() {
        var numbers = getNumbers();

        boolean found  = false;

        do {
            found = false;
            for(int i=0;i<numbers.length-1;i++) {
                if(numbers[i]>numbers[i+1]) {
                    found = true;
                    var tmp = numbers[i];
                    numbers[i] = numbers[i+1];
                    numbers[i+1] = tmp;
                }
            }
        } while(found);
    }
}
