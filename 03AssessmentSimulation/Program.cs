// See https://aka.ms/new-console-template for more information



Console.WriteLine("Welcome to the Assessment Simulation Program!");
Console.WriteLine("Which simulation would you like to run?");
Console.WriteLine("1. Single Threaded");
Console.WriteLine("2. Multi Threaded");

Console.Write("Enter your choice (1 or 2): ");
string inputSelection = Console.ReadLine();
if (inputSelection == "1")
{

    Console.WriteLine("Running Single Threaded Simulation.");
    RunSingleThreadedSimulation();
}
else if (inputSelection == "2")
{
    Console.WriteLine("Running Multi Threaded Simulation.");
    RunMultiThreadedSimulation();
}
else
{
    Console.WriteLine("Invalid selection. Exiting the program.");
}

void RunMultiThreadedSimulation()
{
    Queue<string> requestQueue = new Queue<string>();
    object queueLock = new object();
    while (true)
    {
        string input = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (string.IsNullOrEmpty(input) || input == "exit" || input == "quit")
        {
            Console.WriteLine("Exiting the program. Goodbye!");
            break;
        }
        lock (queueLock)
        {
            requestQueue.Enqueue(input);
        }

        // Start a new thread to process the input request
        Thread processingThread = new Thread(() => { ProcessQueue(requestQueue, queueLock); });
        processingThread.Start();
    }
}

void ProcessQueue(Queue<string> requestQueue, object queueLock)
{
    while (true)
    {
        string request;
        lock (queueLock)
        {
            if (requestQueue.Count == 0) return;
            request = requestQueue.Dequeue();
            Thread.Sleep(1000); // Simulate processing queue time
        }
        Thread processingThread = new Thread(() => ProcessInput(request));
        processingThread.Start();
    }
}



void RunSingleThreadedSimulation()
{
    while (true)
    {
        string input = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (string.IsNullOrEmpty(input) || input == "exit" || input == "quit")
        {
            Console.WriteLine("Exiting the program. Goodbye!");
            break;
        }
        ProcessInput(input);
    }
}


void ProcessInput(string input)
{
    Thread.Sleep(1000); // Simulate processing input time
    Console.WriteLine($"You entered: {input}");
}