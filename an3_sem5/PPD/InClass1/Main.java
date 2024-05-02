package org.example;

import java.util.Arrays;
import java.util.Random;

public class Main {
    public static void main(String[] args) {
        Random rand = new Random();
        int N = 10;
        int P = 4;
        int L = 5;

        int[] A = new int[N];
        int[] B = new int[N];
        int[] C = new int[N];

        for (int i = 0; i < A.length; i++) {
            A[i] = rand.nextInt(L) + 1;
            B[i] = rand.nextInt(L) + 1;
            C[i] = 0;
        }

        Thread[] threads=new Thread[P];

        long start_t = System.currentTimeMillis();
        for(int i=0;i<P;i++) {
            threads[i] = new MyThread2(i, P, A, B, C);
            threads[i].start();
        }

        /*int q = N/P, r = N%P, s=0;
        long start_t = System.currentTimeMillis();
        for(int i=0;i<P;i++) {
            int len = q+((i<r)?1:0);
            threads[i] = new MyThread(i, A, B, C, s, s+=len);
            threads[i].start();
        }*/
        for(int i=0;i<P;i++) {
            try {
                threads[i].join();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
        System.out.println("Parallel time: " + (System.currentTimeMillis() - start_t)+" ms");

        System.out.println(Arrays.toString(A));
        System.out.println(Arrays.toString(B));
        System.out.println(Arrays.toString(C));
    }

    public static class MyThread extends Thread {
        public final int id;
        private final int[] a, b, c;
        private final int iStart, iEnd;

        public MyThread(int id, int[] a, int[] b, int[] c, int iStart, int iEnd) {
            this.id = id;
            this.a = a;
            this.b = b;
            this.c = c;
            this.iStart = iStart;
            this.iEnd = iEnd;
        }

        public void run(){
            for(int i=iStart;i<iEnd;i++){
                c[i]=a[i]+b[i];
            }
        }
    }

    public static class MyThread2 extends Thread {
        public final int id;
        private final int p;
        private final int[] a, b, c;
        public MyThread2(int id, int p, int[] a, int[] b, int[] c) {
            this.id = id;
            this.p = p;
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public void run(){
            for(int i=id;i<a.length;i+=p)
                c[i]=a[i]+b[i];
        }
    }
}