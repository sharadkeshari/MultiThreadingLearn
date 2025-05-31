using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var st = Stopwatch.StartNew();

st.Start();
int sum = 0;
foreach (var i in array)
{
    Thread.Sleep(50);
    sum = sum + i;
}

st.Stop();
Console.WriteLine("Sum is: " + sum);
Console.WriteLine("Time taken is: " + st.ElapsedMilliseconds);


Console.WriteLine("trying by using multi threading");

st.Restart();
int SumSegment(int start, int end)
{
    int segmentSum = 0;
    for (int i = start; i < end; i++)
    {
        Thread.Sleep(50);
        segmentSum += array[i];
    }
    return segmentSum;
}
int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;

int numberOfThreads = 4;
int segmentLength = array.Length / numberOfThreads;

Thread[] threads = new Thread[numberOfThreads];

threads[0] = new Thread(() => sum1 = SumSegment(0, segmentLength));
threads[1] = new Thread(() => sum2 = SumSegment(segmentLength, segmentLength * 2));
threads[2] = new Thread(() => sum3 = SumSegment(segmentLength * 2, segmentLength * 3));
threads[3] = new Thread(() => sum4 = SumSegment(segmentLength * 3,array.Length));

foreach (var thread in threads) { thread.Start(); }
foreach (var thread in threads) { thread.Join(); }
st.Stop();

Console.WriteLine($"Sum multi threaded is: {sum1 + sum2 + sum3 + sum4}");
Console.WriteLine("Time taken is: " + st.ElapsedMilliseconds);
