public class StacksExample
{
    public static void init()
    {
        Console.WriteLine("\n# Stacks\n");

        ArrayStack<int> llstack = new ArrayStack<int>();

        Console.WriteLine(llstack.Length());
        llstack.Push(1);
        Console.WriteLine(llstack.Length());
        llstack.Push(2);
        Console.WriteLine(llstack.Length());
        llstack.Push(3);
        Console.WriteLine(llstack.Length());
        Console.WriteLine(llstack.Pop());
        Console.WriteLine(llstack.Pop());
        llstack.Push(10);
        Console.WriteLine(llstack.Pop());
        Console.WriteLine(llstack.Length());
        for (int i = 10; i < 20; i++)
        {
            llstack.Push(i);
        }
        llstack.Print();
        foreach (var v in llstack)
        {
            Console.WriteLine(v);
        }
        for (int i = 10; i < 20; i++)
        {
            llstack.Pop();
        }
        llstack.Print();

    }
}



public class LLStack<T>
{
    private Node<T>? first;
    private class Node<NT>
    {
        internal NT value { get; set; }
        internal Node<NT>? next;
    }

    public LLStack()
    {
        this.first = null;
    }

    public void Push(T value)
    {
        Node<T> item = new Node<T>()
        {
            value = value,
            next = first
        };
        first = item;
    }

    public T Pop()
    {
        if (first == null)
        {
            throw new Exception("Trying to pop an empty stack");
        }
        Node<T> item = first;
        if (item.next == null)
        {
            first = null;
        }
        else
        {
            first = item.next;
        };
        return item.value;
    }

    public bool IsEmpty()
    {
        return first == null;
    }

    public int Length()
    {
        int len = 0;
        Node<T> cur = first;
        if (cur != null) len += 1;
        while (cur?.next != null)
        {
            cur = cur.next;
            len += 1;
        }
        return len;
    }
}

public class ArrayStack<T>
{
    T[] items;
    int current;
    int max;

    public ArrayStack()
    {
        items = new T[1];
        max = 1;
    }

    public void Push(T item)
    {
        if (current == max)
        {
            max *= 2;
            Array.Resize<T>(ref items, max);
        };
        items[current] = item;
        current += 1;
    }

    public T Pop()
    {
        if (current == 0)
        {
            throw new Exception("Trying to pop an empty stack");
        }
        current--;
        if (max > 4 && current < max * 0.25)
        {
            max /= 2;
            Array.Resize<T>(ref items, max);
        }
        return items[current];
    }

    public bool IsEmpty()
    {
        return current == 0;
    }

    public int Length()
    {
        return current;
    }

    public void Print()
    {
        Console.WriteLine("[ " + string.Join(", ", new ArraySegment<T>(items, 0, current)) + " ]");
    }

    public IEnumerator<T> GetEnumerator()
    {
        int cur = current;
        while (cur > 0)
        {
            cur--;
            yield return items[cur];
        }
    }
}