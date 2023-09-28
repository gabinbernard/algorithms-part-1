public class UnionFindExample
{
    public static void init()
    {
        Console.WriteLine("\n# Union Find \n");

        int percolationSize = 10;

        QuickUnionUF percolationUF = new QuickUnionUF(percolationSize * percolationSize + 2);
        for (int i = 0; i < percolationSize; i++)
        {
            percolationUF.union(i, percolationSize * percolationSize);
            percolationUF.union(i + (percolationSize * (percolationSize - 1)), percolationSize * percolationSize + 1);
        }

        Random random = new Random();
        bool[] table = Enumerable.Repeat(false, percolationSize * percolationSize).ToArray();
        while (!percolationUF.find(100, 101))
        {
            int id = random.Next(percolationSize * percolationSize);
            table[id] = true;

            if (id >= percolationSize && table[id - percolationSize])
            {
                percolationUF.union(id, id - percolationSize);
            }
            if (id < (percolationSize * (percolationSize - 1)) && table[id + percolationSize])
            {
                percolationUF.union(id, id + percolationSize);
            }
            if (id % percolationSize > 0 && table[id - 1])
            {
                percolationUF.union(id, id - 1);
            }
            if (id % percolationSize < percolationSize - 1 && table[id + 1])
            {
                percolationUF.union(id, id + 1);
            }
        }
        for (int i = 0; i < percolationSize; i++)
        {
            Console.WriteLine(string.Join("", table.Skip(i * percolationSize).Take(percolationSize).Select(v => v == true ? "#" : ".")));
        }
    }
}

public class QuickFindUF
{
    private int[] ids;

    public QuickFindUF(int N)
    {
        ids = Enumerable.Range(0, N).ToArray();
    }

    public void union(int a, int b)
    {
        int idA = ids[a];
        int idB = ids[b];
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == idB) ids[i] = idA;
        }
    }

    public bool find(int a, int b)
    {
        return ids[a] == ids[b];
    }
}

public class QuickUnionUF
{
    private int[] ids;
    private int[] sizes;

    public QuickUnionUF(int N)
    {
        ids = Enumerable.Range(0, N).ToArray();
        sizes = Enumerable.Repeat(1, N).ToArray();
    }

    public void union(int a, int b)
    {
        int rootA = root(a);
        int rootB = root(b);
        if (rootA == rootB) return;
        if (sizes[rootA] < sizes[rootB])
        {
            ids[rootA] = rootB;
            sizes[rootB] += sizes[rootA];
        }
        else
        {
            ids[rootB] = rootA;
            sizes[rootA] += sizes[rootB];

        }
    }

    public bool find(int a, int b)
    {
        return root(a) == root(b);
    }

    private int root(int i)
    {
        while (ids[i] != i)
        {
            ids[i] = ids[ids[i]]; // Path compression
            i = ids[i];
        }
        return i;
    }

    public void print()
    {
        Console.WriteLine(string.Join(", ", ids));
    }
}

