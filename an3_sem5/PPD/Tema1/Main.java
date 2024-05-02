package org.example;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.lang.reflect.Array;
import java.util.*;
import java.util.stream.Stream;
import java.util.stream.StreamSupport;

public class Main {
    public static void main(String[] args) throws Exception {
        var p = getIntArg(args, 0, 2);
        var worker = getWorkerArg(args, 1, new CyclicDistribWorker());

        int n = getIntArg(args, 2, 5);
        int m = getIntArg(args, 3, 5);

        int N = getIntArg(args, 4, 10000);
        int M = getIntArg(args, 5, 10);

        System.out.println("Mode: "+(p==0?"Sequencial" : ("Parallel "+p+" threads")));
        if(p>0) System.out.println("Worker: "+worker);
        if(p>0) System.out.println("Kernel size: "+n + "x"+m);
        if(p>0) System.out.println("Matrix size: "+N + "x"+M);

        var ints = readInts("D:\\date.txt");
        var K = getMatrix(n,m,ints,0);
        var A = getMatrix(N,M,ints, 25);

        System.out.println("Running with given settings");
        System.out.print(">>");
        Matrix R = p==0 ? runSequencial(A, K) : runParallel(A,K,p,worker);

        writeToFile("test_result.out", R);
    }

    static int getIntArg(String[] args, int i, int defaultValue){
        if(i>=args.length || i<0) return defaultValue;
        return Integer.parseInt(args[i]);
    }

    static WorkerGenerator getWorkerArg(String[] args, int i, WorkerGenerator defaultValue){
        if(i>=args.length || i<0) return defaultValue;
        if(Objects.equals(args[i], "lines")) return new LinesWorker();
        if(Objects.equals(args[i], "columns")) return new ColumnsWorker();
        if(Objects.equals(args[i], "blocks")) return new BlocksWorker();
        if(Objects.equals(args[i], "lind")) return new LinearDistribWorker();
        if(Objects.equals(args[i], "cycd")) return new CyclicDistribWorker();
        return defaultValue;
    }

    static Matrix runSequencial(Matrix A, Matrix K){
        final var R = new Matrix(A.n, A.m);
        measure(()->convoluteArea(K,A,R,0,0,A.n-1,A.m-1));
        return R;
    }

    static Matrix runParallel(Matrix A, Matrix K, int p, WorkerGenerator workerGen){
        final var R = new Matrix(A.n, A.m);
        final var worker =workerGen.getWorker(A, K, R, p);

        measure(()->worker.startAll().joinAll());
        return R;
    }

    static void measure(Runnable r){
        long start_t = System.currentTimeMillis();
        r.run();
        long end_t=System.currentTimeMillis();
        System.out.println("Measured time = "+(end_t-start_t));
    }

    static void convoluteArea(Matrix K, Matrix A, Matrix R, int i0, int j0, int i1, int j1){
        for(int i=i0;i<=i1;i++){
            for(int j=j0;j<=j1;j++){
                //convoluteAt(K,A,R,i,j);
                int ai0 = i-K.n/2, aj0 = j-K.m/2;
                int result=0;
                for(int ki=0;ki<K.n;ki++)
                    for(int kj=0;kj<K.m;kj++){
                        result+=K.get(ki,kj)*A.get(clamp(ai0+ki,0,A.n-1), clamp(aj0+kj, 0, A.m-1));
                    }
                R.set(i,j, result);
            }
        }
    }

    static void convoluteCyclic(Matrix K, Matrix A, Matrix R, int k0, int p) {
        for(int k=k0;k<A.items.length;k+=p){
            int i=k/A.m, j=k%A.m;
            int ai0 = i-K.n/2, aj0 = j-K.m/2;
            int result=0;
            for(int ki=0;ki<K.n;ki++)
                for(int kj=0;kj<K.m;kj++)
                    result+=K.get(ki,kj)*A.get(clamp(ai0+ki,0,A.n-1), clamp(aj0+kj, 0, A.m-1));
            R.items[k]=result;
        }
    }

    static void convoluteLinear(Matrix K, Matrix A, Matrix R, int k0, int k1) {
        for(int k=k0;k<k1;k++){
            int i=k/A.m, j=k%A.m;
            int ai0 = i-K.n/2, aj0 = j-K.m/2;
            int result=0;
            for(int ki=0;ki<K.n;ki++)
                for(int kj=0;kj<K.m;kj++)
                    result+=K.get(ki,kj)*A.get(clamp(ai0+ki,0,A.n-1), clamp(aj0+kj, 0, A.m-1));
            R.items[k]=result;
        }
    }

    static void convoluteLine(Matrix K, Matrix A, Matrix R, int i){
        for(int j=0;j<A.m;j++){
            //convoluteAt(K,A,R,i,j);
            int ai0 = i-K.n/2, aj0 = j-K.m/2;
            int result=0;
            for(int ki=0;ki<K.n;ki++)
                for(int kj=0;kj<K.m;kj++)
                    result+=K.get(ki,kj)*A.get(clamp(ai0+ki,0,A.n-1), clamp(aj0+kj, 0, A.m-1));
            R.set(i,j, result);
        }
    }
    static void convoluteCol(Matrix K, Matrix A, Matrix R, int j){
        for(int i=0;i<A.n;i++){
            //convoluteAt(K,A,R,i,j);
            int ai0 = i-K.n/2, aj0 = j-K.m/2;
            int result=0;
            for(int ki=0;ki<K.n;ki++)
                for(int kj=0;kj<K.m;kj++)
                    result+=K.get(ki,kj)*A.get(clamp(ai0+ki,0,A.n-1), clamp(aj0+kj, 0, A.m-1));
            R.set(i,j, result);
        }
    }
    static void convoluteAt(Matrix K, Matrix A, Matrix R, int i, int j){
        int ai0 = i-K.n/2, aj0 = j-K.m/2;
        int result=0;
        for(int ki=0;ki<K.n;ki++)
            for(int kj=0;kj<K.m;kj++){
                result+=K.get(ki,kj)*A.get(clamp(ai0+ki,0,A.n-1), clamp(aj0+kj, 0, A.m-1));
            }
        R.set(i,j, result);
    }

    static class BlocksWorker extends WorkerGenerator{
        @Override
        public ThreadsWorker getWorker(Matrix A, Matrix K, Matrix R, int p) {
            var w=new ThreadsWorker(p);
            var blocks = IntervalSplitter.slice2d(0,0,A.n, A.m, p);
            for(int i=0;i<blocks.length;i++)
                w.add(i,new WThread(i,p, A,K,R,
                        blocks[i].i, blocks[i].j, blocks[i].i+blocks[i].n, blocks[i].j+blocks[i].m));
            return w;
        }

        static class WThread extends TemplateThread{
            private final int li0, lj0, li1, lj1;
            public WThread(int id, int p, Matrix a, Matrix k, Matrix r, int li0, int lj0, int li1, int lj1) {
                super(id, p, a, k, r);
                this.li0 = li0;
                this.lj0 = lj0;
                this.li1 = li1;
                this.lj1 = lj1;
                //System.out.printf("Created Block Thread %d %d %d %d%n", li0, lj0, li1, lj1);
            }

            @Override
            public void run() {
                convoluteArea(K,A,R, li0, lj0,li1-1, lj1-1);
            }
        }
    }

    static class LinearDistribWorker extends WorkerGenerator{
        @Override
        public ThreadsWorker getWorker(Matrix A, Matrix K, Matrix R, int p) {
            var w=new ThreadsWorker(p);
            final int[] i = {0};
            IntervalSplitter.split(A.n*A.m, p)
                    .forEach(interval -> w.add(i[0], new WThread(i[0]++,p, A,K,R,interval.start, interval.end)));
            return w;
        }

        static class WThread extends TemplateThread{
            private final int i0, i1;
            public WThread(int id, int p, Matrix a, Matrix k, Matrix r, int i0, int i1) {
                super(id, p, a, k, r);
                this.i0 = i0;
                this.i1 = i1;
            }

            @Override
            public void run() {
                convoluteLinear(K,A,R,i0,i1);
            }
        }
    }

    static class CyclicDistribWorker extends WorkerGenerator{
        @Override
        public ThreadsWorker getWorker(Matrix A, Matrix K, Matrix R, int p) {
            var w=new ThreadsWorker(p);
            for(int i=0;i<p;i++)
                w.add(i, new WThread(i,p, A,K,R));
            final int[] i=new int[1];
            IntervalSplitter.split(A.n*A.m, p)
                    .forEach(interval -> w.add(i[0], new WThread(i[0]++,p, A,K,R)));
            return w;
        }

        static class WThread extends TemplateThread{
            public WThread(int id, int p, Matrix a, Matrix k, Matrix r) {
                super(id, p, a, k, r);
            }

            @Override
            public void run() {
                convoluteCyclic(K,A,R,id,p);
            }
        }
    }

    static class ColumnsWorker extends WorkerGenerator{
        @Override
        public ThreadsWorker getWorker(Matrix A, Matrix K, Matrix R, int p) {
            var w=new ThreadsWorker(p);
            final int[] i = {0};
            IntervalSplitter.split(A.n, p)
                    .forEach(interval -> w.add(i[0], new WThread(i[0]++,p, A,K,R,interval.start, interval.end)));
            return w;
        }

        static class WThread extends TemplateThread{
            private final int lStart;
            private final int lEnd;
            public WThread(int id, int p, Matrix a, Matrix k, Matrix r, int lStart, int lEnd) {
                super(id, p, a, k, r);
                this.lStart=lStart;
                this.lEnd=lEnd;
            }

            @Override
            public void run() {
                for(int j=lStart;j<lEnd;j++){
                    convoluteCol(K,A,R, j);
                }
            }
        }
    }
    static class LinesWorker extends WorkerGenerator{
        @Override
        public ThreadsWorker getWorker(Matrix A, Matrix K, Matrix R, int p) {
            var w=new ThreadsWorker(p);
            final int[] i = {0};
            IntervalSplitter.split(A.n, p)
                    .forEach(interval -> w.add(i[0], new WThread(i[0]++,p, A,K,R,interval.start, interval.end)));
            return w;
        }

        static class WThread extends TemplateThread{
            private final int lStart;
            private final int lEnd;
            public WThread(int id, int p, Matrix a, Matrix k, Matrix r, int lStart, int lEnd) {
                super(id, p, a, k, r);
                this.lStart=lStart;
                this.lEnd=lEnd;
            }

            @Override
            public void run() {
                for(int i=lStart;i<lEnd;i++) convoluteLine(K,A,R, i);
            }
        }
    }

    static abstract class WorkerGenerator{
        public abstract ThreadsWorker getWorker(Matrix A, Matrix K, Matrix R, int p);
    }

    static class IntervalSplitter{
        public static class Interval{
            public final int start, end;

            public Interval(int start, int end) {
                this.start = start;
                this.end = end;
            }
        }
        public static Stream<Interval> split(int n, int p){
            final int q = n/p, r = n%p;
            return StreamSupport.stream(Spliterators.spliteratorUnknownSize(new Iterator<Interval>() {
                int counter = 0, s=0;
                @Override
                public boolean hasNext() { return counter < p; }
                @Override
                public Interval next() {return new Interval(s, s+=q+(((counter++)<r)?1:0));}
            }, Spliterator.IMMUTABLE), false);
        }

        public static class Area {
            final int i, j, n, m;

            public Area(int i, int j, int n, int m) {
                this.i = i;
                this.j = j;
                this.n = n;
                this.m = m;
            }

            @Override
            public String toString() {
                return "Area{" +
                        "i=" + i +
                        ", j=" + j +
                        ", n=" + n +
                        ", m=" + m +
                        '}';
            }
        }

        public static Area[] slice2d(int i, int j, int n, int m, int p){
            if(p==1) return new Area[] {new Area(i,j,n,m)};
            if(n>m){
                var s1 = slice2d(i,j,n/2, m, p/2+p%2);
                var s2 = slice2d(i+n/2,j,n-n/2, m, p/2);
                return concat(s1, s2, Area.class);
            }
            else {
                var s1 = slice2d(i,j, n, m/2, p/2+p%2);
                var s2 = slice2d(i,j+m/2, n, m-m/2, p/2);
                return concat(s1, s2, Area.class);
            }
        }

        private static <T> T[] concat(T[] x, T[] y, Class<T> c){
            @SuppressWarnings("unchecked")
            final var arr=(T[]) Array.newInstance(c, x.length+y.length);
            System.arraycopy(x, 0, arr, 0, x.length);
            System.arraycopy(y, 0, arr, x.length, y.length);
            return arr;
        }
    }

    static class TemplateThread extends Thread {
        public final int id, p;
        public final Matrix A, K, R;
        public TemplateThread(int id, int p, Matrix a, Matrix k, Matrix r) {
            this.id = id;
            this.p = p;
            A = a;
            K = k;
            R = r;
        }
    }

    static class ThreadsWorker{
        private final Thread[] threads;
        public ThreadsWorker(int p) {
            this.threads = new Thread[p];
        }
        public void add(int i, Thread t){threads[i]=t;}

        public ThreadsWorker startAll(){
            for(var t:threads) t.start(); return this;
        }
        public void joinAll(){
            for(var t:threads) {
                try {
                    t.join();
                } catch (InterruptedException e) {
                    throw new RuntimeException(e);
                }
            }
        }
    }

    static Matrix getMatrix(int n, int m, int[] values, int startIndex){
        var a=new Matrix(n,m);
        System.arraycopy(values, startIndex, a.items, 0, n * m);
        return a;
    }

    static int[] readInts(String path) throws FileNotFoundException {
        int[] res = new int[1000000 + 25];
        try(Scanner scanner = new Scanner(new File(path))) {
            int i = 0;
            while (scanner.hasNextInt() && i < res.length)
                res[i++] = scanner.nextInt();
            System.out.println("Read " + i + " values");
            return res;
        }
    }

    static void generateMatrix(){
        int K = 25, N = 1000000, L = 10;
        var rand=new Random();
        var ints = Stream.generate(rand::nextInt).limit(K+N).map(i->Math.abs(i)%L).toArray();

        try {
            FileWriter writer = new FileWriter("D:\\date.txt");
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


    static void writeToFile(String path, Matrix m) throws IOException {
        FileWriter w = new FileWriter(path);
        w.write(m.toString());
        w.close();
    }

    static class Matrix{
        public final int n;
        public final int m;
        public final int[] items;
        public Matrix(int n, int m) {
            this.n=n;
            this.m=m;
            this.items = new int[n*m];
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

    static int clamp(int x, int a, int b){return x<=a?a:x>=b?b:x;}

}