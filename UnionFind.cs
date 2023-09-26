public class QuickFindUF {
    private int[] ids;

    public QuickFindUF(int N) {
        ids = Enumerable.Range(0, N).ToArray();
    }

    public void union(int a, int b) {
        int idA = ids[a];
        int idB = ids[b];
        for (int i = 0; i < ids.Length; i++) {
            if (ids[i] == idB) ids[i] = idA;
        }
    }

    public bool find(int a, int b) {
        return ids[a] == ids[b];
    }
}

public class QuickUnionUF {
    private int[] ids;
    private int[] sizes;

    public QuickUnionUF(int N) {
        ids = Enumerable.Range(0, N).ToArray();
        Array.Fill(sizes, 1);
    }

    public void union(int a, int b) {
        int rootA = root(a);
        int rootB = root(b);
        if (rootA == rootB) return;
        if (sizes[rootA] < sizes[rootB]) {
            ids[rootA] = rootB;
            sizes[rootB] += sizes[rootA];
        } else {
            ids[rootB] = rootA;
            sizes[rootA] += sizes[rootB];

        }
    }

    public bool find(int a, int b) {
        return root(a) == root(b);
    }

    private int root(int i) {
        while (ids[i] != i) {
            ids[i] = ids[ids[i]]; // Path compression
            i = ids[i];
        }
        return i;
    }
}

