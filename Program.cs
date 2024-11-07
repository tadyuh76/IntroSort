using System;
using System.Text;

class Program
{
    // Threshold for switching to Insertion Sort
    const int INSERTION_SORT_THRESHOLD = 16;

    // Main method to perform Introsort
    public static void Sort(int[] arr)
    {
        int maxDepth = 2 * (int)Math.Floor(Math.Log(arr.Length, 2));  // Calculate max recursion depth
        IntrosortUtil(arr, 0, arr.Length - 1, maxDepth);
    }

    // Helper method to perform the sorting
    private static void IntrosortUtil(int[] arr, int low, int high, int depthLimit)
    {
        if (high - low <= INSERTION_SORT_THRESHOLD)
        {
            Console.WriteLine("Insertion sorting");
            // Small subarray, use Insertion Sort
            InsertionSort(arr, low, high);
        }
        else if (depthLimit == 0)
        {
            Console.WriteLine("Heap sorting");
            // Depth limit exceeded, use Heapsort
            Heapsort(arr, low, high);
        }
        else
        {
            Console.WriteLine("Quick sorting");
            // Use Quicksort
            int pivot = Partition(arr, low, high);
            IntrosortUtil(arr, low, pivot - 1, depthLimit - 1);
            IntrosortUtil(arr, pivot + 1, high, depthLimit - 1);
        }
    }

    // Insertion Sort for small subarrays
    private static void InsertionSort(int[] arr, int low, int high)
    {
        for (int i = low + 1; i <= high; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= low && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;
        }
    }

    // Heapsort for large recursion depths
    private static void Heapsort(int[] arr, int low, int high)
    {
        BuildHeap(arr, low, high);
        for (int i = high; i > low; i--)
        {
            // Swap root with the last element
            Swap(arr, low, i);
            Heapify(arr, low, i - 1, low);
        }
    }

    // Build a heap from the array
    private static void BuildHeap(int[] arr, int low, int high)
    {
        for (int i = (high - low) / 2; i >= low; i--)
        {
            Heapify(arr, low, high, i);
        }
    }

    // Heapify the array to maintain heap property
    private static void Heapify(int[] arr, int low, int high, int i)
    {
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        int largest = i;

        if (left <= high && arr[left] > arr[largest])
            largest = left;

        if (right <= high && arr[right] > arr[largest])
            largest = right;

        if (largest != i)
        {
            // Swap and continue heapifying
            Swap(arr, i, largest);
            Heapify(arr, low, high, largest);
        }
    }

    // Partition the array for Quicksort
    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    // Swap two elements in the array
    private static void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    // Main method to test the Introsort algorithm
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Nhập kích thước mảng: ");
        int size = int.Parse(Console.ReadLine());

        var rand = new Random();
        var arr = new int[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = rand.Next(100);
        }
        Console.WriteLine("Mảng ban đầu:");
        PrintArray(arr);

        Sort(arr);

        Console.WriteLine("\nMảng đã sắp xếp:");
        PrintArray(arr);
    }

    // Utility to print the array
    private static void PrintArray(int[] arr)
    {
        foreach (var num in arr)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
