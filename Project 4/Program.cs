using System.Diagnostics;

class TimerUtility
{
    public static void MeasureDuration(Action task)
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        task();
        stopwatch.Stop();


        long microseconds = stopwatch.Elapsed.Ticks / (TimeSpan.TicksPerMillisecond / 1000);
        Console.WriteLine("Time taken: " + microseconds + " Microseconds");
    }
}



class Program
{
    static void Main(string[] args)
    {

        int dataSize = 1000;
        int[] dataset = GenerateDataset(dataSize);

        // Write dataset to file

        string filePath = "/Users/ahmedalaguori/Projects/Project 4/Project 4/unsorteddataset.txt";
        WriteDatasetToFile(dataset, filePath);
        WriteDatasetToFile(dataset, "dataset.txt");

        RunExperiments(dataset);
    }



    static void WriteDatasetToFile(int[] dataset, string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (int value in dataset)
            {
                writer.WriteLine(value);
            }
        }
    }

    static int[] GenerateDataset(int size)
    {
        int[] dataset = new int[size];
        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            dataset[i] = random.Next();
        }
        return dataset;
    }





    static void RunExperiments(int[] originalDataset)
    {
        // Bubble Sort
        int[] datasetForBubbleSort = (int[])originalDataset.Clone();
        Console.WriteLine("Bubble Sort");
        TimerUtility.MeasureDuration(() => BubbleSort(datasetForBubbleSort));
        WriteDatasetToFile(datasetForBubbleSort, "/Users/ahmedalaguori/Projects/Project 4/Project 4/bubble_sorted_dataset.txt");
        // Quick Sort
        int[] datasetForQuickSort = (int[])originalDataset.Clone();
        Console.WriteLine("Quick Sort");
        TimerUtility.MeasureDuration(() => QuickSort(datasetForQuickSort, 0, datasetForQuickSort.Length - 1));
        WriteDatasetToFile(datasetForQuickSort, "/Users/ahmedalaguori/Projects/Project 4/Project 4/quick_sorted_dataset.txt");
    }







    //Sorting Alogrithims

    static void BubbleSort(int[] dataset)
    {
        int n = dataset.Length;
        int temp;
        bool noSwap;

        do
        {
            noSwap = true;
            for (int i = 0; i < n - 1; i++)
            {
                if (dataset[i] > dataset[i + 1])
                {
                    temp = dataset[i];
                    dataset[i] = dataset[i + 1];
                    dataset[i + 1] = temp;
                    noSwap = false;
                }
            }
        } while (!noSwap);
    }



    static void QuickSort(int[] dataset, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(dataset, low, high);

            QuickSort(dataset, low, pi - 1);
            QuickSort(dataset, pi + 1, high);
        }
    }

    private static void Swap(int[] numbers, int i, int j)
    {
        int temp = numbers[i];
        numbers[i] = numbers[j];
        numbers[j] = temp;
    }

    private static int Partition(int[] numbers, int low, int high)
    {
        int pivot = numbers[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (numbers[j] < pivot)
            {
                i++;
                Swap(numbers, i, j);
            }
        }

        Swap(numbers, i + 1, high);
        return i + 1;
    }





}

