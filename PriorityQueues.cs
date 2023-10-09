public class PriorityQueues
{

    public static void init()
    {
        Console.WriteLine("\n# Priority queues\n");

        UnorderedMinPQ<int> pq = new UnorderedMinPQ<int>();
        pq.Insert(2);
        pq.Insert(4);
        pq.Insert(8);
        pq.Insert(1);
        Console.WriteLine(pq.DelMin());
        Console.WriteLine(pq.DelMin());
        Console.WriteLine(pq.DelMin());

        HeapMaxPQ<int> heappq = new HeapMaxPQ<int>();
        heappq.Insert(2);
        heappq.Insert(45);
        heappq.Insert(4);
        heappq.Insert(1);
        heappq.Insert(22);
        heappq.Insert(2);
        heappq.Insert(40);
        heappq.Insert(3);
        heappq.Insert(8);
        heappq.Insert(21);
        Console.WriteLine(heappq.DelMax());
        Console.WriteLine(heappq.DelMax());
        Console.WriteLine(heappq.DelMax());
        Console.WriteLine(heappq.DelMax());
        Console.WriteLine(heappq.DelMax());
    }

}

public class UnorderedMinPQ<T> where T : IComparable
{
    private Node<T>? first = null;
    private int size = 0;

    private class Node<NT>
    {
        internal T? Value { get; set; }
        internal Node<NT>? Next;
    }

    public bool IsEmpty()
    {
        return first == null;
    }

    public void Insert(T elem)
    {
        Node<T> newElem = new Node<T>()
        {
            Value = elem,
            Next = first
        };
        first = newElem;
        size += 1;
    }

    public T DelMin()
    {
        if (IsEmpty())
        {
            throw new Exception("Cannot DelMin from empty queue");
        }

        T? min = first.Value;
        Node<T> previous = new Node<T>
        {
            Value = default(T),
            Next = first
        };

        Node<T> cur = first;
        while (cur.Next != null)
        {
            if (cur.Next.Value.CompareTo(min) < 0)
            {
                min = cur.Next.Value;
                previous = cur;
            }
            cur = cur.Next;
        }

        Node<T> minNode = previous.Next;
        if (previous.Next == first)
        {
            first = first.Next;
        }
        else
        {
            previous.Next = previous.Next.Next;
        }
        return minNode.Value;
    }
}

public class HeapMaxPQ<T> where T : IComparable
{
    T[] elems;
    int N = 0;

    public HeapMaxPQ()
    {
        elems = new T[100];
    }

    public void Insert(T elem)
    {
        elems[++N] = elem;
        Swim(N);
    }

    public T Max()
    {
        if (N == 0)
        {
            throw new Exception("Trying to get max of empty priority queue");
        }
        return elems[1];
    }

    public T DelMax()
    {
        if (N == 0)
        {
            throw new Exception("Trying to get max of empty priority queue");
        }
        T max = elems[1];
        exchange(elems, 1, N);

        elems[N--] = default;
        Sink(1);

        return max;
    }

    private void Sink(int k)
    {
        while (k * 2 <= N)
        {
            int l = k * 2;
            if (less(elems[l], elems[l + 1])) l++;
            if (!less(elems[k], elems[l])) break;
            exchange(elems, k, l);
            k = l;
        }
    }

    private void Swim(int k)
    {
        while (k > 1 && less(elems[k / 2], elems[k]))
        {
            exchange(elems, k, k / 2);
            k = k / 2;
        }
    }

    private bool less(T a, T b)
    {
        return a.CompareTo(b) < 0;
    }

    private void exchange(T[] arr, int a, int b)
    {
        T temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }
}