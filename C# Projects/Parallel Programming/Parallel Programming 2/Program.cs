using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        /* Delegate */
        Console.WriteLine("Running delegate");

        // Action and Function are delegates
        {
            Action action = new Action(AsyncOp);

            // Invoke async operation, assign callback for follow up operation, pass in calling async method for later action in callback
            IAsyncResult ar = action.BeginInvoke(new AsyncCallback(MyCallback), action);

            // Run in parallel
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(800);
                Console.WriteLine("Original: {0}", i);
            }

            // Unreachable code; code contains non-runnable code; used for explanation only
            if (false)
            {
                // Do not call EndInvoke twice on the same async operation
                action.EndInvoke(ar);
            }

        }   // Using curly braces to contain declared variables internally

        Console.WriteLine("Delegate finishing/finished");
        Console.WriteLine("Press any to continue...");
        Console.ReadKey(true);  // block
        Console.WriteLine();



        /* Task */
        Console.WriteLine("Running Task");

        {
            Task task = Task.Factory.StartNew(new Action(AsyncOp));
            Task continuedTask = task.ContinueWith(new Action<Task>(TaskCallback));  // simulating assigning MyCallback

            // Run in parallel
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(800);
                Console.WriteLine("Original: {0}", i);
            }

            if (true)
            {
                Console.WriteLine("Press any to append another task");
                Console.ReadKey(true);

                // Appends another TaskCallback; starts immediately if continuedTask is already completed
                continuedTask = continuedTask.ContinueWith(ExtraTask);
            }

            Console.WriteLine("Waiting for tasks to complete");
            continuedTask.Wait();
        }

        Console.WriteLine("Task finished");
        Console.WriteLine("Press any to continue...");
        Console.ReadKey(true);  // block
        Console.WriteLine();


        /* Action with arguments */
        Console.WriteLine("Running Action<int, int>");

        {
            Action<int, int> action = new Action<int, int>(actionInputs);
            IAsyncResult ar = action.BeginInvoke(3, 5, null, null);   // Not assigning callback

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("original: {0}", i);
                Thread.Sleep(800);
            }

            action.EndInvoke(ar);
            Console.WriteLine("async action ended");

            Console.WriteLine("Starting function");
            Func<int, double> func = new Func<int, double>(funcInputs);
            ar = func.BeginInvoke(4, null, null);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("original: {0}", i);
                Thread.Sleep(800);
            }

            double funcResult = func.EndInvoke(ar);
            Console.WriteLine("Function result: {0}", funcResult);
        }

        Console.WriteLine("Program ended");
        Console.WriteLine("Press to exit");
        Console.ReadKey(true);
    }

    private static double funcInputs(int a)
    {
        for (int i =0; i < 5; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Simulate wait: {0}", i);
        }
        return a / 2.6;
    }

    private static void actionInputs(int a, int b)
    {
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine("action: {0}", (a + b) * i);
        }
        Console.WriteLine("action ended");
    }

    private static void AsyncOp()
    {
        for (int i = 0; i < 5; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Async: {0}", i);
        }
        Console.WriteLine("Async ended");
    }

    private static void MyCallback(IAsyncResult ar)
    {
        // Reference to the calling async method is obtained
        Action action = (Action)ar.AsyncState;

        // Ensures async operation has ended; blocks if otherwise (callback is supposed to be run at end of async op, so this should be redundant, unless used for catching exceptions)
        action.EndInvoke(ar);

        Console.WriteLine("In callback");
    }

    private static void TaskCallback(Task task)
    {
        task.Wait();    // simulate EndInvoke

        Console.WriteLine("In Task callback");
    }

    private static void ExtraTask(Task obj)
    {
        Console.WriteLine("Running extra task");
        Thread.Sleep(1200);
        Console.WriteLine("Extra task finished");
    }

}