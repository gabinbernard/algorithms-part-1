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