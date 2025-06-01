// first create the queue
// total number of seats in the queue is 10
using System.Collections;

var seatAvailable = 10;
var maxSeats = 10;
Queue<string> bookingQueue = new Queue<string>();
object bookingQueueLock = new object();
//running a code to simulate the booking process

Console.WriteLine("Welcome to the seat booking system!");
Console.WriteLine("please enter b to book ticket and c to cancel ticket.");
Thread processsBookingThread = new Thread(ProcessBooking);

void ProcessBooking()
{
    while (true)
    {
        lock (bookingQueueLock)
        {
            bookingQueue.TryDequeue(out string operation);
            if (operation == null)
            {
                Console.WriteLine("No customers in the queue to process.");
                Thread.Sleep(1000); // Wait before checking again
                continue;
            }
            Thread.Sleep(1000); // Simulate processing time
            if (operation == "b")
            {
                if (seatAvailable > 0)
                {

                    Console.WriteLine($"Booking ticket for {operation}...");
                    seatAvailable--;
                    Console.WriteLine($"Ticket booked for {operation}. Seats available: {seatAvailable}");
                }
                else
                {
                    Console.WriteLine("No seats available.");
                }
            }
            else if (operation == "c")
            {
                if (seatAvailable < maxSeats)
                {
                    Console.WriteLine($"Cancelling ticket for {operation}...");
                    seatAvailable++;
                    Console.WriteLine($"Ticket cancelled for {operation}. Seats available: {seatAvailable}");
                }
                else
                {
                    Console.WriteLine("No seats available to cancel.");
                }
            }
            else
            {
                Console.WriteLine("Invalid operation. Please enter 'b' to book or 'c' to cancel.");

            }
        }
    }
}

processsBookingThread.Start();
while (true)
{
    var inputCustomer = Console.ReadLine();

    Thread AddQueueThread = new Thread(() => AddQueue(inputCustomer));
    AddQueueThread.Start();
}

void AddQueue(string inputCustomer)
{
    lock (bookingQueueLock)
    {
        bookingQueue.Enqueue(inputCustomer);
    }
}
