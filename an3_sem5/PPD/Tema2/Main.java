package org.example;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.*;
import java.util.concurrent.BrokenBarrierException;
import java.util.concurrent.CyclicBarrier;
import java.util.stream.Collectors;
import java.util.stream.Stream;
import java.util.stream.StreamSupport;

public class Main {
    public static void main(String[] args) throws Exception {
        //generateMatrix();
        int n=3, m=3;
        var p = getIntArg(args, 0, 0);
        int N = getIntArg(args, 1, 10);
        int M = getIntArg(args, 2, 10);

        var ints = readInts("D:\\date2.txt", 25+N*M);
        var K = getMatrix(n,m,ints,0);
        var A = getMatrix(N,M,ints, 25);

        System.out.println("p = "+p+(p==0?" Sequencial":" Parallel"));
        System.out.println("N = "+N);
        System.out.println("M = "+M);


        if(p==0)
            measure(()-> convolveInPlace(K, A));
        else
            measure(() -> runThreads(p,K,A));

        //System.out.println(A);
        writeToFile("test_result.out", A);
    }

    static void runThreads(int p, Matrix K, Matrix A){
        var b=new CyclicBarrier(p);
        var threads = IntervalSplitter.split(A.n, p)
                .map(i->new LinesThread(K,A,i.start,i.end, b)).collect(Collectors.toList());
        for(var t :threads) t.start();
        for(var t:threads) {
            try {
                t.join();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }

    static class LinesThread extends Thread {
        Matrix K, A;
        int i0, i1;
        CyclicBarrier barrier;

        public LinesThread(Matrix K, Matrix A, int i0, int i1, CyclicBarrier barrier) {
            this.K = K;
            this.A = A;
            this.i0 = i0;
            this.i1 = i1;
            this.barrier=barrier;
            //System.out.println("T "+i0+" "+i1);
        }

        public void run() {
            var prevLine = new int[A.m];
            var crtLine = new int[A.m];
            var lastBorder = new int[A.m];
            A.copyLine(clamp(i1, 0, A.n - 1), lastBorder);
            for (int j = 0, _i = clamp(i0 - 1, 0, A.n - 1); j < A.m; j++) prevLine[j] = A.get(_i, j);

            try {
                barrier.await();
            } catch (InterruptedException | BrokenBarrierException e) {
                throw new RuntimeException(e);
            }

            for (int i = i0; i < i1; i++) {
                A.copyLine(i, crtLine);
                for (int j = 0; j < A.m; j++) {
                    var lk0 = K.get(0, 0) * prevLine[clamp(j - 1, 0, A.m - 1)]
                            + K.get(0, 1) * prevLine[clamp(j, 0, A.m - 1)]
                            + K.get(0, 2) * prevLine[clamp(j + 1, 0, A.m - 1)];
                    var lk1 = K.get(1, 0) * crtLine[clamp(j - 1, 0, A.m - 1)]
                            + K.get(1, 1) * crtLine[clamp(j, 0, A.m - 1)]
                            + K.get(1, 2) * crtLine[clamp(j + 1, 0, A.m - 1)];
                    int lk2 = 0;
                    if (i == i1-1) {
                        lk2 = K.get(2, 0) * lastBorder[clamp(j - 1, 0, A.m - 1)]
                                + K.get(2, 1) * lastBorder[clamp(j, 0, A.m - 1)]
                                + K.get(2, 2) * lastBorder[clamp(j + 1, 0, A.m - 1)];
                    } else {
                        var ni = clamp(i + 1, 0, A.n - 1);
                        lk2 = K.get(2, 0) * A.get(ni, clamp(j - 1, 0, A.m - 1))
                                + K.get(2, 1) * A.get(ni, clamp(j, 0, A.m - 1))
                                + K.get(2, 2) * A.get(ni, clamp(j + 1, 0, A.m - 1));
                    }
                    A.set(i, j, lk0 + lk1 + lk2);
                }
                System.arraycopy(crtLine, 0, prevLine, 0, A.m);
            }
        }
    }


    static void convolveInPlace(Matrix K, Matrix A){
        var prevLine = new int[A.m];
        var crtLine = new int[A.m];

        var lastBorder=new int[A.m];
        A.copyLine(A.n-1, lastBorder);


        for(int j=0;j<A.m;j++) prevLine[j]=A.get(0,j); // -1, j

        for(int i=0;i<A.n;i++){
            A.copyLine(i, crtLine);
            for(int j=0;j<A.m;j++){
                var lk0 = K.get(0,0)*prevLine[clamp(j-1, 0, A.m-1)]
                        + K.get(0,1)*prevLine[clamp(j,0,A.m-1)]
                        + K.get(0, 2)*prevLine[clamp(j+1,0,A.m-1)];
                var lk1 = K.get(1,0)*crtLine[clamp(j-1, 0, A.m-1)]
                        + K.get(1,1)*crtLine[clamp(j,0,A.m-1)]
                        + K.get(1, 2)*crtLine[clamp(j+1,0,A.m-1)];
                int lk2=0;
                if(i==A.n-1){
                    lk2 = K.get(2, 0) * lastBorder[clamp(j - 1, 0, A.m - 1)]
                        + K.get(2, 1) * lastBorder[clamp(j, 0, A.m - 1)]
                        + K.get(2, 2) * lastBorder[clamp(j + 1, 0, A.m - 1)];
                }
                else {
                    var ni = clamp(i+1,0,A.n-1);
                    lk2 = K.get(2, 0) * A.get(ni, clamp(j - 1, 0, A.m - 1))
                        + K.get(2, 1) * A.get(ni, clamp(j, 0, A.m - 1))
                        + K.get(2, 2) * A.get(ni, clamp(j + 1, 0, A.m - 1));
                }
                A.set(i,j, lk0+lk1+lk2);
            }
            System.arraycopy(crtLine, 0, prevLine, 0, A.m);
        }
    }

    static int getIntArg(String[] args, int i, int defaultValue){
        if(i>=args.length || i<0) return defaultValue;
        return Integer.parseInt(args[i]);
    }
    static Matrix getMatrix(int n, int m, int[] values, int startIndex){
        var a=new Matrix(n,m);
        System.arraycopy(values, startIndex, a.items, 0, n * m);
        return a;
    }

    static int[] readInts(String path, int lim) throws FileNotFoundException {
        System.out.println("Reading ints");
        int[] res = new int[100000000 + 25];
        try(Scanner scanner = new Scanner(new File(path))) {
            int i = 0;
            while (scanner.hasNextInt() && i < lim)
                res[i++] = scanner.nextInt();
            System.out.println("Read " + i + " values");
            return res;
        }
    }

    static int clamp(int x, int a, int b){return x<=a?a:x>=b?b:x;}

    static class Matrix{
        public final int n;
        public final int m;
        public final int[] items;
        public Matrix(int n, int m) {
            this.n=n;
            this.m=m;
            this.items = new int[n*m];
        }
        public void copyLine(int i, int[] dest){
            System.arraycopy(items, m * i, dest, 0, m);
        }

        public int get(int i, int j){return items[m*i+j];}
        public void set(int i, int j, int value){items[m*i+j]=value;}

        public Boolean equals(Matrix x){
            if(x.n!=n || x.m!=m) return false;
            return Arrays.equals(x.items, items);
        }

        @Override
        public String toString() {
            StringBuilder sb=new StringBuilder();
            sb.append(n).append(" ").append(m).append("\n");
            for(int i=0;i<n;i++){
                for(int j=0;j<m;j++)
                    sb.append(items[i*m+j]).append(" ");
                sb.append("\n");
            }
            return sb.toString();
        }
    }

    static void writeToFile(String path, Matrix m) throws IOException {
        FileWriter w = new FileWriter(path);

        w.write(m.n+" "+m.m+"\n");
        for(int i=0;i<m.n;i++){
            for(int j=0;j<m.m;j++)
                w.write(m.items[i*m.m+j]+" ");
            w.write("\n");
        }
        w.close();
    }

    static class IntervalSplitter {
        public static class Interval {
            public final int start, end;

            public Interval(int start, int end) {
                this.start = start;
                this.end = end;
            }
        }

        public static Stream<Interval> split(int n, int p) {
            final int q = n / p, r = n % p;
            return StreamSupport.stream(Spliterators.spliteratorUnknownSize(new Iterator<Interval>() {
                int counter = 0, s = 0;
                @Override
                public boolean hasNext() {
                    return counter < p;
                }

                @Override
                public Interval next() {
                    return new Interval(s, s += q + (((counter++) < r) ? 1 : 0));
                }
            }, Spliterator.IMMUTABLE), false);
        }
    }

    static void measure(Runnable r){
        long start_t = System.currentTimeMillis();
        r.run();
        long end_t=System.currentTimeMillis();
        System.out.println(">>Measured time = "+(end_t-start_t));
    }

    static void generateMatrix(){
        int K = 25, N = 100000000, L = 10;
        var rand=new Random();
        var ints = Stream.generate(rand::nextInt).limit(K+N).map(i->Math.abs(i)%L).toArray();

        try {
            FileWriter writer = new FileWriter("D:\\date2.txt");
            for(int i=0;i<ints.length;i++){
                writer.write(ints[i] + " ");
                if(i%25==24)
                    writer.write("\n");
            }
            writer.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}