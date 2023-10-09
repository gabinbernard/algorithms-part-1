public class SymbolTables
{
    public static void init()
    {
        Console.WriteLine("\n# Symbol tables\n");

        BST<int, int> bst = new BST<int, int>();
        Random random = new Random();
        for (int i = 0; i < 25; i += 1)
        {
            int val = random.Next(100);
            bst.Put(i, val);
        }

        bst.DeleteMin();
        bst.Delete(4);
        bst.DeleteMax();
        bst.DeleteMin();
        // foreach (int i in bst)
        // {
        //     Console.WriteLine(i);
        // }

        Console.WriteLine("Size: " + bst.Size());
        Console.WriteLine("Min: " + bst.Min());
        Console.WriteLine("Max: " + bst.Max());
        Console.WriteLine("Floor 18: " + bst.Floor(18));
        Console.WriteLine("Ceiling 18: " + bst.Ceiling(18));
        Console.WriteLine("Get 20: " + bst.Get(20));

    }
}



public class BST<Key, Value> where Key : IComparable
{
    private Node<Key, Value> root;
    private int size = 0;

    private class Node<K, V>
    {
        internal K key;
        internal V value;
        internal Node<K, V> left;
        internal Node<K, V> right;
        internal int count;
        internal bool isRed;

        internal Node(K key, V value, bool isRed)
        {
            this.key = key;
            this.value = value;
            this.isRed = isRed;
        }
    }

    private bool isRed(Node<Key, Value> x)
    {
        if (x == null) return false;
        return x.isRed;
    }

    private Node<Key, Value> RotateLeft(Node<Key, Value> x)
    {
        Node<Key, Value> n = x.right;
        x.right = n.left;
        n.left = x;
        n.isRed = x.isRed;
        x.isRed = true;

        return n;
    }

    private Node<Key, Value> RotateRight(Node<Key, Value> x)
    {
        Node<Key, Value> n = x.left;
        x.left = n.right;
        n.right = x;
        n.isRed = x.isRed;
        x.isRed = true;

        return n;
    }

    private Node<Key, Value> FlipColors(Node<Key, Value> x)
    {
        x.isRed = true;
        x.right.isRed = false;
        x.left.isRed = false;

        return x;
    }

    public Object Get(Key key)
    {
        Node<Key, Value> x = root;

        while (x != null)
        {
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x = x.left;
            else if (cmp > 0) x = x.right;
            else return x.value;
        }

        return null;
    }

    public bool Contains(Key key)
    {
        return Get(key) != null;
    }

    public Key Min()
    {
        return Min(root).key;
    }

    private Node<Key, Value> Min(Node<Key, Value> x)
    {
        while (x.left != null)
            x = x.left;

        return x;
    }

    public Key Max()
    {
        return Max(root).key;
    }

    private Node<Key, Value> Max(Node<Key, Value> x)
    {
        while (x.right != null)
            x = x.right;

        return x;
    }

    public Key Floor(Key key)
    {
        Node<Key, Value> x = root;
        Node<Key, Value> floor = null;

        while (x != null)
        {
            int cmp = key.CompareTo(x.key);
            if (cmp < 0)
            {
                x = x.left;
            }
            else if (cmp > 0)
            {
                if (floor == null || floor.key.CompareTo(x.key) < 0)
                    floor = x;
                x = x.right;
            }
            else
            {
                return key;
            }
        };

        return floor.key;
    }

    public Key Ceiling(Key key)
    {
        Node<Key, Value> x = root;
        Node<Key, Value> ceiling = null;

        while (x != null)
        {
            int cmp = key.CompareTo(x.key);
            if (cmp > 0)
            {
                x = x.right;
            }
            else if (cmp < 0)
            {
                if (ceiling == null || ceiling.key.CompareTo(x.key) > 0)
                    ceiling = x;
                x = x.left;
            }
            else
            {
                return key;
            }
        };

        return ceiling.key;
    }

    public int Size()
    {
        return size;
    }

    public void Put(Key key, Value value)
    {
        root = Put(root, key, value);
    }

    private Node<Key, Value> Put(Node<Key, Value> node, Key key, Value value)
    {
        if (node == null)
        {
            size++;
            return new Node<Key, Value>(key, value, true);
        }

        int cmp = key.CompareTo(node.key);
        if (cmp < 0)
            node.left = Put(node.left, key, value);
        else if (cmp > 0)
            node.right = Put(node.right, key, value);
        else
            node.value = value;

        if (isRed(node.right) && !isRed(node.left)) node = RotateLeft(node);
        if (isRed(node.left) && isRed(node.left.left)) node = RotateRight(node);
        if (isRed(node.left) && isRed(node.right)) node = FlipColors(node);

        return node;
    }

    public void DeleteMin()
    {
        root = DeleteMin(root);
        size--;
    }

    private Node<Key, Value> DeleteMin(Node<Key, Value> x)
    {
        if (x.left == null)
        {
            return x.right;
        }
        x.left = DeleteMin(x.left);
        return x;
    }

    public void DeleteMax()
    {
        root = DeleteMax(root);
        size--;
    }

    private Node<Key, Value> DeleteMax(Node<Key, Value> x)
    {
        if (x.right == null)
        {
            return x.left;
        }
        x.right = DeleteMax(x.right);
        return x;
    }

    public void Delete(Key key)
    {
        root = Delete(root, key);
        size--;
    }


    private Node<Key, Value> Delete(Node<Key, Value> x, Key key)
    {
        if (x == null) return null;
        int cmp = key.CompareTo(x.key);
        if (cmp < 0) x.left = Delete(x.left, key);
        else if (cmp > 0) x.right = Delete(x.right, key);
        else
        {
            if (x.right == null) return x.left;
            if (x.left == null) return x.right;

            Node<Key, Value> t = x;
            x = Min(t.right);
            x.right = DeleteMin(t.right);
            x.left = t.left;
        }
        return x;
    }

    public IEnumerator<Key> GetEnumerator()
    {
        LLQueue<Key> queue = new LLQueue<Key>();
        InOrder(root, queue);
        return queue.GetEnumerator();
    }

    private void InOrder(Node<Key, Value> x, LLQueue<Key> queue)
    {
        if (x == null) return;
        InOrder(x.left, queue);
        queue.Enqueue(x.key);
        InOrder(x.right, queue);
    }
}