// See https://aka.ms/new-console-template for more information
using System.Drawing;

Console.WriteLine("Hello, World!");

// Union find : Quick find
QuickFindUF quickFindUF = new QuickFindUF(10);
quickFindUF.union(0, 2);
quickFindUF.union(0, 4);
quickFindUF.union(0, 8);
quickFindUF.union(1, 5);
quickFindUF.union(4, 3);
Console.WriteLine(quickFindUF.find(2, 4));
Console.WriteLine(quickFindUF.find(3, 8));
Console.WriteLine(quickFindUF.find(0, 9));

// Union find : Quick union (weighted, path compression)
QuickUnionUF quickUnionUF = new QuickUnionUF(10);
quickUnionUF.union(0, 2);
quickUnionUF.union(0, 4);
quickUnionUF.union(0, 8);
quickUnionUF.union(1, 5);
quickUnionUF.union(4, 3);
Console.WriteLine(quickUnionUF.find(2, 4));
Console.WriteLine(quickUnionUF.find(3, 8));
Console.WriteLine(quickUnionUF.find(0, 9));

// Union find : Percolation
int percolationSize = 10;

QuickUnionUF percolationUF = new QuickUnionUF(percolationSize * percolationSize + 2);
for (int i = 0; i < percolationSize; i++) {
    percolationUF.union(i, percolationSize * percolationSize);
    percolationUF.union(i +(percolationSize * (percolationSize - 1)), percolationSize * percolationSize + 1);
}
percolationUF.print();

Random random = new Random();
bool[] table = Enumerable.Repeat(false, percolationSize * percolationSize).ToArray();
while (!percolationUF.find(100, 101)) {
    int id = random.Next(percolationSize * percolationSize);
    table[id] = true;

    if (id >= percolationSize && table[id - percolationSize]) {
        percolationUF.union(id, id - percolationSize);
    }
    if (id < (percolationSize * (percolationSize - 1)) && table[id + percolationSize]) {
        percolationUF.union(id, id + percolationSize);
    }
    if (id % percolationSize > 0 && table[id - 1]) {
        percolationUF.union(id, id - 1);
    }
    if (id % percolationSize < percolationSize - 1 && table[id + 1]) {
        percolationUF.union(id, id + 1);
    }
}
for (int i = 0; i < percolationSize; i++) {
    Console.WriteLine(string.Join("", table.Skip(i * percolationSize).Take(percolationSize).Select(v=>v==true?"#":".")));
}
