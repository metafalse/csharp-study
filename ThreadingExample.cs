using System;
using System.Threading;

public class Program
{
    public static void Main()
    {
        Thread hear = new Thread(new ThreadStart(Hear));
        hear.Start();
        Thread sight = new Thread(new ThreadStart(Sight));
        sight.Start();
        Thread touch = new Thread(new ThreadStart(Touch));
        touch.Start();
        Thread smell = new Thread(new ThreadStart(Smell));
        smell.Start();
        Thread taste = new Thread(new ThreadStart(Taste));
        taste.Start();
    }

    public static void Hear() {
        Random rand = new Random(Environment.TickCount + 10);
        int delay = rand.Next(500, 1001);
        Thread.Sleep(delay);
        Console.WriteLine("ThreadId {0}: Method Hear,  Delay {1} ms, ThreadState {2}, Priority {3}, IsAlive {4}, IsBackground {5}", 
                          Thread.CurrentThread.ManagedThreadId,
                          delay,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority,
                          Thread.CurrentThread.IsAlive,
                          Thread.CurrentThread.IsBackground);
    }

    public static void Sight() {
        Random rand = new Random(Environment.TickCount + 20);
        int delay = rand.Next(500, 1001);
        Thread.Sleep(delay);
        Console.WriteLine("ThreadId {0}: Method Sight, Delay {1} ms, ThreadState {2}, Priority {3}, IsAlive {4}, IsBackground {5}", 
                          Thread.CurrentThread.ManagedThreadId,
                          delay,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority,
                          Thread.CurrentThread.IsAlive,
                          Thread.CurrentThread.IsBackground);
    }

    public static void Touch() {
        Random rand = new Random(Environment.TickCount + 30);
        int delay = rand.Next(500, 1001);
        Thread.Sleep(delay);
        Console.WriteLine("ThreadId {0}: Method Touch, Delay {1} ms, ThreadState {2}, Priority {3}, IsAlive {4}, IsBackground {5}", 
                          Thread.CurrentThread.ManagedThreadId,
                          delay,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority,
                          Thread.CurrentThread.IsAlive,
                          Thread.CurrentThread.IsBackground);
    }

    public static void Smell() {
        Random rand = new Random(Environment.TickCount + 40);
        int delay = rand.Next(500, 1001);
        Thread.Sleep(delay);
        Console.WriteLine("ThreadId {0}: Method Smell, Delay {1} ms, ThreadState {2}, Priority {3}, IsAlive {4}, IsBackground {5}", 
                          Thread.CurrentThread.ManagedThreadId,
                          delay,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority,
                          Thread.CurrentThread.IsAlive,
                          Thread.CurrentThread.IsBackground);
    }

    public static void Taste() {
        Random rand = new Random(Environment.TickCount + 50);
        int delay = rand.Next(500, 1001);
        Thread.Sleep(delay);
        Console.WriteLine("ThreadId {0}: Method Taste, Delay {1} ms, ThreadState {2}, Priority {3}, IsAlive {4}, IsBackground {5}", 
                          Thread.CurrentThread.ManagedThreadId,
                          delay,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority,
                          Thread.CurrentThread.IsAlive,
                          Thread.CurrentThread.IsBackground);
    }
}
