using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.Write("Choice: ");
        bool flag = int.TryParse(Console.ReadLine(), out int choice);

        if (flag)
        {
            switch (choice)
            {
                case 0:
                    new ThreadExample();
                    break;
                case 1:
                    new DelegateExample();
                    break;
                case 2:
                    new TaskExample();
                    break;
                default:
                    break;
            }
        }

        Console.WriteLine("End of Program");
        Console.ReadKey(true);
    }
}

class TestCode
{
    public static int TestRun()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("TestRun: {0}", i);
            Thread.Sleep(1000);
        }

        return 1;
    }

    public static int TestWithException()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 3) throw new Exception("Exception thrown");
            Console.WriteLine("TestWithException: {0}", i);
            Thread.Sleep(1000);
        }

        return 2;
    }
}

class ThreadExample
{
    public ThreadExample()
    {
        Thread thread = new Thread(RunThread);
        thread.Start();
        thread.Join();
        Console.WriteLine("End of ThreadExample");

        thread = new Thread(ThreadWithException);

        // Thread exception cannot be caught in a separate thread.
        try
        {
            thread.Start();
            thread.Join();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception caught: {0}", e);
        }
    }

    private void ThreadWithException()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 3) throw new Exception("Exception thrown");    // Exception will not be caught by other thread
            Console.WriteLine("ThreadWithException: {0}", i);
            Thread.Sleep(1000);
        }
    }

    private void RunThread()
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("RunThread: {0}", i);
            Thread.Sleep(1000);
        }
    }
}

class DelegateExample
{
    public delegate int MyDelegate();

    public DelegateExample()
    {
        MyDelegate del = new MyDelegate(TestCode.TestRun);
        var ar = del.BeginInvoke(null, null);
        int result = del.EndInvoke(ar); // Gets result from the asynchornous method; blocks till result is returned
        Console.WriteLine("RunDelegate Result: {0}", result);

        del = new MyDelegate(TestCode.TestWithException);
        ar = del.BeginInvoke(null, null);
        try
        {
            result = del.EndInvoke(ar);
            Console.WriteLine("RunWithDelegate Result: {0}", result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception caught: {0}", e);
        }

        Console.WriteLine("End of DelegateExample");
    }
}

class TaskExample
{
    public TaskExample()
    {
        var task = Task.Factory.StartNew(TestCode.TestRun);
        //task.Wait();
        Console.WriteLine("RunTask Result: {0}", task.Result);  // Task.Result blocks, therefore task.Wait is irrelevant.

        task = Task.Factory.StartNew(TestCode.TestWithException);
        try
        {
            Console.WriteLine("RunTask Result: {0}", task.Result);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception caught: {0}", e);
        }

        Console.WriteLine("End of TaskExample");
    }
}