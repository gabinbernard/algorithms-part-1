
public class Sorts
{
    public static void init()
    {
        Console.WriteLine("\n# Sorts\n");

        int[] test = new int[102];
        for (int i = 0; i < 102; i++)
        {
            Random random = new Random();
            test[i] = random.Next(-100, 100);
        }
        HeapSort<int>.Sort(test);
        Console.WriteLine(string.Join(", ", test));
        FisherYates<int>.Shuffle(test);
        Console.WriteLine(string.Join(", ", test));
    }
}

public class FisherYates<T>
{
    public static void Shuffle(T[] arr)
    {
        Random rand = new Random();
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
}


public class Selection<T> where T : IComparable
{
    public static void Sort(T[] arr)
    {
        int length = arr.Length;
        for (int i = 0; i < length; i++)
        {
            int min = i;
            for (int j = i + 1; j < length; j++)
                if (less(arr[j], arr[min]))
                    min = j;
            exchange(arr, i, min);
        }
    }

    private static bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }

    private static void exchange(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}

public class Insertion<T> where T : IComparable
{
    public static void Sort(T[] arr)
    {
        int length = arr.Length;
        for (int i = 1; i < length; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (less(arr[j], arr[j - 1]))
                    exchange(arr, j, j - 1);
                else break;
            }
        }
    }

    public static void Sort(T[] arr, int lo, int hi)
    {
        for (int i = lo + 1; i < hi + 1; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (less(arr[j], arr[j - 1]))
                    exchange(arr, j, j - 1);
                else break;
            }
        }
    }

    private static bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }

    private static void exchange(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}

public class Shell<T> where T : IComparable
{
    public static void Sort(T[] arr)
    {
        int length = arr.Length;
        int h = 1;
        while (h < length / 3) h = 3 * h + 1;

        while (h >= 1)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = i; j >= h && less(arr[j], arr[j - h]); j -= h)
                {
                    exchange(arr, j, j - h);
                }
            }

            h = h / 3;
        }
    }

    private static bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }

    private static void exchange(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}

public class Merge<T> where T : IComparable
{
    public static void Sort(T[] arr)
    {
        T[] aux = new T[arr.Length];
        Sort(arr, aux, 0, arr.Length - 1);
    }

    private static void Sort(T[] arr, T[] aux, int lo, int hi)
    {
        if (hi <= lo) return;
        int mid = lo + (hi - lo) / 2;
        Sort(arr, aux, lo, mid);
        Sort(arr, aux, mid + 1, hi);
        if (!less(arr[mid + 1], arr[mid])) return;
        merge(arr, aux, lo, mid, hi);
    }

    private static void merge(T[] arr, T[] aux, int lo, int mid, int hi)
    {

        for (int k = lo; k <= hi; k++)
        {
            aux[k] = arr[k];
        }

        int i = lo;
        int j = mid + 1;
        for (int k = lo; k <= hi; k++)
        {
            if (i > mid) arr[k] = aux[j++];
            else if (j > hi) arr[k] = aux[i++];
            else if (less(aux[j], aux[i])) arr[k] = aux[j++];
            else arr[k] = aux[i++];
        }
    }

    private static bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }
}

public class BottomUpMerge<T> where T : IComparable
{
    public static void Sort(T[] arr)
    {
        int N = arr.Length;
        T[] aux = new T[N];
        for (int sz = 1; sz < N; sz = sz << 1)
        {
            for (int lo = 0; lo < N - sz; lo += sz + sz)
            {
                merge(arr, aux, lo, lo + sz - 1, Math.Min(lo + sz + sz - 1, N - 1));
            }
        }
    }

    private static void merge(T[] arr, T[] aux, int lo, int mid, int hi)
    {

        for (int k = lo; k <= hi; k++)
        {
            aux[k] = arr[k];
        }

        int i = lo;
        int j = mid + 1;
        for (int k = lo; k <= hi; k++)
        {
            if (i > mid) arr[k] = aux[j++];
            else if (j > hi) arr[k] = aux[i++];
            else if (less(aux[j], aux[i])) arr[k] = aux[j++];
            else arr[k] = aux[i++];
        }
    }

    private static bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }
}

class QuickSort<T> where T : IComparable
{
    public static void Sort(T[] arr)
    {
        FisherYates<T>.Shuffle(arr);
        Sort(arr, 0, arr.Length - 1);
    }

    public static void Sort(T[] arr, int lo, int hi)
    {
        if (hi - lo < 10)
        {
            Insertion<T>.Sort(arr, lo, hi);
            return;
        }
        int j = Partition(arr, lo, hi);
        Sort(arr, lo, j - 1);
        Sort(arr, j + 1, hi);
    }

    public static int Partition(T[] arr, int lo, int hi)
    {
        int i = lo, j = hi + 1;

        while (true)
        {
            while (less(arr[++i], arr[lo]))
                if (i == hi) break;

            while (less(arr[lo], arr[--j]))
                if (j == lo) break;

            if (i >= j) break;
            exchange(arr, i, j);
        }

        exchange(arr, lo, j);
        return j;
    }

    private static bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }

    private static void exchange(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}

class HeapSort<T> where T : IComparable
{

    public static void Sort(T[] arr)
    {
        int N = arr.Length;
        for (int i = N / 2; i >= 1; i--)
        {
            Sink(arr, N, i);
        }

        while (N > 1)
        {
            exchange(arr, 1, N--);
            Sink(arr, N, 1);
        }
    }

    private static void Sink(T[] arr, int N, int k)
    {
        while (k * 2 < N)
        {
            int l = k * 2;
            if (less(arr, l, l + 1)) l++;
            if (!less(arr, k, l)) break;
            exchange(arr, k, l);
            k = l;
        }
    }

    private static bool less(T[] arr, int a, int b)
    {
        return arr[a - 1].CompareTo(arr[b - 1]) < 0;
    }

    private static void exchange(T[] arr, int a, int b)
    {
        T temp = arr[a - 1];
        arr[a - 1] = arr[b - 1];
        arr[b - 1] = temp;
    }
}