void WriteThreadId()
{
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
}
void WriteThread100Times()
{
    for(int i = 0; i < 100; i++)
    {
        Console.WriteLine(Environment.CurrentManagedThreadId);
        Thread.Sleep(100);
    }
}

WriteThreadId();

Thread thread1= new Thread(WriteThread100Times);
thread1.Start();

Thread thread2= new Thread(WriteThread100Times);
thread2.Start();
Console.ReadLine();