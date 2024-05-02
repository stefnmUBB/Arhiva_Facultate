package sorter;

public class MergeSorter extends AbstractSorter {

    public MergeSorter(int[] numbers) {
        super(numbers, SortStrategy.MERGESORT);
    }

    private void sortHelper(int l, int r) {
        if(l>=r) return;
        var numbers = getNumbers();
        int m=(l+r)/2;
        sortHelper(l,m);
        sortHelper(m+1,r);

        int[] tmp = new int[r-l+1];

        int k=0;

        int cnt_left = m-l+1;
        int cnt_right = r-m;

        int i=0, j=0;
        while(i<cnt_left && j<cnt_right) {
            if(numbers[l+i]<numbers[m+1+j]) {
                tmp[k++] = numbers[l+(i++)];
            }
            else {
                tmp[k++] = numbers[m+1+(j++)];
            }
        }

        while(i<cnt_left) tmp[k++] = numbers[l+(i++)];
        while(j<cnt_right) tmp[k++] = numbers[m+1+(j++)];

        System.arraycopy(tmp, 0, numbers, l, r-l+1);
    }

    @Override
    public void sort() {
        sortHelper(0,getNumbers().length-1);
    }
}
