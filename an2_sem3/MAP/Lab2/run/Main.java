package run;


import container.Strategy;
import factory.TaskContainerFactory;
import model.MessageTask;
import runner.DelayTaskRunner;
import runner.PrinterTaskRunner;
import runner.StrategyTaskRunner;
import runner.TaskRunner;
import sorter.MergeSorter;
import sorter.SortStrategy;

public class Main {
    private static TaskRunner createTaskRunner(Strategy strat) {
        TaskRunner runner = new StrategyTaskRunner(strat);
        MessageTask[] messages = TestRunner.getMessages();
        for(MessageTask task : messages) {
            runner.addTask(task);
        }
        return runner;
    }
    public static void main(String[] args) {

        String strategy = "FIFO";
        if(args.length>0){
            strategy = args[0];
        }

        Strategy strat = Strategy.valueOf(strategy);

        System.out.println("\nStrategyRunner");
        TaskRunner runner = createTaskRunner(strat);
        runner.executeAll();

        System.out.println("\nDelayRunner");
        runner = createTaskRunner(strat);
        DelayTaskRunner delayRunner = new DelayTaskRunner(runner);
        delayRunner.executeAll();

        System.out.println("\nPrinterRunner");
        runner = createTaskRunner(strat);
        PrinterTaskRunner printerRunner = new PrinterTaskRunner(runner);
        printerRunner.executeAll();

        System.out.println("\nTestRunner");
        TestRunner.run();
    }
}