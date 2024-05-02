package run;

import container.Strategy;
import model.MessageTask;
import model.SortingTask;
import runner.DelayTaskRunner;
import runner.PrinterTaskRunner;
import runner.StrategyTaskRunner;
import sorter.BubbleSorter;
import sorter.MergeSorter;
import sorter.SortStrategy;
import sorter.SorterFactory;

import java.time.LocalDateTime;

public class TestRunner {
    public static MessageTask[] getMessages() {
        MessageTask taskLaborator = new MessageTask(
                "1","Seminar", "tema laborator",
                "Florentin", "Razvan", LocalDateTime.now());
        MessageTask taskTema = new MessageTask(
                "2","Laborator", "Solutie",
                "Razvan", "Florentin", LocalDateTime.now());
        MessageTask taskNota = new MessageTask(
                "3","Nota Lab", "10",
                "Florentin", "Razvan", LocalDateTime.now());
        return new MessageTask[]{
                taskLaborator, taskTema, taskNota
        };
    }

    public static void runSorter() {
        int[] sample = new int[]{5,1,4,7,3,2,8};
        System.out.println("Bubble sorter");
        var bubble_task = new SortingTask("SB", "Bubble", sample, SortStrategy.BUBBLESORT);
        bubble_task.execute();

        System.out.println("Merge sorter");
        var merge_task = new SortingTask("SM","Merge",sample, SortStrategy.MERGESORT);
        merge_task.execute();
    }

    public static void runTaskRunners() {
        MessageTask[] messages = getMessages();

        StrategyTaskRunner runner = new StrategyTaskRunner(Strategy.LIFO);
        runner.executeAll();

        runner = new StrategyTaskRunner(Strategy.LIFO);
        PrinterTaskRunner printer = new PrinterTaskRunner(runner);
        for(MessageTask msg:messages)
            printer.addTask(msg);
        printer.executeAll();

        runner = new StrategyTaskRunner(Strategy.FIFO);
        DelayTaskRunner delayRunner = new DelayTaskRunner(runner);
        for(MessageTask msg:messages)
            delayRunner.addTask(msg);
        delayRunner.executeAll();
    }

    public static void run() {
        runSorter();
        runTaskRunners();
    }
}
